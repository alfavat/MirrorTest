using Entity.Models;
using HtmlAgilityPack;
using System;
using System.IO;
using System.Linq;
using System.Web;

public static class StringExtensions
{
    public static int ToInt32(this string str)
    {
        if (string.IsNullOrEmpty(str)) return 0;
        if (str == "null") return 0;
        return Convert.ToInt32(str);
    }

    public static string GetFullName(this User user)
    {
        return user == null ? "" : user.FirstName + " " + user.LastName;
    }

    public static bool ToBool(this string str)
    {
        if (string.IsNullOrEmpty(str)) return false;
        if (str == "null") return false;
        if (str == "1" || str == "true") return true;
        if (str == "0" || str == "false") return false;
        return Convert.ToBoolean(str);
    }
    public static bool StringIsNullOrEmpty(this string val)
    {
        return string.IsNullOrEmpty(val);
    }
    public static bool StringNotNullOrEmpty(this string val)
    {
        return !string.IsNullOrEmpty(val);
    }
    public static string GetFileContentType(this string contentType)
    {
        return contentType == "video/quicktime" ? "video/mp4" : contentType;
    }
    public static DateTime UnixTimestampToDateTime(double timeStamp)
    {
        var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        return dtDateTime.AddSeconds(timeStamp).ToLocalTime();
    }

    public static int GetHistoryNoFromUrl(this string url)
    {
        int hixtoryNo = 0;
        if (url.StringNotNullOrEmpty())
        {
            var splited = url.Split('-');
            if (splited.Length > 0)
            {
                int.TryParse(splited[splited.Length - 1], out hixtoryNo);
            }
        }
        return hixtoryNo;
    }

    public static bool GetPreviewFromUrl(this string url)
    {
        try
        {
            if (url.StartsWith("/"))
            {
                url = url.Remove(0, 1);
            }
            var uriBuilder = new UriBuilder(url);
            return HttpUtility.ParseQueryString(uriBuilder.Query).Get("preview") == "true";
        }
        catch
        {
            return false;
        }

    }

    public static int CheckLimit(this int val)
    {
        return Math.Min(val, 1000);
    }

    public static string ToUrlFormat(this string val)
    {
        return val.ToLower().Trim().Replace(" ", "-");
    }

    public static string FromUrlFormat(this string val)
    {
        return val.ToLower().Trim().Replace("-", " ");
    }

    public static string GetFileExtension(this string val)
    {
        var ext = Path.GetExtension(val);
        return ext.Replace(".", "");
    }

    public static string ShortenText(this string val, int count)
    {
        if (val.StringIsNullOrEmpty()) return "";
        return val.Substring(0, val.Length - 1 < count ? val.Length - 1 : count) + (val.Length > count ? " ..." : "");
    }

    public static string GetFirstWords(this string val, int count)
    {
        if (val.StringIsNullOrEmpty()) return "";
        var splited = val.Split(" ").ToList();
        var words = splited.Take(count).ToList();
        return string.Join(" ", words) + (splited.Count > count ? " ..." : "");
    }

    public static long GetFileSize(this string val)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), val);
        return new FileInfo(path).Length / 1024;
    }

    public static bool IsIhaNewsImage(this string filePath)
    {
        var uriBuilder = new UriBuilder(filePath);
        return HttpUtility.ParseQueryString(uriBuilder.Query).Get("type") == "image";
    }

    public static string ReplaceSpecialCharacters(this string val)
    {
        if (val.StringIsNullOrEmpty()) return "";
        var replacedVal = val.Replace("^", "").Replace("%", "").Replace("&", "")//.Replace("/", "")
            .Replace("?", "").Replace("\"", "").Replace("@", "").Replace("#", "").Replace("$", "")
            .Replace("*", "").Replace("(", "").Replace(")", "").Replace("=", "").Replace("|", "")
            .Replace(">", "").Replace("<", "").Replace("!", "");
        return replacedVal;
    }

    public static string ParseHtml(this string val)
    {
        if (val.StringIsNullOrEmpty()) return "";
        HtmlDocument htmlDoc = new HtmlDocument();
        var str = HttpUtility.HtmlDecode(val);
        htmlDoc.LoadHtml(str);
        return htmlDoc.DocumentNode.InnerText;
    }
}
