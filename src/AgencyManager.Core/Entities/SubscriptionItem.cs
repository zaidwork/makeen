using AgencyManager.Core.Enums;

namespace AgencyManager.Core.Entities;

public class SubscriptionItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ServiceName { get; set; } = string.Empty; // أدوبي، فيجما، سيرفر Hetzner، دومينات...
    public decimal Cost { get; set; } // التكلفة بالشيكل ₪
    public BillingCycle Cycle { get; set; } = BillingCycle.Monthly;
    public DateTime NextRenewalDate { get; set; } = DateTime.Now.AddDays(30);
    public string PaymentCardOrMethod { get; set; } = "بطاقة الشركة";
    public int AlertDaysBefore { get; set; } = 3; // تنبيه قبل 3 أيام
    public bool AutoRenew { get; set; } = true;
    public bool IsActive { get; set; } = true;
    public string Notes { get; set; } = string.Empty;

    // خاصية مساعدة لحساب الأيام المتبقية للتجديد
    public int DaysUntilRenewal => (int)Math.Ceiling((NextRenewalDate.Date - DateTime.Now.Date).TotalDays);
    public bool IsUrgentRenewal => DaysUntilRenewal <= AlertDaysBefore && DaysUntilRenewal >= 0;
}
