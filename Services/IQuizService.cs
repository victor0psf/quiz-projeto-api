using quizsergipe_api.Dtos;

namespace quizsergipe_api.Services;

public interface IQuizService
{
    Task<QuizIniciadoDto> IniciarQuizAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<PerguntaDto>> ListarPerguntasAsync(Guid quizId, CancellationToken cancellationToken = default);
    Task<PerguntaDto?> BuscarPerguntaAsync(Guid quizId, int ordem, CancellationToken cancellationToken = default);
    Task<ResultadoRespostaDto> ResponderPerguntaAsync(RespostaPerguntaDto dto, CancellationToken cancellationToken = default);
    Task<ResultadoQuizDto> FinalizarQuizAsync(Guid quizId, CancellationToken cancellationToken = default);
}
