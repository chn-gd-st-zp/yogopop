/// <summary>
/// 核心类实现
/// </summary>
public class WSInstance
{
    protected WSRedisClient _redisFunc;
    protected string[] _servers;
    protected string _pathMatch;
    protected string _redisPrefix;

    /// <summary>
    /// 推送消息的事件，可审查推向哪个Server节点
    /// </summary>
    public EventHandler<WSSendEventArgs> OnSend;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="options"></param>
    public WSInstance(WSOptions options)
    {
        if (options.RedisFunc == null) throw new ArgumentException("WSClientOptions.Redis 参数不能为空");
        if (options.Servers.Any() == false) throw new ArgumentException("WSClientOptions.Servers 参数不能为空");
        _redisFunc = options.RedisFunc;
        _servers = options.Servers;
        _pathMatch = options.PathMatch;
        _redisPrefix = $"ws_{_pathMatch}";
    }

    /// <summary>
    /// 负载分区规则: 取clientId后四位字符，转成10进制数字0-65535，求模
    /// </summary>
    /// <param name="clientId">客户端id</param>
    /// <returns></returns>
    protected string SelectServer(Guid clientId)
    {
        var servers_idx = int.Parse(clientId.ToString("N").Substring(28), NumberStyles.HexNumber) % _servers.Length;
        if (servers_idx >= _servers.Length) servers_idx = 0;
        return _servers[servers_idx];
    }

    /// <summary>
    /// WSServer 连接前的负载、授权，返回 ws 目标地址，使用该地址连接 websocket 服务端
    /// </summary>
    /// <param name="clientId">客户端id</param>
    /// <param name="clientMetaData">客户端相关信息，比如ip</param>
    /// <returns>websocket 地址: ws://xxxx/ws?token=xxx 或 wss://xxxx/ws?token=xxx</returns>
    public string PrevConnectServer(Guid clientId, string clientMetaData)
    {
        var token = $"{Guid.NewGuid()}{Guid.NewGuid()}{Guid.NewGuid()}{Guid.NewGuid()}".Replace("-", string.Empty);
        using (var client = _redisFunc.Invoke())
            client.Set($"{_redisPrefix}Token{token}", (clientId, clientMetaData).ToJson(), 10);
        var server = SelectServer(clientId);
        //return $"{_pathMatch}://{server}/ws?token={token}";
        return $"{_pathMatch}://{server}/{_pathMatch}?token={token}";
    }

    /// <summary>
    /// 向指定的多个客户端id发送消息
    /// </summary>
    /// <param name="senderClientId">发送者的客户端id</param>
    /// <param name="receiveClientId">接收者的客户端id</param>
    /// <param name="message">消息</param>
    /// <param name="receipt">是否回执</param>
    public void SendMessage(Guid senderClientId, IEnumerable<Guid> receiveClientId, object message, bool receipt = false)
    {
        receiveClientId = receiveClientId.Distinct().ToArray();
        Dictionary<string, WSSendEventArgs> redata = new Dictionary<string, WSSendEventArgs>();

        foreach (var uid in receiveClientId)
        {
            string server = SelectServer(uid);
            if (redata.ContainsKey(server) == false) redata.Add(server, new WSSendEventArgs(server, senderClientId, message, receipt));
            redata[server].ReceiveClientId.Add(uid);
        }
        var messageJson = message.ToJson();
        foreach (var sendArgs in redata.Values)
        {
            OnSend?.Invoke(this, sendArgs);
            using (var client = _redisFunc.Invoke())
                client.Publish($"{_redisPrefix}Server{sendArgs.Server}", (senderClientId, sendArgs.ReceiveClientId, messageJson, sendArgs.Receipt).ToJson());
        }
    }

    /// <summary>
    /// 获取所在线客户端id
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Guid> GetClientListByOnline()
    {
        using (var client = _redisFunc.Invoke())
            return client.HKeys($"{_redisPrefix}Online").Select(a => Guid.TryParse(a, out var tryguid) ? tryguid : Guid.Empty).Where(a => a != Guid.Empty);
    }

    /// <summary>
    /// 判断客户端是否在线
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    public bool HasOnline(Guid clientId)
    {
        using (var client = _redisFunc.Invoke())
            return client.HGet<int>($"{_redisPrefix}Online", clientId.ToString()) > 0;
    }

    /// <summary>
    /// 强制下线
    /// </summary>
    /// <param name="clientId"></param>
    public void ForceOffline(Guid clientId)
    {
        string server = SelectServer(clientId);
        using (var client = _redisFunc.Invoke())
            client.Publish($"{_redisPrefix}Server{server}", $"__WS__(ForceOffline){clientId}");
    }

    /// <summary>
    /// 事件订阅
    /// </summary>
    /// <param name="online">上线</param>
    /// <param name="offline">下线</param>
    public void EventBus(
        Action<(Guid clientId, string clientMetaData)> online,
        Action<(Guid clientId, string clientMetaData)> offline)
    {
        var chanOnline = $"evt_{_redisPrefix}Online";
        var chanOffline = $"evt_{_redisPrefix}Offline";

        using (var client = _redisFunc.Invoke())
        {
            client.Subscribe(new[] { chanOnline, chanOffline }, (chan, msg) =>
            {
                if (chan == chanOnline) online((msg as string).ToObject<(Guid clientId, string clientMetaData)>());
                if (chan == chanOffline) offline((msg as string).ToObject<(Guid clientId, string clientMetaData)>());
            });
        }
    }

