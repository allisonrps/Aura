namespace Aura.Domain.Entities;

public class Student : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty; // Acesso futuro do aluno
    public string? PhotoUrl { get; set; }
    public string Phone { get; set; } = string.Empty;
    
    // Localização
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = "Brasil";

    // Acadêmico
    public string Subject { get; set; } = string.Empty; // Matéria
    public string Level { get; set; } = string.Empty;   // Nível
    public string? Observations { get; set; }
    public decimal CurrentGrade { get; set; } // Nota atual/média
    
    // Relacionamento com o Professor (Dono do registro)
    public Guid TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;

    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}