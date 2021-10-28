using Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class AppSettingsExtension
{
    public static T GetValue<T>(string key)
    {
        var _config = EntityServiceTool.ServiceProvider.GetService<IConfiguration>();
        return _config.GetSection(key).Get<T>();
    }
}
