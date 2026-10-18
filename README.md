# 🏢 مكين | Makeen Agency OS
### نظام الإدارة المتكامل والذكي لوكالات البرمجة والتصميم والإنتاج الرقمي
**The Ultimate All-in-One Agency Management Suite (.NET 10 + Blazor)**

<p align="center">
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 10" />
  <img src="https://img.shields.io/badge/Blazor-Hybrid_%26_Web-512BD4?style=for-the-badge&logo=blazor&logoColor=white" alt="Blazor" />
  <img src="https://img.shields.io/badge/Architecture-Clean_Architecture-239120?style=for-the-badge" alt="Clean Architecture" />
  <img src="https://img.shields.io/badge/Database-SQLite_%2F_Supabase-003B57?style=for-the-badge&logo=sqlite&logoColor=white" alt="Database" />
  <img src="https://img.shields.io/badge/Platform-Windows_%26_Web-0078D6?style=for-the-badge&logo=windows&logoColor=white" alt="Platform" />
  <img src="https://img.shields.io/badge/Language-Arabic_RTL_%26_EN-gold?style=for-the-badge" alt="Language" />
</p>

---

## 📖 نبذة عن المشروع (About The Project)

**مكين (Makeen)** هو نظام إداري ومالي متكامل فائق السرعة، مبني بأحدث تقنيات **مايكروسوفت (.NET 10 & Blazor)**، ومصمم خصيصاً لتلبية احتياجات **وكالات البرمجة، استوديوهات التصميم، وشركات الإنتاج والميديا**.

يجمع النظام بين قوتين:
1. **🖥️ تطبيق سطح المكتب (Desktop App - WPF/Blazor Hybrid):** مخصص للإدارة وأصحاب القرار، يعمل محلياً بأعلى أداء وسرعة فائقة مع إمكانية الطباعة الفورية وإدارة الخزينة والنسخ الاحتياطي.
2. **🌐 منصة الويب (Web App - Blazor Server):** مخصصة لأعضاء الفريق (المبرمجين، المصممين، والمونتيرين) لتسجيل ساعات العمل وإنجاز المهام مع عزل كامل لصلاحيات كل موظف.

> **💡 العملة الأساسية:** يدعم النظام حسابات دقيقة بـ **الشيكل (₪ - ILS)** والدولار، مع توافق كامل للغة العربية والاتجاه من اليمين لليسار (**RTL**).

---

## ✨ أبرز الميزات القوية (Key Features)

### 1. 🏛️ محفظة الشيكات المصرفية المؤجلة (Post-Dated Checks)
- إدارة ذكية لشيكات العملاء والشركات (رقم الشيك، البنك، الفرع، تاريخ الاستحقاق، المبلغ بالشيكل ₪).
- تتبع دقيق للحالات: *(في الخزنة / تم إيداعه / تم الصرف / شيك راجع)*.
- **تنبيهات فورية:** إشعار بألوان تحذيرية للشيكات المستحقة خلال 3 أيام.
- **قيد تلقائي:** عند اختيار "تم الصرف"، يتم تحويل قيمة الشيك فورياً كدفعة معتمدة في حساب العميل.

### 2. 💰 محرك حساب صافي ربح المشاريع (Project Profitability Engine)
- حساب حقيقي للربحية: `صافي الربح = قيمة العقد - (ساعات العمل المسجلة × أجر ساعة الموظف)`.
- تقارير ذكية لنسبة هامش الربح (**Profit Margin %**) لكشف المشاريع الأكثر ربحاً وتلك المستنزفة للموارد.

### 3. ⏱️ عداد الوقت الحي للمهام (Live Task Stopwatch)
- زر مباشر **"▶️ ابدأ العمل"** و **"⏸️ إيقاف"** داخل بطاقة كل مهمة.
- احتساب آلي وتراكمي للوقت المستغرق مع عكس التكلفة فوراً على ميزانية المشروع.

### 4. 🔄 حماية جولات التعديل وتفادي الاستنزاف (Revision Trap Shield)
- تحديد سقف جولات التعديل المجانية المتفق عليها مع العميل (مثلاً: جولة 1/2، 2/2).
- إشعار نظام حاسم عند تجاوز الحد المجاني: `⚠️ تجاوز الحد المجاني (مطلوب أمر تغيير مدفوع)` لضمان حقوق الوكالة.

### 5. 📲 التواصل الفوري والواتساب بضغطة زر (WhatsApp 1-Click)
- تجهيز فواتير وسندات القبض وإرسالها مباشرة لرقم واتساب العميل بنقرة زر واحدة.
- مشاركة ملخص إنجاز المشروع دورياً مع العميل دون الحاجة لكتابة رسائل يدوية.

