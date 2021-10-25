using System;
using System.Globalization;
using System.Linq;
using System.Text;

public static class StringExtension
{
    public static DateTime? GetDateTime(this string date)
    {
        if (date.StringNotNullOrEmpty() && DateTime.TryParse(date, out DateTime result))
        {
            return result;
        }
        return null;
    }

    public static DateTime? TurkishToUTCDate(this string dt)
    {
        try
        {
            if (dt.StringIsNullOrEmpty())
            {
                return null;
            }
            if (DateTime.TryParse(dt, out DateTime result))
            {
                return result;
            }
            var sp = dt.Split('.');
            if (sp.Length < 3)
            {
                return null;
            }
            return DateTime.Parse(sp[1] + "/" + sp[0] + "/" + sp[2]);

        }
        catch
        {
            return null;
        }
    }
    public static string ToEnglishCharacter(this string text)
    {
        if (string.IsNullOrEmpty(text)) return "";

        return string.Join("", text.Normalize(NormalizationForm.FormD)
       .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)).Replace("ı", "i");
    }

    public static int ToSeconds(this string val)
    {
        return int.Parse(val) * 1000;
    }

    public static bool ToBoolean(this string val)
    {
        return bool.Parse(val);
    }

    public static DateTime UnixTimeStampToDateTime(this long unixTimeStamp)
    {
        try
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        catch
        {
            return DateTime.Now;
        }
    }

}
