namespace DForge.DSP.GoDaddy;

internal class ResponseResult
{
    [JsonProperty("domainId")]
    public string ID { get; set; }

    [JsonProperty("domain")]
    public string Name { get; set; }

    [JsonProperty("createdAt")]
    public DateTime CreateTime { get; set; }

    [JsonProperty("expires")]
    public DateTime? ExpiredTime { get; set; }

    [JsonProperty("nameServers")]
    public string[] NameServers { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }
}

internal class ResponseResults : List<ResponseResult> { }