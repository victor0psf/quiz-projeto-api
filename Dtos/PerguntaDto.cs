namespace quizsergipe_api.Dtos;

public class PerguntaDto
{
    public int Id { get; set; }
    public int Ordem { get; set; }
    public string Enunciado { get; set; } = string.Empty;
    public IReadOnlyCollection<AlternativaDto> Alternativas { get; set; } = Array.Empty<AlternativaDto>();
}
