public delegate RedisClient WSRedisClient();

public static class WSExtension
{
    /// <summary>
    /// 启用 WSServer 服务端
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseWSServer(this IApplicationBuilder app, WSServerOptions options)
    {
        app.Map($"/{options.PathMatch}", appcur =>
        {
            appcur.UseWebSockets();
            var wsserv = new WSServer(options);
            appcur.Use((ctx, next) => wsserv.Acceptor(ctx, next));
        });
        return app;
    }
}

public class WSSendEventArgs : EventArgs
{
    /// <summary>
    /// 发送者的客户端id
    /// </summary>
    public Guid SenderClientId { get; }

    /// <summary>
    /// 接收者的客户端id
    /// </summary>
    public List<Guid> ReceiveClientId { get; } = new List<Guid>();

    /// <summary>
    /// WSServer 服务器节点
    /// </summary>
    public string Server { get; }

    /// <summary>
    /// 消息
    /// </summary>
    public object Message { get; }

    /// <summary>
    /// 是否回执
    /// </summary>
    public bool Receipt { get; }

    internal WSSendEventArgs(string server, Guid senderClientId, object message, bool receipt = false)
    {
        this.Server = server;
        this.SenderClientId = senderClientId;
        this.Message = message;
        this.Receipt = receipt;
    }
}