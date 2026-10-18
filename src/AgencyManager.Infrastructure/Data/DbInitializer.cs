using AgencyManager.Core.Entities;
using AgencyManager.Core.Enums;
using AgencyManager.Core.Interfaces;

namespace AgencyManager.Infrastructure.Data;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context, ISecurityService securityService)
    {
        context.Database.EnsureCreated();

        if (context.TeamMembers.Any())
        {
            return; // قاعدة البيانات ممتلئة مسبقاً
        }

        // 1. إضافة فريق العمل
        var admin = new TeamMember
        {
            FullName = "المدير العام (المالك)",
            Username = "admin",
            PasswordHash = securityService.HashPassword("admin123"),
            Role = RoleType.Admin,
            Phone = "059-9001122",
            Email = "owner@agency.com",
            MonthlyRateOrSalary = 15000, // ₪
            IsActive = true
        };

        var dev = new TeamMember
        {
            FullName = "أحمد مصطفى",
            Username = "ahmad_dev",
            PasswordHash = securityService.HashPassword("123456"),
            Role = RoleType.Developer,
            Phone = "059-8123456",
            Email = "ahmad@agency.com",
            MonthlyRateOrSalary = 7500, // ₪
            IsActive = true
        };

        var designer = new TeamMember
        {
            FullName = "سارة التميمي",
            Username = "sara_ui",
            PasswordHash = securityService.HashPassword("123456"),
            Role = RoleType.Designer,
            Phone = "056-9876543",
            Email = "sara@agency.com",
            MonthlyRateOrSalary = 6000, // ₪
            IsActive = true
        };

        var editor = new TeamMember
        {
            FullName = "عمر الخالدي",
            Username = "omar_motion",
            PasswordHash = securityService.HashPassword("123456"),
            Role = RoleType.VideoEditor,
            Phone = "059-5544332",
            Email = "omar@agency.com",
            MonthlyRateOrSalary = 5500, // ₪
            IsActive = true
        };

        context.TeamMembers.AddRange(admin, dev, designer, editor);
        context.SaveChanges();

        // 2. إضافة العملاء
        var client1 = new Client
        {
            FullName = "م. خالد عبد الرحمن",
            CompanyName = "شركة الأفق للاستيراد والتصدير",
            Phone = "050-1234567",
            Email = "khaled@alofok.com",
            Address = "رام الله - الماصيون",
            Notes = "عميل مميز - يفضل الدفع عبر تطبيق Bit أو تحويل بنكي"
        };

        var client2 = new Client
        {
            FullName = "د. ليث النتشة",
            CompanyName = "مجمع رعاية الطبي",
            Phone = "052-9871122",
            Email = "dr.laith@reaya.ps",
            Address = "القدس - بيت حنينا",
            Notes = "حملات إعلانات شهرية وهوية بصرية"
        };

        var client3 = new Client
        {
            FullName = "السيد طارق عساف",
            CompanyName = "سلسلة مطاعم جريل هاوس",
            Phone = "054-3322114",
            Email = "tariq@grillhouse.com",
            Address = "نابلس - رفيديا",
            Notes = "تصوير فيديوهات ريلز وموقع حجز وجبات"
        };

        context.Clients.AddRange(client1, client2, client3);
        context.SaveChanges();

        // 3. إضافة العقود
        var contract1 = new Contract
        {
            ContractNumber = "CNT-2026-001",
            Title = "برمجة متجر إلكتروني وتطبيق ولاء لشركة الأفق",
            ClientId = client1.Id,
            TotalAmount = 24000, // ₪
            StartDate = DateTime.Now.AddDays(-15),
            DeliveryDeadline = DateTime.Now.AddDays(25),
            Status = ContractStatus.Active,
            ProjectScope = "تطوير متجر C# Blazor وتطبيقات موبايل مع لوحة تحكم سريعة وربط بوابات دفع محلية.",
            TermsAndConditions = "1. الدفعة الأولى 50% عند التوقيع، 25% بعد النماذج الأولية، 25% عند التسليم النهائي.\n2. الصيانة مجانية لمدة 3 شهور بعد الإطلاق."
        };

        var contract2 = new Contract
        {
            ContractNumber = "CNT-2026-002",
            Title = "هوية بصرية كاملة وتصوير ريلز لمجمع رعاية الطبي",
            ClientId = client2.Id,
            TotalAmount = 14500, // ₪
            StartDate = DateTime.Now.AddDays(-5),
            DeliveryDeadline = DateTime.Now.AddDays(15),
            Status = ContractStatus.Active,
            ProjectScope = "شعار، ألوان، قوالب سوشيال ميديا، وتصوير 12 فيديو ريلز طبي احترافي.",
            TermsAndConditions = "1. دفعة أولى 60% مقدماً، والباقي عند تسليم الدليل الإعلاني.\n2. التعديلات مسموحة حتى جولتين لكل تصميم."
        };

        context.Contracts.AddRange(contract1, contract2);
        context.SaveChanges();

        // 4. بنود العقود وتفكيكها لتاسكات
        var c1_item1 = new ContractItem
        {
            ContractId = contract1.Id,
            Title = "تصميم واجهات المتجر على فيجما (UI/UX)",
            Description = "تصميم صفحات الهوم، الكتالوج، وسلة الشراء مع النسخة المتجاوبة للموبايل",
            Price = 6000, // ₪
            TargetRole = RoleType.Designer,
            EstimatedHours = 25,
            IsConvertedToTask = true,
            AssignedToUserId = designer.Id
        };

        var c1_item2 = new ContractItem
        {
            ContractId = contract1.Id,
            Title = "برمجة لوحة التحكم والـ API وقاعدة البيانات",
            Description = "بناء خدمات EF Core وإدارة المنتجات والطلبات وربط فواتير الشيكل",
            Price = 12000, // ₪
            TargetRole = RoleType.Developer,
            EstimatedHours = 45,
            IsConvertedToTask = true,
            AssignedToUserId = dev.Id
        };

        var c2_item1 = new ContractItem
        {
            ContractId = contract2.Id,
            Title = "تصميم الشعار وأدلة الهوية البصرية",
            Description = "الشعار بالألوان والأبيض والأسود مع الأيقونات والخطوط الرسمية",
            Price = 5500, // ₪
            TargetRole = RoleType.Designer,
            EstimatedHours = 20,
            IsConvertedToTask = true,
            AssignedToUserId = designer.Id
        };

        var c2_item2 = new ContractItem
        {
            ContractId = contract2.Id,
            Title = "مونتاج وتعديل ألوان 12 ريلز طبي",
            Description = "تقطيع المشاهد، تركيب المؤثرات الصوتية، الكابشنز، وتعديل الإضاءة",
            Price = 9000, // ₪
            TargetRole = RoleType.VideoEditor,
            EstimatedHours = 35,
            IsConvertedToTask = true,
            AssignedToUserId = editor.Id
        };

        context.ContractItems.AddRange(c1_item1, c1_item2, c2_item1, c2_item2);
        context.SaveChanges();

        // 5. المهام الموزعة على الشباب (Tasks)
        var taskDev = new TaskItem
        {
            Title = "برمجة سلة المشتريات ومحرك الفواتير بالشيكل ₪",
            Description = "تطوير لوحة الكاشير وحساب الخصومات وحساب الضريبة والمجموع النهائي",
            ContractId = contract1.Id,
            AssignedToUserId = dev.Id,
            Deadline = DateTime.Now.AddDays(4),
            EstimatedHours = 18,
            ActualHoursSpent = 12,
            ProgressPercentage = 70,
            Status = TaskItemStatus.InProgress,
            Priority = TaskPriority.High,
            DeliverableLinkOrNotes = "فرع git: feature/cart-checkout"
        };

        var taskDev2 = new TaskItem
        {
            Title = "ربط بوابة التحقق عبر SMS وتأكيد الطلبات",
            Description = "إرسال كود OTP عبر الـ API المحلي للتأكد من هاتف الزبون",
            ContractId = contract1.Id,
            AssignedToUserId = dev.Id,
            Deadline = DateTime.Now.AddDays(8),
            EstimatedHours = 10,
            ActualHoursSpent = 0,
            ProgressPercentage = 0,
            Status = TaskItemStatus.Pending,
            Priority = TaskPriority.Medium
        };

        var taskDesign = new TaskItem
        {
            Title = "تسليم نماذج صفحات الهبوط لشركة الأفق على فيجما",
            Description = "التصميم جاهز وبانتظار جلسة المراجعة مع العميل يوم الخميس",
            ContractId = contract1.Id,
            AssignedToUserId = designer.Id,
            Deadline = DateTime.Now.AddDays(2),
            EstimatedHours = 15,
            ActualHoursSpent = 14,
            ProgressPercentage = 90,
            Status = TaskItemStatus.UnderReview,
            Priority = TaskPriority.Urgent,
            DeliverableLinkOrNotes = "figma.com/file/alofok-store-v2"
        };

        var taskVideo = new TaskItem
        {
            Title = "مونتاج أول 4 فيديوهات ريلز مجمع رعاية الطبي",
            Description = "إضافة المؤثرات البصرية وتنسيق التكبير والتصغير والموسيقى الطبية الهادئة",
            ContractId = contract2.Id,
            AssignedToUserId = editor.Id,
            Deadline = DateTime.Now.AddDays(3),
            EstimatedHours = 12,
            ActualHoursSpent = 8,
            ProgressPercentage = 60,
            Status = TaskItemStatus.InProgress,
            Priority = TaskPriority.High,
            DeliverableLinkOrNotes = "drive.google.com/reaya-reels-draft1"
        };

        context.TaskItems.AddRange(taskDev, taskDev2, taskDesign, taskVideo);

        // 6. دفعات وسندات قبض العملاء
        var payment1 = new PaymentRecord
        {
            ClientId = client1.Id,
            ContractId = contract1.Id,
            Amount = 12000, // ₪
            PaymentDate = DateTime.Now.AddDays(-14),
            Method = PaymentMethod.BankTransfer,
            ReferenceNumber = "TRF-99214-PAL",
            Notes = "الدفعة الأولى 50% عند بدء المشروع",
            IsConfirmed = true
        };

        var payment2 = new PaymentRecord
        {
            ClientId = client2.Id,
            ContractId = contract2.Id,
            Amount = 8500, // ₪
            PaymentDate = DateTime.Now.AddDays(-4),
            Method = PaymentMethod.Bit,
            ReferenceNumber = "BIT-223401",
            Notes = "دفعة مقدمة عبر تطبيق Bit",
            IsConfirmed = true
        };

        context.PaymentRecords.AddRange(payment1, payment2);

        // 7. خزنة الحسابات والسيرفرات المشفرة
        var vault1 = new VaultCredential
        {
            Title = "سيرفر الأفق الرئيسي (Hetzner Cloud VPS)",
            Category = VaultCategory.ServerVPS,
            UrlOrHost = "188.40.120.45:22",
            UsernameOrEmail = "root",
            EncryptedPassword = securityService.EncryptSecret("Vp$!Alofok2026Secure"),
            Notes = "يحتوي على قاعدة بيانات PostgreSQL وموقع الإنتاج",
            ClientId = client1.Id
        };

        var vault2 = new VaultCredential
        {
            Title = "مدير إعلانات فيسبوك (مجمع رعاية الطبي)",
            Category = VaultCategory.SocialMedia,
            UrlOrHost = "business.facebook.com",
            UsernameOrEmail = "ads@reaya.ps",
            EncryptedPassword = securityService.EncryptSecret("Reaya@Ads#FB2026"),
            Notes = "مرتبط ببطاقة الدفع الممولة من طرف العميل",
            ClientId = client2.Id
        };

        var vault3 = new VaultCredential
        {
            Title = "حساب مزود الدومينات والـ DNS (Cloudflare)",
            Category = VaultCategory.HostingAndDomain,
            UrlOrHost = "dash.cloudflare.com",
            UsernameOrEmail = "domains@agency.com",
            EncryptedPassword = securityService.EncryptSecret("Cloud#AgencyMaster99!"),
            Notes = "الحساب الموحد لجميع نطاقات العملاء"
        };

        context.VaultCredentials.AddRange(vault1, vault2, vault3);

        // 8. اشتراكات الأدوات والبرمجيات
        var subAdobe = new SubscriptionItem
        {
            ServiceName = "Adobe Creative Cloud All Apps",
            Cost = 310, // ₪
            Cycle = BillingCycle.Monthly,
            NextRenewalDate = DateTime.Now.AddDays(2), // تنبيه عاجل
            PaymentCardOrMethod = "فيزا البنك الإسلامي",
            AlertDaysBefore = 3,
            Notes = "حساب الفريق للمصممين والمونتيرية"
        };

        var subFigma = new SubscriptionItem
        {
            ServiceName = "Figma Professional Team",
            Cost = 165, // ₪
            Cycle = BillingCycle.Monthly,
            NextRenewalDate = DateTime.Now.AddDays(11),
            PaymentCardOrMethod = "بطاقة الشركة",
            AlertDaysBefore = 3,
            Notes = "مساحة العمل المشتركة للمصممة سارة"
        };

        var subVPS = new SubscriptionItem
        {
            ServiceName = "Hetzner Dedicated Server",
            Cost = 280, // ₪
            Cycle = BillingCycle.Monthly,
            NextRenewalDate = DateTime.Now.AddDays(18),
            PaymentCardOrMethod = "بطاقة الشركة",
            AlertDaysBefore = 4,
            Notes = "سيرفر البناء والاختبار والمواقع التجريبية"
        };

        context.SubscriptionItems.AddRange(subAdobe, subFigma, subVPS);

        // 9. مصروفات الشركة وسلف الشباب
        var exp1 = new CompanyExpense
        {
            Title = "سلفة على حساب الراتب - أحمد مصطفى",
            Amount = 1000, // ₪
            Category = ExpenseCategory.TeamSalaryOrAdvance,
            TeamMemberId = dev.Id,
            ExpenseDate = DateTime.Now.AddDays(-7),
            PaymentMethod = PaymentMethod.Cash,
            ReceiptReferenceOrNote = "تم التسليم كاش بمكتب الشركة"
        };

        var exp2 = new CompanyExpense
        {
            Title = "شراء هارد SSD خارجي لوحدة المونتاج 2TB",
            Amount = 450, // ₪
            Category = ExpenseCategory.OfficeAndEquipment,
            TeamMemberId = editor.Id,
            ExpenseDate = DateTime.Now.AddDays(-3),
            PaymentMethod = PaymentMethod.Cash,
            ReceiptReferenceOrNote = "فاتورة ضريبية من شركة النظم"
        };

        context.CompanyExpenses.AddRange(exp1, exp2);

        // 10. شيكات مصرفية مؤجلة (حافظة الشيكات)
        var check1 = new BankCheck
        {
            ClientId = client1.Id,
            ContractId = contract1.Id,
            CheckNumber = "CHQ-008129",
            BankName = "بنك فلسطين",
            BranchName = "فرع الماصيون - رام الله",
            DrawerName = "شركة الأفق للتجارة العامة",
            Amount = 6000, // ₪
            ReceivedDate = DateTime.Now.AddDays(-10),
            DueDate = DateTime.Now.AddDays(15), // يستحق بعد 15 يوم
            Status = CheckStatus.InVault,
            Notes = "الدفعة الثانية لمشروع المتجر الإلكتروني"
        };

        var check2 = new BankCheck
        {
            ClientId = client2.Id,
            ContractId = contract2.Id,
            CheckNumber = "CHQ-099411",
            BankName = "البنك العربي",
            BranchName = "فرع العيزرية - القدس",
            DrawerName = "د. ليث النتشة",
            Amount = 3500, // ₪
            ReceivedDate = DateTime.Now.AddDays(-20),
            DueDate = DateTime.Now.AddDays(2), // تنبيه عاجل يستحق بعد يومين!
            Status = CheckStatus.InVault,
            Notes = "شيك مستحق للصرف قريباً - ريلز وهوية"
        };

        var check3 = new BankCheck
        {
            ClientId = client3.Id,
            CheckNumber = "CHQ-044230",
            BankName = "بنك القدس",
            BranchName = "فرع رفيديا - نابلس",
            DrawerName = "سلسلة مطاعم جريل هاوس",
            Amount = 5000, // ₪
            ReceivedDate = DateTime.Now.AddDays(-5),
            DueDate = DateTime.Now.AddDays(28),
            Status = CheckStatus.InVault,
            Notes = "دفعة شيك مؤجل لحملة تصوير الفيديوهات"
        };

        context.BankChecks.AddRange(check1, check2, check3);

        context.SaveChanges();
    }
}
