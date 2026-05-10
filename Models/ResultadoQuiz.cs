namespace quizsergipe_api.Models;

public class ResultadoQuiz
{
    public Guid QuizId { get; set; }
    public int TotalPerguntas { get; set; }
    public int TotalRespondidas { get; set; }
    public int TotalAcertos { get; set; }
    public int TotalErros { get; set; }
    public decimal PontuacaoFinal { get; set; }
}
