using Aura.Domain.Enums;

namespace Aura.Domain.Entities;

public class Payment : BaseEntity
{
    public decimal Value { get; set; }
    public DateTime DueDate { get; set; } // Data de vencimento
    public PaymentMethod Method { get; set; }
    public PaymentStatus Status { get; set; }
    
    public Guid StudentId { get; set; }
    public Student Student { get; set; } = null!;
}