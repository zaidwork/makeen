using AgencyManager.Core.Enums;

namespace AgencyManager.Core.Entities;

public class Contract
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ContractNumber { get; set; } = $"CNT-{DateTime.Now:yyyyMMdd}-{new Random().Next(100, 999)}";
    public string Title { get; set; } = string.Empty;
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }

    public decimal TotalAmount { get; set; } // بالشيكل ₪
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime DeliveryDeadline { get; set; } = DateTime.Now.AddDays(30);
    public ContractStatus Status { get; set; } = ContractStatus.Draft;

    // بنود ونطاق المشروع
    public string ProjectScope { get; set; } = string.Empty;
    public string TermsAndConditions { get; set; } = "1. يعتبر هذا العقد ملزماً للطرفين.\n2. يتم تسليم العمل وفق المراحل المتفق عليها.\n3. الدفعات تتم حسب التواريخ الموضحة.";

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string TrackingToken { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public ICollection<ContractItem> Items { get; set; } = new List<ContractItem>();
    public ICollection<PaymentRecord> Payments { get; set; } = new List<PaymentRecord>();
    public ICollection<TaskItem> GeneratedTasks { get; set; } = new List<TaskItem>();
    public ICollection<BankCheck> Checks { get; set; } = new List<BankCheck>();

    public decimal TotalPaid => Payments?.Where(p => p.IsConfirmed).Sum(p => p.Amount) ?? 0;
    public decimal RemainingBalance => TotalAmount - TotalPaid;

    // محرك حساب التكلفة وصافي الأرباح الفعلية للمشروع بالشيكل ₪
    public decimal ActualLaborCost => GeneratedTasks?.Sum(t => t.ActualHoursSpent * (t.AssignedToUser?.HourlyRate ?? 45m)) ?? 0;
    public decimal ActualNetProfit => TotalAmount - ActualLaborCost;
    public decimal ProfitMarginPercentage => TotalAmount > 0 ? Math.Round((ActualNetProfit / TotalAmount) * 100m, 1) : 0;

    public int ProgressPercentage
    {
        get
        {
            if (Status == ContractStatus.Completed) return 100;
            if (GeneratedTasks != null && GeneratedTasks.Count > 0)
            {
                var completed = GeneratedTasks.Count(t => t.Status == TaskItemStatus.Completed);
                return (int)Math.Round((double)completed / GeneratedTasks.Count * 100);
            }
            if (TotalAmount > 0 && TotalPaid > 0)
            {
                return (int)Math.Min(100, Math.Round((TotalPaid / TotalAmount) * 100));
            }
            return Status == ContractStatus.Active ? 40 : 15;
        }
    }
}
