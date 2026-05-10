namespace quizsergipe_api.Models;

public class Pergunta
{
    public int Id { get; set; }
    public string Enunciado { get; set; } = string.Empty;
    public ICollection<Alternativa> Alternativas { get; set; } = new List<Alternativa>();
}
