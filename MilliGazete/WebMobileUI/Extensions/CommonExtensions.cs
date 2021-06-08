using Core.Utilities.Results;
using System;
using WebMobileUI.Models;

public static class CommonExtensions
{
    public static bool DataResultIsNotNull<T>(this IDataResult<T> result) where T : class
    {
        return result != null && result.Success && result.Data != null;
    }
    public static bool DataResultIsNotNull<T>(this PagingResult<T> result) where T : class
    {
        return result != null && result.Success && result.Data != null;
    }

    public static string GetPrettyDate(this DateTime dt)
    {
        TimeSpan span = DateTime.Now - dt;
        if (span.Days > 365)
        {
            int years = (span.Days / 365);
            if (span.Days % 365 != 0)
                years += 1;
            return string.Format("{0} {1} önce",
            years, "yıl");
        }
        if (span.Days > 30)
        {
            int months = (span.Days / 30);
            if (span.Days % 31 != 0)
                months += 1;
            return string.Format("{0} {1} önce", months, "ay");
        }
        if (span.Days > 0)
            return string.Format("{0} {1} önce", span.Days, "gün");
        if (span.Hours > 0)
            return string.Format("{0} {1} önce", span.Hours, "saat");
        if (span.Minutes > 0)
            return string.Format("{0} {1} önce", span.Minutes, "dakika");
        if (span.Seconds > 30)
            return string.Format("{0} saniye önce", span.Seconds);
        if (span.Seconds <= 30)
            return "az önce";
        return string.Empty;
    }
}

