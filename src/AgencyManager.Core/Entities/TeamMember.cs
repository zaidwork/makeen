using AgencyManager.Core.Enums;

namespace AgencyManager.Core.Entities;

public class TeamMember
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FullName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public RoleType Role { get; set; } = RoleType.Developer;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public decimal MonthlyRateOrSalary { get; set; } // بالشيكل ₪
    public decimal HourlyRate => MonthlyRateOrSalary > 0 ? Math.Round(MonthlyRateOrSalary / 160m, 2) : 50; // سعر الساعة بالشيكل ₪
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // علاقات
    public ICollection<TaskItem> AssignedTasks { get; set; } = new List<TaskItem>();
    public ICollection<CompanyExpense> MemberExpenses { get; set; } = new List<CompanyExpense>();
}
