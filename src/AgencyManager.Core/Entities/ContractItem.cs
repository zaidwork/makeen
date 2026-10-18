using AgencyManager.Core.Enums;

namespace AgencyManager.Core.Entities;

public class ContractItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ContractId { get; set; }
    public Contract? Contract { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; } // بالشيكل ₪
    public RoleType TargetRole { get; set; } = RoleType.Developer;
    public int EstimatedHours { get; set; } = 10;

    // تم توليد تاسك له أم لا
    public bool IsConvertedToTask { get; set; } = false;
    public Guid? AssignedToUserId { get; set; }
    public TeamMember? AssignedToUser { get; set; }
}
