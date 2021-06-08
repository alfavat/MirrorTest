
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;

public partial class AaFileResponse
{
    [JsonProperty("response")]
    public Response Response { get; set; }

    [JsonProperty("data")]
    public FileData Data { get; set; }
}

public partial class FileData
{
    [JsonProperty("result")]
    public FileResult[] Result { get; set; }
}

public partial class FileResult
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("location")]
    public Uri Location { get; set; }
}

public partial class FileResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("code")]
    public long Code { get; set; }
}

public partial class AaFileResponse
{
    public static AaFileResponse FromJson(string json) => JsonConvert.DeserializeObject<AaFileResponse>(json, Converter.Settings);
}

public static class Serialize
{
    public static string ToJson(this AaFileResponse self) => JsonConvert.SerializeObject(self, Converter.Settings);
}

internal static class Converter
{
    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
    };
}
