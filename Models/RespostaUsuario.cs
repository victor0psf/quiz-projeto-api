namespace quizsergipe_api.Models;

public class RespostaUsuario
{
    public int Id { get; set; }
    public Guid QuizSessaoId { get; set; }
    public int PerguntaId { get; set; }
    public int AlternativaId { get; set; }
    public bool Acertou { get; set; }
    public DateTime RespondidaEm { get; set; }

    public QuizSessao? QuizSessao { get; set; }
    public Pergunta? Pergunta { get; set; }
    public Alternativa? Alternativa { get; set; }
}
