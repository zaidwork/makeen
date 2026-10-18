using AgencyManager.Core.Enums;

namespace AgencyManager.Core.Entities;

public class CompanyExpense
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty; // سلفة، مكافأة، شراء كيبورد، فاتورة كهرباء...
    public decimal Amount { get; set; } // المبلغ بالشيكل ₪
    public ExpenseCategory Category { get; set; } = ExpenseCategory.TeamSalaryOrAdvance;

    // في حال كان المصروف مرتبط بعضو فريق محدد (سلفة أو مستحقات)
    public Guid? TeamMemberId { get; set; }
    public TeamMember? TeamMember { get; set; }

    public DateTime ExpenseDate { get; set; } = DateTime.Now;
    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;
    public string ReceiptReferenceOrNote { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
