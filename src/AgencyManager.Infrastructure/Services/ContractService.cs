using System.Text;
using AgencyManager.Core.Common;
using AgencyManager.Core.Entities;
using AgencyManager.Core.Enums;
using AgencyManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgencyManager.Infrastructure.Services;

public class ContractService
{
    private readonly AppDbContext _context;

    public ContractService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// تفكيك بنود العقد تلقائياً إلى تاسكات مخصصة لكل تخصص في الفريق (مبرمج، مصمم، مونتير)
    /// </summary>
    public async Task<int> DecomposeContractToTasksAsync(Guid contractId)
    {
        var contract = await _context.Contracts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == contractId);

        if (contract == null || contract.Items.Count == 0) return 0;

        var teamMembers = await _context.TeamMembers.Where(m => m.IsActive).ToListAsync();
        int tasksCreated = 0;

        foreach (var item in contract.Items.Where(i => !i.IsConvertedToTask))
        {
            // إيجاد العضو المناسب حسب التخصص
            var targetMember = teamMembers.FirstOrDefault(m => m.Role == item.TargetRole) 
                               ?? teamMembers.FirstOrDefault(m => m.Role != RoleType.Admin) 
                               ?? teamMembers.First();

            var task = new TaskItem
            {
                Title = $"[{contract.Title}] {item.Title}",
                Description = item.Description,
                ContractId = contract.Id,
                AssignedToUserId = item.AssignedToUserId ?? targetMember.Id,
                Deadline = contract.DeliveryDeadline,
                EstimatedHours = item.EstimatedHours,
                ActualHoursSpent = 0,
                ProgressPercentage = 0,
                Status = TaskItemStatus.Pending,
                Priority = TaskPriority.High,
                DeliverableLinkOrNotes = "مهمة تم توليدها تلقائياً من بنود العقد الرسمي"
            };

            item.IsConvertedToTask = true;
            item.AssignedToUserId = task.AssignedToUserId;
            _context.TaskItems.Add(task);
            tasksCreated++;
        }

        await _context.SaveChangesAsync();
        return tasksCreated;
    }

    /// <summary>
    /// توليد صفحة عقد رسمي قابلة للطباعة الفورية على الورق قياس A4
    /// </summary>
    public string GenerateContractHtml(Contract contract)
    {
        var clientName = contract.Client?.FullName ?? "العميل المحترم";
        var companyName = contract.Client?.CompanyName ?? "الشركة الشريكة";
        var phone = contract.Client?.Phone ?? "-";

        var sb = new StringBuilder();
        sb.Append($@"
<!DOCTYPE html>
<html lang=""ar"" dir=""rtl"">
<head>
    <meta charset=""UTF-8"">
    <title>عقد اتفاق وتقديم خدمات - {contract.ContractNumber}</title>
    <style>
        @page {{ size: A4; margin: 15mm; }}
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: #111827;
            background: #ffffff;
            margin: 0;
            padding: 20px;
            direction: rtl;
        }}
        .header {{
            display: flex;
            justify-content: space-between;
            align-items: center;
            border-bottom: 3px solid #2563EB;
            padding-bottom: 15px;
            margin-bottom: 25px;
        }}
        .logo-area h1 {{
            margin: 0;
            color: #2563EB;
            font-size: 24px;
        }}
        .contract-meta {{
            text-align: left;
            font-size: 14px;
            color: #4B5563;
        }}
        .contract-meta span {{
            font-weight: bold;
            color: #111827;
        }}
        .title-banner {{
            background: #F3F4F6;
            padding: 12px 18px;
            border-radius: 6px;
            border-right: 5px solid #F59E0B;
            margin-bottom: 25px;
        }}
        .title-banner h2 {{
            margin: 0;
            font-size: 18px;
            color: #1F2937;
        }}
        .parties-box {{
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 20px;
            margin-bottom: 25px;
        }}
        .party-card {{
            border: 1px solid #E5E7EB;
            padding: 15px;
            border-radius: 6px;
            background: #F9FAFB;
        }}
        .party-card h3 {{
            margin-top: 0;
            color: #2563EB;
            font-size: 15px;
            border-bottom: 1px solid #E5E7EB;
            padding-bottom: 8px;
        }}
        table {{
            width: 100%;
            border-collapse: collapse;
            margin: 20px 0;
        }}
        th, td {{
            border: 1px solid #E5E7EB;
            padding: 10px 14px;
            text-align: right;
            font-size: 14px;
        }}
        th {{
            background-color: #F8FAFC;
            color: #374151;
            font-weight: 600;
        }}
        .total-box {{
            margin-top: 15px;
            text-align: left;
            font-size: 16px;
        }}
        .total-box .amount {{
            font-size: 22px;
            font-weight: bold;
            color: #2563EB;
        }}
        .badge {{
            display: inline-block;
            padding: 3px 8px;
            border-radius: 4px;
            font-size: 12px;
            font-weight: bold;
            background: #FEF3C7;
            color: #92400E;
        }}
        .terms-section {{
            margin-top: 25px;
            font-size: 13px;
            color: #4B5563;
            line-height: 1.8;
            white-space: pre-line;
            border-top: 1px solid #E5E7EB;
            padding-top: 15px;
        }}
        .signature-section {{
            margin-top: 40px;
            display: flex;
            justify-content: space-between;
            page-break-inside: avoid;
        }}
        .sig-box {{
            width: 45%;
            border: 1px dashed #9CA3AF;
            border-radius: 6px;
            padding: 20px;
            text-align: center;
        }}
        .sig-space {{
            height: 70px;
        }}
        @media print {{
            body {{ padding: 0; }}
            .no-print {{ display: none; }}
        }}
    </style>
