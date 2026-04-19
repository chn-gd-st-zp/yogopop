/// <summary>
/// WS 核心类实现的配置所需
/// </summary>
public class WSOptions
{
    /// <summary>
    /// redis对象
    /// </summary>
    public WSRedisClient RedisFunc { get; set; }

    /// <summary>
    /// 负载的服务端
    /// </summary>
    public string[] Servers { get; set; }

    /// <summary>
    /// websocket请求的路径，默认值: ws
    /// </summary>
    public string PathMatch { get; set; } = "ws";
}

/// <summary>
/// WS 核心类实现的配置所需
/// </summary>
public class WSServerOptions : WSOptions
{
    /// <summary>
    /// 设置服务名称，它应该是 servers 内的一个
    /// </summary>
    public string Server { get; set; }
}