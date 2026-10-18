namespace AgencyManager.Core.Enums;

public enum RoleType
{
    Admin = 1,          // مدير النظام / المالك
    Developer = 2,      // مبرمج
    Designer = 3,       // مصمم جرافيك / واجهات
    VideoEditor = 4,    // مونتير / فيديو
    ProjectManager = 5  // مدير مشاريع
}

public static class RoleTypeExtensions
{
    public static string ToArabic(this RoleType role) => role switch
    {
        RoleType.Admin => "مدير النظام (أدمن)",
        RoleType.Developer => "مبرمج",
        RoleType.Designer => "مصمم",
        RoleType.VideoEditor => "مونتير ومؤثرات",
        RoleType.ProjectManager => "مدير مشاريع",
        _ => "عضو فريق"
    };
}

public enum TaskItemStatus
{
    Pending = 1,        // قيد الانتظار
    InProgress = 2,     // جاري العمل
    UnderReview = 3,    // قيد المراجعة والتدقيق
    Completed = 4,      // مكتملة
    Blocked = 5         // معلقة / بانتظار العميل
}

public static class TaskItemStatusExtensions
{
    public static string ToArabic(this TaskItemStatus status) => status switch
    {
        TaskItemStatus.Pending => "قيد الانتظار",
        TaskItemStatus.InProgress => "جاري العمل",
        TaskItemStatus.UnderReview => "قيد المراجعة",
        TaskItemStatus.Completed => "مكتملة",
        TaskItemStatus.Blocked => "معلقة",
        _ => "غير محدد"
    };
}

public enum TaskPriority
{
    Low = 1,
    Medium = 2,
    High = 3,
    Urgent = 4
}

public static class TaskPriorityExtensions
{
    public static string ToArabic(this TaskPriority priority) => priority switch
    {
        TaskPriority.Low => "منخفضة",
        TaskPriority.Medium => "متوسطة",
        TaskPriority.High => "عالية",
        TaskPriority.Urgent => "عاجلة جداً",
        _ => "عادية"
    };
}

public enum ContractStatus
{
    Draft = 1,          // مسودة
    Active = 2,         // نشط / قيد التنفيذ
    Completed = 3,      // مكتمل ومسلم
    Cancelled = 4       // ملغي
}

public static class ContractStatusExtensions
{
    public static string ToArabic(this ContractStatus status) => status switch
    {
        ContractStatus.Draft => "مسودة",
        ContractStatus.Active => "نشط (قيد التنفيذ)",
        ContractStatus.Completed => "مكتمل ومسلم",
        ContractStatus.Cancelled => "ملغي",
        _ => "غير محدد"
    };
}

public enum PaymentMethod
{
    Cash = 1,           // كاش / نقد
    BankTransfer = 2,   // تحويل بنكي
    Bit = 3,            // تطبيق بت (Bit)
    Check = 4,          // شيك مصرفي
    CreditCard = 5      // بطاقة ائتمان / فيزا
}

public static class PaymentMethodExtensions
{
    public static string ToArabic(this PaymentMethod method) => method switch
    {
        PaymentMethod.Cash => "كاش (نقد)",
        PaymentMethod.BankTransfer => "تحويل بنكي",
        PaymentMethod.Bit => "تطبيق بت (Bit)",
        PaymentMethod.Check => "شيك مصرفي",
        PaymentMethod.CreditCard => "بطاقة ائتمان",
        _ => "أخرى"
    };
}

public enum VaultCategory
{
    SocialMedia = 1,     // فيسبوك، انستغرام، تيك توك، وغيرها
    ServerVPS = 2,       // سيرفرات واستضافات (Hetzner, AWS, cPanel)
    Database = 3,        // قواعد بيانات (SQL, Mongo, Redis)
    HostingAndDomain = 4,// نطاقات وموزعات دومينات
    ClientAccount = 5,   // حسابات خاصة بالعملاء
    Other = 6            // أدوات وحسابات أخرى
}

public static class VaultCategoryExtensions
{
    public static string ToArabic(this VaultCategory category) => category switch
    {
        VaultCategory.SocialMedia => "سوشيال ميديا",
        VaultCategory.ServerVPS => "سيرفرات واستضافات",
        VaultCategory.Database => "قواعد بيانات",
        VaultCategory.HostingAndDomain => "دومينات ونطاقات",
        VaultCategory.ClientAccount => "بوابات العملاء",
        _ => "حسابات أخرى"
    };
}

public enum ExpenseCategory
{
    TeamSalaryOrAdvance = 1, // رواتب وسلف ومستحقات الفريق
    SoftwareSubscription = 2,// اشتراكات البرامج والأدوات (Adobe, Figma)
    OfficeAndEquipment = 3,  // معدات، أجهزة، ولوازم مكتب
    Marketing = 4,           // إعلانات وتسويق
    Other = 5                // مصاريف نثرية عامة
}

public static class ExpenseCategoryExtensions
{
    public static string ToArabic(this ExpenseCategory category) => category switch
    {
        ExpenseCategory.TeamSalaryOrAdvance => "مستحقات وسلف الفريق",
        ExpenseCategory.SoftwareSubscription => "اشتراكات برامج وأدوات",
        ExpenseCategory.OfficeAndEquipment => "معدات ومصاريف مكتبية",
        ExpenseCategory.Marketing => "حملات تسويقية وإعلانات",
        _ => "مصروفات عامة"
    };
}

public enum BillingCycle
{
    Monthly = 1,
    Yearly = 2
}

public static class BillingCycleExtensions
{
    public static string ToArabic(this BillingCycle cycle) => cycle switch
    {
        BillingCycle.Monthly => "شهري",
        BillingCycle.Yearly => "سنوي",
        _ => "شهري"
    };
}

public enum CheckStatus
{
    InVault = 1,     // في الصندوق / الخزنة
    Deposited = 2,   // تم إيداعه في البنك للتحصيل
    Cleared = 3,     // تم صرفه وتحصيل المبلغ بنجاح
    Bounced = 4      // شيك راجع / مرتجع بدون رصيد
}

public static class CheckStatusExtensions
{
    public static string ToArabic(this CheckStatus status) => status switch
    {
        CheckStatus.InVault => "في الخزنة (بانتظار الاستحقاق)",
        CheckStatus.Deposited => "مودع بالبنك للتحصيل",
        CheckStatus.Cleared => "تم الصرف بنجاح",
        CheckStatus.Bounced => "شيك مرتجع",
        _ => "غير محدد"
    };
}

