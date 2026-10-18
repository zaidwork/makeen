using System.Globalization;

namespace AgencyManager.Core.Common;

public static class CurrencyHelper
{
    public const string CurrencySymbol = "₪";
    public const string CurrencyCode = "ILS";

    public static string FormatIls(decimal amount, bool includeDecimals = false)
    {
        var formatted = includeDecimals 
            ? amount.ToString("#,##0.00", CultureInfo.InvariantCulture)
            : amount.ToString("#,##0", CultureInfo.InvariantCulture);

        return $"{formatted} {CurrencySymbol}";
    }

    public static string FormatIls(double amount, bool includeDecimals = false)
    {
        return FormatIls((decimal)amount, includeDecimals);
    }
}
