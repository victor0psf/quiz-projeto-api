namespace quizsergipe_api.Models;

public class QuizSessao
{
    public Guid Id { get; set; }
    public DateTime IniciadoEm { get; set; }
    public DateTime? FinalizadoEm { get; set; }
    public int? PontuacaoFinal { get; set; }

    public ICollection<QuizSessaoPergunta> Perguntas { get; set; } = new List<QuizSessaoPergunta>();
    public ICollection<RespostaUsuario> Respostas { get; set; } = new List<RespostaUsuario>();
}
