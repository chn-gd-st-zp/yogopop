namespace DForge.DSP.CloudFlare;

internal class ResponseResult<T>
{
    [JsonProperty("success")]
    internal bool Success { get; set; }

    [JsonProperty("errors")]
    internal List<ApiError> Errors { get; set; }

    [JsonProperty("messages")]
    internal List<ApiMessage> Messages { get; set; }

    [JsonProperty("result")]
    internal T Result { get; set; }
}

internal class ResponseResults<T>
{
    [JsonProperty("success")]
    internal bool Success { get; set; }

    [JsonProperty("errors")]
    internal List<ApiError> Errors { get; set; }

    [JsonProperty("messages")]
    internal List<ApiMessage> Messages { get; set; }

    [JsonProperty("result_info")]
    internal ResultInfo ResultInfo { get; set; }

    [JsonProperty("result")]
    internal List<T> Result { get; set; }
}

internal class ApiError
{
    [JsonProperty("code")]
    internal int Code { get; set; }

    [JsonProperty("message")]
    internal string Message { get; set; }
}

internal class ApiMessage
{
    [JsonProperty("code")]
    internal int Code { get; set; }

    [JsonProperty("message")]
    internal string Message { get; set; }
}

internal class ResultInfo
{
    [JsonProperty("count")]
    internal int Count { get; set; }

    [JsonProperty("page")]
    internal int Page { get; set; }

    [JsonProperty("per_page")]
    internal int PerPage { get; set; }

    [JsonProperty("total_count")]
    internal int TotalCount { get; set; }

    [JsonProperty("total_pages")]
    internal int TotalPages { get; set; }
}

internal class DomainItem
{
    [JsonProperty("id")]
    internal string ID { get; set; }

    [JsonProperty("name")]
    internal string Name { get; set; }

    [JsonProperty("cname_suffix")]
    internal string CnameSuffix { get; set; }

    [JsonProperty("paused")]
    internal bool Paused { get; set; }

    [JsonProperty("permissions")]
    internal List<string> Permissions { get; set; }

    [JsonProperty("type")]
    internal string Type { get; set; }

    [JsonProperty("vanity_name_servers")]
    internal List<string> VanityNameServers { get; set; }
}

internal class DNSRecordItem
{
    [JsonProperty("id")]
    internal string ID { get; set; }

    [JsonProperty("name")]
    internal string Name { get; set; }

    [JsonProperty("ttl")]
    internal int TTL { get; set; }

    [JsonProperty("type")]
    internal string Type { get; set; }

    [JsonProperty("comment")]
    internal string Comment { get; set; }

    [JsonProperty("content")]
    internal string Content { get; set; }

    [JsonProperty("proxied")]
    internal bool Proxied { get; set; }

    [JsonProperty("settings")]
    internal DNSSettings Settings { get; set; }

    [JsonProperty("tags")]
    internal List<string> Tags { get; set; }
}

internal class DNSSettings
{
    [JsonProperty("ipv4_only")]
    internal bool Ipv4Only { get; set; }

    [JsonProperty("ipv6_only")]
    internal bool Ipv6Only { get; set; }
}