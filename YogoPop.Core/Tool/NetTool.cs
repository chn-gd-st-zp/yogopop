namespace YogoPop.Core.Tool;

public static class NetTool
{
    public static string GetSimplifyOrigin => Origin?.Replace("http://", string.Empty).Replace("https://", string.Empty);

    /// <summary>
    /// 获取客户UserAgent
    /// </summary>
    public static string UserAgnet
    {
        get
        {
            HttpContext httpContext = InjectionContext.Resolve<IHttpContextAccessor>().HttpContext;
            return httpContext.Request.Headers["User-Agent"].FirstOrDefault();
        }
    }

    /// <summary>
    /// 获取客户请求地址
    /// </summary>
    public static string UserRequestDomain
    {
        get
        {
            HttpContext httpContext = InjectionContext.Resolve<IHttpContextAccessor>().HttpContext;

            return new StringBuilder()
             .Append(httpContext.Request.Scheme)
             .Append("://")
             .Append(httpContext.Request.Host)
             .Append(httpContext.Request.PathBase).ToString();
        }
    }

    /// <summary>
    /// 获取浏览器信息
    /// </summary>
    public static string Browser
    {
        get
        {
            var httpContext = InjectionContext.Resolve<IHttpContextAccessor>();
            if (httpContext == null)
                return string.Empty;
            var browser = httpContext.HttpContext.Request.Headers["User-Agent"];
            return browser;
        }
    }

    /// <summary>
    /// 获取 请求域名
    /// </summary>
    public static string Host
    {
        get
        {
            var httpContext = InjectionContext.Resolve<IHttpContextAccessor>();
            if (httpContext != null)
            {
                return httpContext.HttpContext.Request.Host.Host;
            }

            return string.Empty;
        }
    }

    /// <summary>
    /// 获取 请求域名
    /// </summary>
    public static string Origin
    {
        get
        {
            var httpContext = InjectionContext.Resolve<IHttpContextAccessor>();
            if (httpContext != null)
            {
                return httpContext.HttpContext.Request.Headers["Origin"];
            }

            return string.Empty;
        }
    }

    /// <summary>
    /// 获取主域名
    /// </summary>
    /// <param name="host"></param>
    /// <returns></returns>
    public static string GetRootDomain(this string host)
    {
        // 如果传的是 URL，取 Host 部分
        if (Uri.TryCreate(host, UriKind.Absolute, out var uri))
        {
            host = uri.Host;
        }

        // 转小写
        host = host.ToLower();

        // 只匹配最后两个或三个部分（支持 .com.cn）
        var match = Regex.Match(host, @"([^.]+\.(?:com|net|org|cn|gov|edu|co)(?:\.[a-z]{2})?)$");
        return match.Success ? match.Value : host;
    }

    /// <summary>
    /// 判断两个域名是否是同一个主域名
    /// </summary>
    /// <param name="domain1"></param>
    /// <param name="domain2"></param>
    /// <returns></returns>
    public static bool IsSameRootDomain(this string domain1, string domain2) => GetRootDomain(domain1) == GetRootDomain(domain2);

    /// <summary>
    /// 获取客户端IP地址
    /// </summary>
    /// <param name="throughProxy">是否穿透代理</param>
    /// <returns></returns>
    public static string GetIP(bool throughProxy = true)
    {
        var httpContext = InjectionContext.Resolve<IHttpContextAccessor>().HttpContext;

        if (!throughProxy)
            return httpContext.Connection.RemoteIpAddress.ToString();

        var checkKeys = new List<string>
        {
            "X-FORWARDED-FOR",
            "X_FORWARDED_FOR",
        };

        foreach (var header in httpContext.Request.Headers)
        {
            foreach (var checkKey in checkKeys)
            {
                if (header.Key.Contains(checkKey, StringComparison.OrdinalIgnoreCase))
                {
                    var ip = httpContext.Request.Headers[header.Key].ToString();
                    if (!ip.IsEmptyString())
                        return ip;
                }
            }
        }

        return "0.0.0.0";
    }

