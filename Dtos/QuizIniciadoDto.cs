namespace quizsergipe_api.Dtos;

public class QuizIniciadoDto
{
    public Guid QuizId { get; set; }
    public int TotalPerguntas { get; set; }
    public PerguntaDto? PrimeiraPergunta { get; set; }
}
