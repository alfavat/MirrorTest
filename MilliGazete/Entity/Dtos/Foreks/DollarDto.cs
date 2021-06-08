using Newtonsoft.Json;
namespace Entity.Dtos
{
    public partial class ForeksDto
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("DailyChangePercent")]
        public double DailyChangePercent { get; set; }

        [JsonProperty("Ask")]
        public double Ask { get; set; }


        [JsonProperty("Last")]
        public double Last { get; set; }

        [JsonProperty("DateTime")]
        public long DateTime { get; set; }

        [JsonProperty("Ticker")]
        public string Ticker { get; set; }
    }
}

