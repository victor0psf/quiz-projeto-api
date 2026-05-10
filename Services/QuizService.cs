using Microsoft.EntityFrameworkCore;
using quizsergipe_api.Data;
using quizsergipe_api.Dtos;
using quizsergipe_api.Models;

namespace quizsergipe_api.Services;

public class QuizService(QuizDbContext context) : IQuizService
{
    public async Task<QuizIniciadoDto> IniciarQuizAsync(CancellationToken cancellationToken = default)
    {
        var perguntas = await context.Perguntas
            .Include(x => x.Alternativas)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        if (perguntas.Count == 0)
        {
            throw new InvalidOperationException("Nenhuma pergunta cadastrada.");
        }

        var perguntaIdsRandomizados = perguntas
            .OrderBy(_ => Guid.NewGuid())
            .Select(x => x.Id)
            .ToList();

        var quizSessao = new QuizSessao
        {
            Id = Guid.NewGuid(),
            IniciadoEm = DateTime.UtcNow,
            Perguntas = perguntaIdsRandomizados
                .Select((perguntaId, indice) => new QuizSessaoPergunta
                {
                    PerguntaId = perguntaId,
                    Ordem = indice + 1
                })
                .ToList()
        };

        context.QuizSessoes.Add(quizSessao);
        await context.SaveChangesAsync(cancellationToken);

        var perguntasPorId = perguntas.ToDictionary(x => x.Id);
        var perguntasDto = quizSessao.Perguntas
            .OrderBy(x => x.Ordem)
            .Select(x => MapPerguntaDto(perguntasPorId[x.PerguntaId], x.Ordem))
            .ToList();

        return new QuizIniciadoDto
        {
            QuizId = quizSessao.Id,
            TotalPerguntas = perguntasDto.Count,
            PrimeiraPergunta = perguntasDto.FirstOrDefault()
        };
    }

    public async Task<IReadOnlyCollection<PerguntaDto>> ListarPerguntasAsync(Guid quizId, CancellationToken cancellationToken = default)
    {
        var perguntas = await context.QuizSessaoPerguntas
            .AsNoTracking()
            .Where(x => x.QuizSessaoId == quizId)
            .Include(x => x.Pergunta!)
            .ThenInclude(x => x.Alternativas)
            .OrderBy(x => x.Ordem)
            .ToListAsync(cancellationToken);

        if (perguntas.Count == 0)
        {
            throw new KeyNotFoundException("Quiz nao encontrado.");
        }

        return perguntas
            .Select(x => MapPerguntaDto(x.Pergunta!, x.Ordem))
            .ToList();
    }

    public async Task<PerguntaDto?> BuscarPerguntaAsync(Guid quizId, int ordem, CancellationToken cancellationToken = default)
    {
        var pergunta = await context.QuizSessaoPerguntas
            .AsNoTracking()
            .Where(x => x.QuizSessaoId == quizId && x.Ordem == ordem)
            .Include(x => x.Pergunta!)
            .ThenInclude(x => x.Alternativas)
            .SingleOrDefaultAsync(cancellationToken);

        return pergunta is null ? null : MapPerguntaDto(pergunta.Pergunta!, pergunta.Ordem);
    }

