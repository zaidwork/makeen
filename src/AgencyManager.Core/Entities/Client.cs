namespace AgencyManager.Core.Entities;

public class Client
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FullName { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // العقود والدفعات والشيكات
    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    public ICollection<PaymentRecord> Payments { get; set; } = new List<PaymentRecord>();
    public ICollection<VaultCredential> Credentials { get; set; } = new List<VaultCredential>();
    public ICollection<BankCheck> Checks { get; set; } = new List<BankCheck>();

    // حسابات مالية مجمعة بالشيكل ₪
    public decimal TotalAgreedDeals => Contracts?.Sum(c => c.TotalAmount) ?? 0;
    public decimal TotalPaid => Payments?.Where(p => p.IsConfirmed).Sum(p => p.Amount) ?? 0;
    public decimal BalanceDue => TotalAgreedDeals - TotalPaid;
}
