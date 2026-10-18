using System.Net;
using AgencyManager.Core.Common;
using AgencyManager.Core.Entities;
using AgencyManager.Core.Enums;

namespace AgencyManager.Infrastructure.Services;

public static class WhatsAppService
{
    private static string SanitizePhone(string phone)
    {
        if (string.IsNullOrEmpty(phone)) return string.Empty;
        var digits = new string(phone.Where(char.IsDigit).ToArray());

        // تحويل أرقام فلسطين والمحلية لصيغة دولية مقبولة لواتساب
        if (digits.StartsWith("05"))
        {
            digits = "970" + digits.Substring(1); // أو 972 حسب مشغل العميل
        }
        else if (digits.StartsWith("5"))
        {
            digits = "970" + digits;
        }

        return digits;
    }

    public static string CreateWhatsAppUrl(string phone, string message)
    {
        var cleanPhone = SanitizePhone(phone);
        var encodedMsg = WebUtility.UrlEncode(message);
        return $"https://api.whatsapp.com/send?phone={cleanPhone}&text={encodedMsg}";
    }

    public static string GeneratePaymentReceiptMessage(Client client, PaymentRecord payment)
    {
        var clientName = string.IsNullOrEmpty(client.FullName) ? "العميل المحترم" : client.FullName;
        return $@"مرحباً {clientName}
تحية طيبة من إدارة الوكالة

تم بنجاح قيد وتأكيد استلام دفعتكم الكريمة:
- المبلغ المستلم: {CurrencyHelper.FormatIls(payment.Amount)}
- طريقة الدفع: {payment.Method.ToArabic()}
- التاريخ: {payment.PaymentDate:yyyy/MM/dd}
- الرقم المرجعي: {(string.IsNullOrEmpty(payment.ReferenceNumber) ? "سند نقدي" : payment.ReferenceNumber)}

الرصيد المتبقي على الحساب: {CurrencyHelper.FormatIls(client.BalanceDue)}

شاكرين تعاونكم وثقتكم بنا.";
    }

    public static string GenerateContractApprovedMessage(Client client, Contract contract)
    {
        var clientName = string.IsNullOrEmpty(client.FullName) ? "العميل المحترم" : client.FullName;
        return $@"أهلاً وسهلاً بحضرتك {clientName}
يسعدنا إعلامك ببدء تنفيذ مشروعكم الرسمي:
- المشروع: {contract.Title}
- رقم العقد: {contract.ContractNumber}
- موعد التسليم المتوقع: {contract.DeliveryDeadline:yyyy/MM/dd}

يمكنك متابعة سير إنجاز العمل خطوة بخطوة عبر الرابط المباشر لمشروعكم.
نتشرف بالعمل معكم وتقديم أفضل جودة تليق بشركتكم.";
    }
}