    public async Task<ResultadoRespostaDto> ResponderPerguntaAsync(RespostaPerguntaDto dto, CancellationToken cancellationToken = default)
    {
        var quizSessao = await context.QuizSessoes
            .FirstOrDefaultAsync(x => x.Id == dto.QuizId, cancellationToken);

        if (quizSessao is null)
        {
            throw new KeyNotFoundException("Quiz nao encontrado.");
        }

        if (quizSessao.FinalizadoEm.HasValue)
        {
            throw new InvalidOperationException("Quiz ja finalizado.");
        }

        var perguntaDoQuiz = await context.QuizSessaoPerguntas
            .AsNoTracking()
            .AnyAsync(x => x.QuizSessaoId == dto.QuizId && x.PerguntaId == dto.PerguntaId, cancellationToken);

        if (!perguntaDoQuiz)
        {
            throw new InvalidOperationException("Pergunta nao pertence a este quiz.");
        }

        var respostaExistente = await context.RespostasUsuario
            .AsNoTracking()
            .AnyAsync(x => x.QuizSessaoId == dto.QuizId && x.PerguntaId == dto.PerguntaId, cancellationToken);

        if (respostaExistente)
        {
            throw new InvalidOperationException("Esta pergunta ja foi respondida.");
        }

        var alternativas = await context.Alternativas
            .Where(x => x.PerguntaId == dto.PerguntaId)
            .ToListAsync(cancellationToken);

        var alternativaSelecionada = alternativas.FirstOrDefault(x => x.Id == dto.AlternativaId);
        if (alternativaSelecionada is null)
        {
            throw new InvalidOperationException("Alternativa invalida para esta pergunta.");
        }

        var alternativaCorreta = alternativas.First(x => x.Correta);

        var resposta = new RespostaUsuario
        {
            QuizSessaoId = dto.QuizId,
            PerguntaId = dto.PerguntaId,
            AlternativaId = dto.AlternativaId,
            Acertou = alternativaSelecionada.Correta,
            RespondidaEm = DateTime.UtcNow
        };

        context.RespostasUsuario.Add(resposta);
        await context.SaveChangesAsync(cancellationToken);

        return new ResultadoRespostaDto
        {
            QuizId = dto.QuizId,
            PerguntaId = dto.PerguntaId,
            AlternativaSelecionadaId = dto.AlternativaId,
            Acertou = resposta.Acertou,
            AlternativaCorreta = new AlternativaDto
            {
                Id = alternativaCorreta.Id,
                Letra = alternativaCorreta.Letra,
                Texto = alternativaCorreta.Texto
            }
        };
    }

    public async Task<ResultadoQuizDto> FinalizarQuizAsync(Guid quizId, CancellationToken cancellationToken = default)
    {
        var quizSessao = await context.QuizSessoes
            .FirstOrDefaultAsync(x => x.Id == quizId, cancellationToken);

        if (quizSessao is null)
        {
            throw new KeyNotFoundException("Quiz nao encontrado.");
        }

        var totalPerguntas = await context.QuizSessaoPerguntas
            .CountAsync(x => x.QuizSessaoId == quizId, cancellationToken);

        var totalRespondidas = await context.RespostasUsuario
            .CountAsync(x => x.QuizSessaoId == quizId, cancellationToken);

        var totalAcertos = await context.RespostasUsuario
            .CountAsync(x => x.QuizSessaoId == quizId && x.Acertou, cancellationToken);

        var totalErros = totalPerguntas - totalAcertos;
        var pontuacaoFinal = totalPerguntas == 0
            ? 0
            : Math.Round((decimal)totalAcertos / totalPerguntas * 100, 2);

        if (!quizSessao.FinalizadoEm.HasValue)
        {
            quizSessao.FinalizadoEm = DateTime.UtcNow;
            quizSessao.PontuacaoFinal = totalAcertos;
            await context.SaveChangesAsync(cancellationToken);
        }

        return new ResultadoQuizDto
        {
            QuizId = quizId,
            TotalPerguntas = totalPerguntas,
            TotalRespondidas = totalRespondidas,
            TotalAcertos = totalAcertos,
            TotalErros = totalErros,
            PontuacaoFinal = pontuacaoFinal
        };
    }

    private static PerguntaDto MapPerguntaDto(Pergunta pergunta, int ordem)
    {
        return new PerguntaDto
        {
            Id = pergunta.Id,
            Ordem = ordem,
            Enunciado = pergunta.Enunciado,
            Alternativas = pergunta.Alternativas
                .OrderBy(x => x.Letra)
                .Select(x => new AlternativaDto
                {
                    Id = x.Id,
                    Letra = x.Letra,
                    Texto = x.Texto
                })
                .ToList()
        };
    }
}
