namespace Aura.Domain.Enums;

public enum PaymentStatus
{
    Pending = 1,
    Paid = 2,
    Overdue = 3,
    Canceled = 4
}

public enum PaymentMethod
{
    Pix = 1,
    CreditCard = 2,
    Cash = 3,
    BankTransfer = 4
}