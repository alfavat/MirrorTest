using Newtonsoft.Json;
using System;

public partial class AaStandardNewsDto
{
    [JsonProperty("response")]
    public Response Response { get; set; }

    [JsonProperty("data")]
    public AaDetails Data { get; set; }
}

public partial class AaDetails
{
    [JsonProperty("result")]
    public Result[] Result { get; set; }

    [JsonProperty("total")]
    public long Total { get; set; }
}

public partial class Result
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("date")]
    public DateTimeOffset Date { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }
    [JsonProperty("group_id")]
    public string GroupId { get; set; }
}

public partial class Response
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("code")]
    public long Code { get; set; }
}
