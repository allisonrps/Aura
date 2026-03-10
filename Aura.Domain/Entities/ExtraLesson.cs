namespace Aura.Domain.Entities;

public class ExtraLesson : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty; // Pode ser texto ou um link
    public string? Category { get; set; } // Ex: Gramática, Exercícios, Vídeos
    
    public Guid TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;
}