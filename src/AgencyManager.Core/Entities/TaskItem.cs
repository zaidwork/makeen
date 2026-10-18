using AgencyManager.Core.Enums;

namespace AgencyManager.Core.Entities;

public class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // المشروع أو العقد المرتبط
    public Guid? ContractId { get; set; }
    public Contract? Contract { get; set; }

    // العضو المسؤول عن التاسك
    public Guid AssignedToUserId { get; set; }
    public TeamMember? AssignedToUser { get; set; }

    // بيانات العمل والوقت
    public DateTime Deadline { get; set; } = DateTime.Now.AddDays(7);
    public decimal EstimatedHours { get; set; } = 4; // كم متوقع يوخذ وقت
    public decimal ActualHoursSpent { get; set; } = 0; // كم اشتغل فعلياً
    public int ProgressPercentage { get; set; } = 0; // لوين وصل (0 - 100%)

    // الحالة والأولوية
    public TaskItemStatus Status { get; set; } = TaskItemStatus.Pending;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;

    public string DeliverableLinkOrNotes { get; set; } = string.Empty; // رابط المخرجات (Figma, GitHub PR, Drive)

    // عداد الوقت الحي (Live Stopwatch)
    public bool IsTimerRunning { get; set; } = false;
    public DateTime? TimerStartedAt { get; set; }

    // ضبط جولات التعديل (Revision Rounds Trap)
    public int MaxFreeRevisions { get; set; } = 2; // الجولات المجانية المتفق عليها
    public int CurrentRevisionsCount { get; set; } = 0; // الجولات المستهلكة
    public bool IsRevisionLimitExceeded => CurrentRevisionsCount > MaxFreeRevisions;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? CompletedAt { get; set; }
}
