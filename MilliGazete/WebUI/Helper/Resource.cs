using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace WebUI
{
    public class Resource
    {
        public string key { get; set; }
        public string value { get; set; }
        public string description { get; set; }
    }
    public static class Translator
    {
        public static string GetByKey(string key)
        {
            var op = new LocalizationOptions();
            op.ResourcesPath = "Resources";
            var options = Options.Create(op);
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var localizer = new StringLocalizer<Resource>(factory);
            return localizer[key];
        }
    }
}
