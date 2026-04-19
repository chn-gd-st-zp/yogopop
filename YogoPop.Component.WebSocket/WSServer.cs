/// <summary>
/// WS链接中的客户端
/// </summary>
public class WSServerClient
{
    public WebSocket socket;
    public Guid clientId;

    public WSServerClient(WebSocket socket, Guid clientId)
    {
        this.socket = socket;
        this.clientId = clientId;
    }
}

/// <summary>
/// WS服务端
/// </summary>
public class WSServer : WSInstance
{
    public const int BufferSize = 4096;

    private ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, WSServerClient>> _clients = new ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, WSServerClient>>();

    protected string Server { get; private set; }

    public WSServer(WSServerOptions options) : base(options)
    {
        Server = options.Server;
        using (var client = _redisFunc.Invoke())
            client.Subscribe($"{_redisPrefix}Server{Server}", RedisSubScribleMessage);
    }

    private void RedisSubScribleMessage(string chan, object msg)
    {
        try
        {
            var msgtxt = msg as string;
            if (msgtxt.StartsWith("__WS__(ForceOffline)"))
            {
                if (Guid.TryParse(msgtxt.Substring(24), out var clientId) && _clients.TryRemove(clientId, out var oldclients))
                    foreach (var oldcli in oldclients)
                    {
                        try { oldcli.Value.socket.CloseAsync(WebSocketCloseStatus.EndpointUnavailable, "disconnect", CancellationToken.None).GetAwaiter().GetResult(); } catch { }
                        try { oldcli.Value.socket.Abort(); } catch { }
                        try { oldcli.Value.socket.Dispose(); } catch { }
                    }
                return;
            }
            var data = (msg as string).ToObject<(Guid senderClientId, Guid[] receiveClientId, string content, bool receipt)>();
            //Console.WriteLine($"收到消息: {data.content}" + (data.receipt ? "【需回执】" : string.Empty));

            var outgoing = new ArraySegment<byte>(Encoding.UTF8.GetBytes(data.content));
            foreach (var clientId in data.receiveClientId)
            {
                if (_clients.TryGetValue(clientId, out var wslist) == false)
                {
                    //Console.WriteLine($"websocket{clientId} 离线了，{data.content}" + (data.receipt ? "【需回执】" : string.Empty));
                    if (data.senderClientId != Guid.Empty && clientId != data.senderClientId && data.receipt)
                        SendMessage(clientId, new[] { data.senderClientId }, new
                        {
                            data.content,
                            receipt = "用户不在线"
                        });
                    continue;
                }

                WSServerClient[] sockarray = wslist.Values.ToArray();

                //如果接收消息人是发送者，并且接收者只有1个以下，则不发送
                //只有接收者为多端时，才转发消息通知其他端
                if (clientId == data.senderClientId && sockarray.Length <= 1) continue;

                foreach (var sh in sockarray)
                    sh.socket.SendAsync(outgoing, WebSocketMessageType.Text, true, CancellationToken.None)
                        .ContinueWith(async (t, state) =>
                        {
                            if (t.Exception != null)
                            {
                                var ws = state as WebSocket;
                                try { await ws.CloseAsync(WebSocketCloseStatus.EndpointUnavailable, "disconnect", CancellationToken.None); } catch { }
                                try { ws.Abort(); } catch { }
                                try { ws.Dispose(); } catch { }
                            }
                        }, sh.socket);

                if (data.senderClientId != Guid.Empty && clientId != data.senderClientId && data.receipt)
                    SendMessage(clientId, new[] { data.senderClientId }, new
                    {
                        data.content,
                        receipt = "发送成功"
                    });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"WSServer 订阅方法出错了: {ex.Message}");
        }
    }

    internal async Task Acceptor(HttpContext context, Func<Task> next)
    {
        if (!context.WebSockets.IsWebSocketRequest) return;

        string token = context.Request.Query["token"];
        if (token.IsEmptyString()) return;

        using (var client = _redisFunc.Invoke())
        {
            var token_value = client.Get($"{_redisPrefix}Token{token}");
            if (token_value.IsEmptyString())
                throw new Exception("授权错误");

            var data = token_value.ToObject<(Guid clientId, string clientMetaData)>();

            var socket = await context.WebSockets.AcceptWebSocketAsync();
            var cli = new WSServerClient(socket, data.clientId);
            var newid = Guid.NewGuid();

            var wslist = _clients.GetOrAdd(data.clientId, cliid => new ConcurrentDictionary<Guid, WSServerClient>());
            wslist.TryAdd(newid, cli);
            using (var pipe = client.StartPipe())
            {
                pipe.HIncrBy($"{_redisPrefix}Online", data.clientId.ToString(), 1);
                pipe.Publish($"evt_{_redisPrefix}Online", token_value);
                pipe.EndPipe();
            }

            var buffer = new byte[BufferSize];
            var seg = new ArraySegment<byte>(buffer);
            try
            {
                while (socket.State == WebSocketState.Open && _clients.ContainsKey(data.clientId))
                {
                    var incoming = await socket.ReceiveAsync(seg, CancellationToken.None);
                    var outgoing = new ArraySegment<byte>(buffer, 0, incoming.Count);
                }
                socket.Abort();
            }
            catch
            {
            }
            wslist.TryRemove(newid, out var oldcli);
            if (wslist.Any() == false) _clients.TryRemove(data.clientId, out var oldwslist);
            client.Eval($"if redis.call('HINCRBY', KEYS[1], '{data.clientId}', '-1') <= 0 then redis.call('HDEL', KEYS[1], '{data.clientId}') end return 1", new[] { $"{_redisPrefix}Online" });
            LeaveChan(data.clientId, GetChanListByClientId(data.clientId));
            client.Publish($"evt_{_redisPrefix}Offline", token_value);
        }
    }
}