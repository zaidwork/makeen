using AgencyManager.Core.Enums;

namespace AgencyManager.Core.Entities;

public class BankCheck
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }

    public Guid? ContractId { get; set; }
    public Contract? Contract { get; set; }

    public string CheckNumber { get; set; } = string.Empty; // رقم الشيك
    public string BankName { get; set; } = string.Empty; // بنك فلسطين، العربي، القدس، هبوعليم، لئومي...
    public string BranchName { get; set; } = string.Empty; // الفرع
    public string DrawerName { get; set; } = string.Empty; // اسم صاحب الشيك / الساحب

    public decimal Amount { get; set; } // القيمة بالشيكل ₪
    public DateTime ReceivedDate { get; set; } = DateTime.Now; // تاريخ استلام الشيك
    public DateTime DueDate { get; set; } = DateTime.Now.AddDays(30); // موعد استحقاق الصرف

    public CheckStatus Status { get; set; } = CheckStatus.InVault;
    public string Notes { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // خصائص مساعدة لحساب المتبقي لصرف الشيك والتنبيه
    public int DaysUntilDue => (int)Math.Ceiling((DueDate.Date - DateTime.Now.Date).TotalDays);
    public bool IsDueSoon => Status == CheckStatus.InVault && DaysUntilDue <= 3 && DaysUntilDue >= 0;
    public bool IsOverdue => Status == CheckStatus.InVault && DaysUntilDue < 0;
}
