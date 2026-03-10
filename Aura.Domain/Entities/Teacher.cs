namespace Aura.Domain.Entities;

public class Teacher : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    // Relacionamentos
    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<ExtraLesson> ExtraLessons { get; set; } = new List<ExtraLesson>();
}