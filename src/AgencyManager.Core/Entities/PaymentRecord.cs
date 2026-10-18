using AgencyManager.Core.Enums;

namespace AgencyManager.Core.Entities;

public class PaymentRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }

    public Guid? ContractId { get; set; }
    public Contract? Contract { get; set; }

    public decimal Amount { get; set; } // بالشيكل ₪
    public DateTime PaymentDate { get; set; } = DateTime.Now;
    public PaymentMethod Method { get; set; } = PaymentMethod.Cash;
    public string ReferenceNumber { get; set; } = string.Empty; // رقم الشيك، رقم إشعار الحوالة، أو مرجع Bit
    public string Notes { get; set; } = string.Empty;
    public bool IsConfirmed { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
