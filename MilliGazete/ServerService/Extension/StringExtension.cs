using System;

public static class StringExtension
{
    public static DateTime? GetDateTime(this string date)
    {
        if (date.StringIsNotNullOrEmpty() && DateTime.TryParse(date, out DateTime result))
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

    public static bool StringIsNullOrEmpty(this string val)
    {
        return string.IsNullOrEmpty(val);
    }

    public static bool StringIsNotNullOrEmpty(this string val)
    {
        return !string.IsNullOrEmpty(val);
    }

    public static int ToInt32(this string val)
    {
        return int.Parse(val);
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
