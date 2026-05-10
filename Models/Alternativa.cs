namespace quizsergipe_api.Models;

public class Alternativa
{
    public int Id { get; set; }
    public int PerguntaId { get; set; }
    public string Letra { get; set; } = string.Empty;
    public string Texto { get; set; } = string.Empty;
    public bool Correta { get; set; }

    public Pergunta? Pergunta { get; set; }
}
