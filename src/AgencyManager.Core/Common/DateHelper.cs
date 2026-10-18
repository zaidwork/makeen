namespace AgencyManager.Core.Common;

public static class DateHelper
{
    public static string FormatRelativeDays(int days)
    {
        if (days < 0)
        {
            var abs = Math.Abs(days);
            return abs switch
            {
                1 => "متأخر منذ يوم",
                2 => "متأخر منذ يومين",
                >= 3 and <= 10 => $"متأخر منذ {abs} أيام",
                _ => $"متأخر منذ {abs} يوماً"
            };
        }

        return days switch
        {
            0 => "يستحق اليوم",
            1 => "يستحق غداً",
            2 => "يستحق خلال يومين",
            >= 3 and <= 10 => $"يستحق خلال {days} أيام",
            _ => $"يستحق خلال {days} يوماً"
        };
    }

    public static string FormatRenewalDays(int days)
    {
        if (days < 0)
        {
            var abs = Math.Abs(days);
            return abs switch
            {
                1 => "متأخر منذ يوم",
                2 => "متأخر منذ يومين",
                >= 3 and <= 10 => $"متأخر منذ {abs} أيام",
                _ => $"متأخر منذ {abs} يوماً"
            };
        }

        return days switch
        {
            0 => "التجديد اليوم",
            1 => "التجديد غداً",
            2 => "خلال يومين",
            >= 3 and <= 10 => $"خلال {days} أيام",
            _ => $"خلال {days} يوماً"
        };
    }
}
