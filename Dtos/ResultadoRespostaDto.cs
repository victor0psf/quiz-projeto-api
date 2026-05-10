namespace quizsergipe_api.Dtos;

public class ResultadoRespostaDto
{
    public Guid QuizId { get; set; }
    public int PerguntaId { get; set; }
    public int AlternativaSelecionadaId { get; set; }
    public bool Acertou { get; set; }
    public AlternativaDto AlternativaCorreta { get; set; } = new();
}
