namespace quizsergipe_api.Dtos;

public class RespostaPerguntaDto
{
    public Guid QuizId { get; set; }
    public int PerguntaId { get; set; }
    public int AlternativaId { get; set; }
}
