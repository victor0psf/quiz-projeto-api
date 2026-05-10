using Microsoft.AspNetCore.Mvc;
using quizsergipe_api.Dtos;
using quizsergipe_api.Services;

namespace quizsergipe_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizController(IQuizService quizService) : ControllerBase
{
    [HttpPost("iniciar")]
    public async Task<ActionResult<QuizIniciadoDto>> Iniciar(CancellationToken cancellationToken)
    {
        var quiz = await quizService.IniciarQuizAsync(cancellationToken);
        return Ok(quiz);
    }

    [HttpGet("{quizId:guid}/perguntas")]
    public async Task<ActionResult<IReadOnlyCollection<PerguntaDto>>> ListarPerguntas(Guid quizId, CancellationToken cancellationToken)
    {
        try
        {
            var perguntas = await quizService.ListarPerguntasAsync(quizId, cancellationToken);
            return Ok(perguntas);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }

    [HttpGet("{quizId:guid}/perguntas/{ordem:int}")]
    public async Task<ActionResult<PerguntaDto>> BuscarPergunta(Guid quizId, int ordem, CancellationToken cancellationToken)
    {
        var pergunta = await quizService.BuscarPerguntaAsync(quizId, ordem, cancellationToken);
        if (pergunta is null)
        {
            return NotFound(new { mensagem = "Pergunta nao encontrada para este quiz." });
        }

        return Ok(pergunta);
    }

    [HttpPost("responder")]
    public async Task<ActionResult<ResultadoRespostaDto>> Responder([FromBody] RespostaPerguntaDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var resultado = await quizService.ResponderPerguntaAsync(dto, cancellationToken);
            return Ok(resultado);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpPost("{quizId:guid}/finalizar")]
    public async Task<ActionResult<ResultadoQuizDto>> Finalizar(Guid quizId, CancellationToken cancellationToken)
    {
        try
        {
            var resultado = await quizService.FinalizarQuizAsync(quizId, cancellationToken);
            return Ok(resultado);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}
