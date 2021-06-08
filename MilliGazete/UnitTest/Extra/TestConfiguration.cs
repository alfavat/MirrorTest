using Microsoft.Extensions.Configuration;
using System.IO;

namespace UnitTest.Extra
{
    public class TestConfiguration
    {
        public IConfiguration _configuration { get; set; }
        public TestConfiguration()
        {
            _configuration = MockConfiguration();
        }

        private IConfiguration MockConfiguration()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("myappconfig.json", optional: false, reloadOnChange: false)
            .Build();
            return configuration;
        }
    }
}
