using Entity;
using Entity.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public static class CommonHelper
{
    public static int CurrentLanguageId
    {
        get
        {
            var httpContextAccessor = EntityServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            string language = httpContextAccessor.HttpContext.Request.Headers["accept-language"].ToString();
            switch (language)
            {
                case "tr-TR":
                    return (int)Languages.Turkish;
                case "en-US":
                    return (int)Languages.English;
                default:
                    return (int)Languages.All;
            }
        }
    }
}

