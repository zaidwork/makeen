using AgencyManager.Core.Enums;

namespace AgencyManager.Core.Entities;

public class VaultCredential
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty; // عنوان الحساب (مثلاً: سيرفر الزبون X، إنستغرام الوكالة)
    public VaultCategory Category { get; set; } = VaultCategory.SocialMedia;

    public string UrlOrHost { get; set; } = string.Empty; // رابط اللوحة أو الـ IP
    public string UsernameOrEmail { get; set; } = string.Empty; // اسم المستخدم أو الإيميل
    public string EncryptedPassword { get; set; } = string.Empty; // كلمة المرور المشفرة بـ AES-256

    public string PortOrExtraInfo { get; set; } = string.Empty; // منفذ الاتصال، قاعدة البيانات الافتراضية
    public string Notes { get; set; } = string.Empty; // ملاحظات أمان إضافية (مفاتيح 2FA احتياطية)

    public Guid? ClientId { get; set; }
    public Client? Client { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
}