</head>
<body>
    <div class=""header"">
        <div class=""logo-area"">
            <h1>وكالة الإبداع والحلول الرقمية</h1>
            <p style=""margin: 3px 0 0 0; color: #6B7280; font-size: 13px;"">Creative Agency & Tech Systems</p>
        </div>
        <div class=""contract-meta"">
            <div>رقم العقد: <span>{contract.ContractNumber}</span></div>
            <div>تاريخ التحرير: <span>{contract.StartDate:yyyy/MM/dd}</span></div>
            <div>موعد التسليم: <span>{contract.DeliveryDeadline:yyyy/MM/dd}</span></div>
        </div>
    </div>

    <div class=""title-banner"">
        <h2>{contract.Title}</h2>
    </div>

    <div class=""parties-box"">
        <div class=""party-card"">
            <h3>الطرف الأول (مقدم الخدمة)</h3>
            <p><strong>وكالة الإبداع التقني</strong></p>
            <p>سجل تجاري معتمد</p>
            <p>هاتف الدعم: 059-9001122</p>
        </div>
        <div class=""party-card"">
            <h3>الطرف الثاني (العميل)</h3>
            <p><strong>{clientName}</strong> - {companyName}</p>
            <p>هاتف العميل: {phone}</p>
            <p>عنوان المشروع: {contract.Client?.Address ?? "فلسطين"}</p>
        </div>
    </div>

    <h3 style=""color: #1F2937; margin-bottom: 10px;"">بنود ونطاق المشروع المتفق عليه:</h3>
    <table>
        <thead>
            <tr>
                <th style=""width: 50px;"">#</th>
                <th>البند والمخرج المطلوب</th>
                <th style=""width: 140px;"">التخصص</th>
                <th style=""width: 150px; text-align: left;"">القيمة بالشيكل</th>
            </tr>
        </thead>
        <tbody>");

        int index = 1;
        foreach (var item in contract.Items)
        {
            sb.Append($@"
            <tr>
                <td>{index++}</td>
                <td>
                    <strong>{item.Title}</strong>
                    <div style=""color: #6B7280; font-size: 12px; margin-top: 4px;"">{item.Description}</div>
                </td>
                <td><span class=""badge"">{item.TargetRole.ToArabic()}</span></td>
                <td style=""text-align: left; font-weight: bold;"">{CurrencyHelper.FormatIls(item.Price)}</td>
            </tr>");
        }

        sb.Append($@"
        </tbody>
    </table>

    <div class=""total-box"">
        <div>إجمالي قيمة العقد: <span class=""amount"">{CurrencyHelper.FormatIls(contract.TotalAmount)}</span></div>
        <div style=""font-size: 14px; color: #4B5563; margin-top: 4px;"">
            المدفوع: {CurrencyHelper.FormatIls(contract.TotalPaid)} | المتبقي للاستحقاق: <strong style=""color: #D97706;"">{CurrencyHelper.FormatIls(contract.RemainingBalance)}</strong>
        </div>
    </div>

    <div class=""terms-section"">
        <h4 style=""margin: 0 0 8px 0; color: #111827;"">الشروط والأحكام المتفق عليها:</h4>
        {contract.TermsAndConditions}
    </div>

    <div class=""signature-section"">
        <div class=""sig-box"">
            <strong>توقيع وختم الطرف الأول</strong>
            <div class=""sig-space""></div>
            <span style=""font-size: 12px; color: #6B7280;"">وكالة الإبداع التقني</span>
        </div>
        <div class=""sig-box"">
            <strong>توقيع الطرف الثاني (العميل)</strong>
            <div class=""sig-space""></div>
            <span style=""font-size: 12px; color: #6B7280;"">{clientName}</span>
        </div>
    </div>
</body>
</html>");

        return sb.ToString();
    }
}
