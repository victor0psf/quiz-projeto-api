namespace quizsergipe_api.Models;

public class QuizSessaoPergunta
{
    public int Id { get; set; }
    public Guid QuizSessaoId { get; set; }
    public int PerguntaId { get; set; }
    public int Ordem { get; set; }

    public QuizSessao? QuizSessao { get; set; }
    public Pergunta? Pergunta { get; set; }
}