    #region 群聊频道，每次上线都必须重新加入

    /// <summary>
    /// 加入群聊频道，每次上线都必须重新加入
    /// </summary>
    /// <param name="clientId">客户端id</param>
    /// <param name="chan">群聊频道名</param>
    public void JoinChan(Guid clientId, string chan)
    {
        using (var client = _redisFunc.Invoke())
        using (var pipe = client.StartPipe())
        {
            pipe.HSet($"{_redisPrefix}Chan{chan}", clientId.ToString(), 0);
            pipe.HSet($"{_redisPrefix}Client{clientId}", chan, 0);
            pipe.HIncrBy($"{_redisPrefix}ListChan", chan, 1);
            pipe.EndPipe();
        }
    }

    /// <summary>
    /// 离开群聊频道
    /// </summary>
    /// <param name="clientId">客户端id</param>
    /// <param name="chans">群聊频道名</param>
    public void LeaveChan(Guid clientId, params string[] chans)
    {
        if (chans?.Any() != true) return;
        using (var client = _redisFunc.Invoke())
        using (var pipe = client.StartPipe())
        {
            foreach (var chan in chans)
            {
                pipe.HDel($"{_redisPrefix}Chan{chan}", clientId.ToString());
                pipe.HDel($"{_redisPrefix}Client{clientId}", chan);
                pipe.Eval($"if redis.call('HINCRBY', KEYS[1], '{chan}', '-1') <= 0 then redis.call('HDEL', KEYS[1], '{chan}') end return 1", new[] { $"{_redisPrefix}ListChan" });
            }
            pipe.EndPipe();
        }
    }

    /// <summary>
    /// 获取群聊频道所有客户端id（测试）
    /// </summary>
    /// <param name="chan">群聊频道名</param>
    /// <returns></returns>
    public Guid[] GetChanClientList(string chan)
    {
        using (var client = _redisFunc.Invoke())
            return client.HKeys($"{_redisPrefix}Chan{chan}").Select(a => a.ToGuid()).ToArray();
    }

    /// <summary>
    /// 清理群聊频道的离线客户端（测试）
    /// </summary>
    /// <param name="chan">群聊频道名</param>
    public void ClearChanClient(string chan)
    {
        using (var client = _redisFunc.Invoke())
        {
            var websocketIds = client.HKeys($"{_redisPrefix}Chan{chan}");
            var offline = new List<string>();
            var span = websocketIds.AsSpan();
            var start = span.Length;
            while (start > 0)
            {
                start = start - 10;
                var length = 10;
                if (start < 0)
                {
                    length = start + 10;
                    start = 0;
                }
                var slice = span.Slice(start, length);
                var hvals = client.HMGet($"{_redisPrefix}Online", slice.ToArray().Select(b => b.ToString()).ToArray());
                for (var a = length - 1; a >= 0; a--)
                {
                    if (hvals[a].IsEmptyString())
                    {
                        offline.Add(span[start + a]);
                        span[start + a] = null;
                    }
                }
            }
            //删除离线订阅
            if (offline.Any()) client.HDel($"{_redisPrefix}Chan{chan}", offline.ToArray());
        }
    }

    /// <summary>
    /// 获取所有群聊频道和在线人数
    /// </summary>
    /// <returns>频道名和在线人数</returns>
    public IEnumerable<(string chan, long online)> GetChanList()
    {
        using (var client = _redisFunc.Invoke())
        {
            var ret = client.HGetAll<long>($"{_redisPrefix}ListChan");
            return ret.Select(a => (a.Key, a.Value));
        }
    }

    /// <summary>
    /// 获取用户参与的所有群聊频道
    /// </summary>
    /// <param name="clientId">客户端id</param>
    /// <returns></returns>
    public string[] GetChanListByClientId(Guid clientId)
    {
        using (var client = _redisFunc.Invoke())
            return client.HKeys($"{_redisPrefix}Client{clientId}");
    }

    /// <summary>
    /// 获取群聊频道的在线人数
    /// </summary>
    /// <param name="chan">群聊频道名</param>
    /// <returns>在线人数</returns>
    public long GetChanOnline(string chan)
    {
        using (var client = _redisFunc.Invoke())
            return client.HGet<long>($"{_redisPrefix}ListChan", chan);
    }

    /// <summary>
    /// 发送群聊消息，所有在线的用户将收到消息
    /// </summary>
    /// <param name="senderClientId">发送者的客户端id</param>
    /// <param name="chan">群聊频道名</param>
    /// <param name="message">消息</param>
    public void SendChanMessage(Guid senderClientId, string chan, object message)
    {
        using (var client = _redisFunc.Invoke())
        {
            var websocketIds = client.HKeys($"{_redisPrefix}Chan{chan}");
            //SendMessage(Guid.Empty, websocketIds.Where(a => !string.IsNullOrEmpty(a)).Select(a => Guid.TryParse(a, out var tryuuid) ? tryuuid : Guid.Empty).ToArray(), message);
            SendMessage(Guid.Empty, websocketIds.Where(a => a.IsNotEmptyString()).Select(a => Guid.TryParse(a, out var tryuuid) ? tryuuid : Guid.Empty).ToArray(), message);
        }
    }

    #endregion
}