### 6. 🌐 بوابة متابعة العميل (Client Portal)
- شاشة نظيفة ومهيأة للعملاء تعرض نسبة الإنجاز والمراحل المكتملة والدفعات المتبقية، مع حجب كامل للتفاصيل الداخلية وأسماء الموظفين وهوامش الربح.

### 7. 🔐 خزنة السيرفرات وبيانات الاعتماد (Credentials Vault)
- تخزين مشفر بتقنية **AES-256** لبيانات السيرفرات (SSH/FTP/cPanel)، وقواعد البيانات، وحسابات الإعلانات، مع خاصية النسخ السريع بضغطة زر واحدة.

### 8. 💾 النسخ الاحتياطي الآمن (Database Backup)
- زر مدمج في الواجهة لأخذ نسخة احتياطية لحظية ومؤرخة لقاعدة البيانات وحفظها في مجلد `backups/`.

---

## 🛠️ البنية التقنية (Tech Stack)

| المكون (Layer) | التقنية المستخدمة (Technology) | التفاصيل (Details) |
| :--- | :--- | :--- |
| **Framework** | **.NET 10.0** | أحدث إصدار من بيئة دوت نت |
| **Frontend UI** | **Blazor Components & Razor** | واجهات موحدة وقابلة لإعادة الاستخدام |
| **Desktop App** | **WPF + Blazor Hybrid** | تطبيق سطح مكتب فائق السرعة وخفيف الوزن |
| **Web App** | **ASP.NET Core Blazor Web** | منصة سحابية تفاعلية للفريق |
| **Architecture** | **Clean Architecture** | هيكلية نظيفة تعزل المنطق عن الواجهات |
| **ORM** | **Entity Framework Core 10** | وصول عالي الأداء للبيانات |
| **Database** | **SQLite (Local) / Supabase (Cloud)** | تدعم الترحيل الفوري للسحابة بسهولة |

---

## 📁 هيكلية المشروع (Project Architecture)

```text
mange/
├── AgencyManager.slnx                # ملف الحل الرئيسي (Solution)
├── تشغيل_الديسك_توب.bat              # تشغيل سريع لتطبيق سطح المكتب
├── تشغيل_موقع_الويب.bat              # تشغيل سريع لمنصة الويب
└── src/
    ├── AgencyManager.Core/           # الكيانات (Entities)، العقود، ونماذج البيانات
    ├── AgencyManager.Infrastructure/ # قاعدة البيانات (DbContext)، التشفير، والمستودعات
    ├── AgencyManager.Shared/         # واجهات Blazor المشتركة، التصميم، والمكونات
    ├── AgencyManager.Desktop/        # تطبيق الديسك توب (WPF / Windows Native)
    └── AgencyManager.Web/            # موقع الويب (ASP.NET Core Web Server)
```

---

## 🚀 التشغيل السريع (Quick Start)

### الطريقة الأولى: التشغيل بضغطة زر (Windows Quick Launch)
- **لتشغيل تطبيق الديسك توب (Desktop App):**  
  اضغط نقرتين على: `تشغيل_الديسك_توب.bat`
- **لتشغيل منصة الويب (Web App):**  
  اضغط نقرتين على: `تشغيل_موقع_الويب.bat`  
  (ستفتح المنصة تلقائياً على المتصفح: `http://localhost:5080`)

### الطريقة الثانية: عبر سطر الأوامر (.NET CLI)

1. **تشغيل تطبيق سطح المكتب:**
   ```bash
   dotnet run --project src/AgencyManager.Desktop
   ```

2. **تشغيل تطبيق الويب:**
   ```bash
   dotnet run --project src/AgencyManager.Web
   ```

---

## ☁️ الربط السحابي مع Supabase (Cloud Migration)

تم تصميم قاعدة البيانات عبر **Entity Framework Core** بعناية لتسهيل الانتقال لقاعدة بيانات سحابية (**PostgreSQL / Supabase**) بدقائق:
1. أنشئ مشروعاً جديداً في منصة **[Supabase](https://supabase.com)**.
2. احصل على رمز الاتصال (`Connection String`) من إعدادات قاعدة البيانات.
3. استبدل `UseSqlite` بسطر `UseNpgsql(connectionString)` في إعدادات التطبيق.
4. نفّذ الترحيل بضغطة زر لتعمل جميع المنصات على قاعدة بيانات مركزية وموحدة!

---

## 👨‍💻 المطور والمساهمة (Author & License)

- **المطور:** Zaid Daraghmeh ([@zaidwork](https://github.com/zaidwork))
- **البريد الإلكتروني:** zaidbashardaraghmeh@gmail.com
- **الترخيص:** مفتوح المصدر للاستخدام والتطوير (MIT License).