    /// <summary>
    /// 获取IP地址信息
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public static string GetLocation(string ip)
    {
        if (ip.IsEmptyString() || ip.Equals("0.0.0.0") || ip.Equals("localhost") || ip.Equals("127.0.0.1"))
        {
            return string.Empty;
        }

        string res = string.Empty;

        try
        {
            string url = "http://apis.juhe.cn/ip/ip2addr?ip=" + ip + "&dtype=json&key=a86451534a6e72728b8cea430dabc633";
            res = url.GetJsonAsync().GetAwaiter().GetResult();
            var resjson = res.ToObject<objex>();
            res = resjson.result.area + " " + resjson.result.location;
        }
        catch
        {
            res = string.Empty;
        }

        if (!res.IsEmptyString())
        {
            return res;
        }

        try
        {
            string url = "https://sp0.baidu.com/8aQDcjqpAAV3otqbppnN2DJv/api.php?query=" + ip + "&resource_id=6006&ie=utf8&oe=gbk&format=json";
            res = url.GetJsonAsync().GetAwaiter().GetResult();
            var resjson = res.ToObject<obj>();
            res = resjson.data[0].location;
        }
        catch
        {
            res = string.Empty;
        }

        return res;
    }

    public static string GetUrl(this HttpRequest request)
    {
        return new StringBuilder()
            .Append(request.Scheme)
            .Append("://")
            .Append(request.Host)
            .Append(request.PathBase)
            .Append(request.Path)
            .Append(request.QueryString)
            .ToString();
    }

    public static string GetQueryString(this HttpRequest request)
    {
        var queryString = request.QueryString;
        var collection = HttpUtility.ParseQueryString(queryString.ToString());
        return collection.AllKeys.ToDictionary(k => k, v => collection[v]).ToJson();
    }

    public static async Task<string> GetRequestValue(this HttpRequest request)
    {
        var json = "{}";

        try
        {
            if (request.Method.IsEquals("get", StringComparison.OrdinalIgnoreCase))
            {
                json = request.GetQueryString();
            }
            else if (request.HasFormContentType && request.Form.IsNotEmpty() && request.Form.Keys.IsNotEmpty())
            {
                json = request.Form.Keys.ToDictionary(k => k, v => request.Form[v].First()).ToJson();
            }
            else
            {
                var bodyStr = await request.ReadBodyAsync();
                bodyStr = bodyStr.IsNotEmptyString() ? bodyStr : string.Empty;
                if (bodyStr.StartsWith('{') && bodyStr.EndsWith('}'))
                {
                    json = bodyStr;
                }
                else if (bodyStr.Contains('&') && bodyStr.Contains('='))
                {
                    var collection = HttpUtility.ParseQueryString(bodyStr);
                    json = collection.AllKeys.ToDictionary(k => k, v => collection[v]).ToJson();
                }
                else if (bodyStr.Contains("?xml"))
                {
                    var xml = new XmlDocument();
                    xml.LoadXml(bodyStr);
                    json = JsonConvert.SerializeXmlNode(xml);
                }
            }

            //json = JToken.Parse(json).ToJson();
        }
        catch
        {
            json = "{}";
        }

        return json;
    }

    public static async Task<string> ReadBodyAsync(this HttpRequest request)
    {
        if (request.ContentLength > 0)
        {
            await EnableRewindAsync(request).ConfigureAwait(false);
            var encoding = GetRequestEncoding(request);
            return await ReadStreamAsync(request.Body, encoding).ConfigureAwait(false);
        }
        return null;
    }

    private static async Task EnableRewindAsync(HttpRequest request)
    {
        if (!request.Body.CanSeek)
        {
            request.EnableBuffering();

            await request.Body.DrainAsync(CancellationToken.None);
            request.Body.Seek(0L, SeekOrigin.Begin);
        }
    }

    private static Encoding GetRequestEncoding(HttpRequest request)
    {
        var requestContentType = request.ContentType;
        var requestMediaType = requestContentType == null ? default(MediaType) : new MediaType(requestContentType);
        var requestEncoding = requestMediaType.Encoding;
        if (requestEncoding == null)
        {
            requestEncoding = Encoding.UTF8;
        }
        return requestEncoding;
    }

    private static async Task<string> ReadStreamAsync(Stream stream, Encoding encoding)
    {
        using (var sr = new StreamReader(stream, encoding, true, 1024, true))
        {
            stream.Seek(0, SeekOrigin.Begin);
            var str = await sr.ReadToEndAsync();
            return str;
        }
    }
}

#region 对象实体

/// <summary>
/// 百度接口
/// </summary>
public class obj
{
    public List<dataone> data { get; set; }
}

public class dataone
{
    public string location { get; set; }
}

/// <summary>
/// 聚合数据
/// </summary>
public class objex
{
    public string resultcode { get; set; }
    public dataoneex result { get; set; }
    public string reason { get; set; }
    public string error_code { get; set; }
}

public class dataoneex
{
    public string area { get; set; }
    public string location { get; set; }
}

#endregion