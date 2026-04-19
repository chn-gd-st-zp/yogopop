namespace YogoPop.Component.Cache;

[DIModeForService(DIModeEnum.AsSelf)]
public class SERedisProvider : ISingleton
{
    private readonly object _lock = new();
    private ConnectionMultiplexer _client { get; set; }

    public ConnectionMultiplexer GetClient()
    {
        var redisSettings = InjectionContext.Resolve<RedisSettings>();
        if (redisSettings == null) return default;
        if (redisSettings.ConnectionList.IsEmpty()) return default;

        var options = ConfigurationOptions.Parse(redisSettings.ConnectionList.ToString(","));
        //options.ConnectTimeout = Convert.ToInt32(TimeSpan.FromSeconds(redisSettings.Timeout).TotalMilliseconds);
        //options.SyncTimeout = Convert.ToInt32(TimeSpan.FromSeconds(redisSettings.Timeout).TotalMilliseconds);
        //options.AsyncTimeout = Convert.ToInt32(TimeSpan.FromSeconds(redisSettings.Timeout).TotalMilliseconds);

        if (redisSettings.PoolSize == default)
        {
            return ConnectionMultiplexer.Connect(options);
        }
        else
        {
            //StackExchange.Redis 不支持通过连接字符串直接设置连接池大小，
            //因为 StackExchange.Redis 没有显式的连接池设置
            //它是通过 ConnectionMultiplexer 自动管理连接的，复用已有的连接来实现高效的并发访问
            lock (_lock)
            {
                if (_client != null) return _client;
                _client = ConnectionMultiplexer.Connect(options);
                return _client;
            }
        }
    }
}

public class SERedis : RedisBasic
{
    public SERedis(RedisSettings redisSettings, int defaultDatabase = -1) : base(redisSettings, defaultDatabase) { }

    private ConnectionMultiplexer _client { get; set; }

    public override void DisposeClient() { if (_client != null) { _client.Dispose(); _client = null; } }

    private IDatabase getClient(int? dbIndex = null)
    {
        var client = default(ConnectionMultiplexer);

        if (RedisSettings.PoolSize == default)
        {
            if (_client == null) _client = InjectionContext.Resolve<SERedisProvider>().GetClient();
            client = _client;
        }
        else
        {
            client = InjectionContext.Resolve<SERedisProvider>().GetClient();
        }

        return client.GetDatabase(dbIndex.HasValue ? dbIndex.Value : CurrentDatabase);
    }

    public override T GetClient<T>(int? dbIndex = null) => (T)getClient(dbIndex);

    public override bool Expire(string key, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.KeyExpire(key, expiry: default(DateTime?));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override async Task<bool> ExpireAsync(string key, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.KeyExpireAsync(key, expiry: default(DateTime?));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override bool Expire(string key, TimeSpan ts, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.KeyExpire(key, ts);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override async Task<bool> ExpireAsync(string key, TimeSpan ts, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.KeyExpireAsync(key, ts);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override List<string> Keys(string pattern = "*", int? dbIndex = null)
    {
        var result = new List<string>();
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            var redisResult = db.ScriptEvaluate(LuaScript.Prepare("return redis.call('KEYS', @keypattern)"), new { @keypattern = pattern });
            if (redisResult.IsNull) return result;

            var keys = (RedisKey[])redisResult;
            if (keys.IsEmpty()) return result;

            result = keys.Select(o => o.ToString()).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override async Task<List<string>> KeysAsync(string pattern = "*", int? dbIndex = null)
    {
        var result = new List<string>();
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            var redisResult = await db.ScriptEvaluateAsync(LuaScript.Prepare("return redis.call('KEYS', @keypattern)"), new { @keypattern = pattern });
            if (redisResult.IsNull) return result;

            var keys = (RedisKey[])redisResult;
            if (keys.IsEmpty()) return result;

            result = keys.Select(o => o.ToString()).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override List<T> List<T>(string pattern = "*", int? dbIndex = null)
    {
        var result = new List<T>();
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            var redisResult = db.ScriptEvaluate(LuaScript.Prepare("return redis.call('KEYS', @keypattern)"), new { @keypattern = pattern });
            if (redisResult.IsNull) return result;

            var keys = (RedisKey[])redisResult;
            if (keys.IsEmpty()) return result;

            result = db
                .StringGet(keys)
                .Select(o => o.ToString())
                .Select(o => o.IsEmptyString() ? string.Empty : o)
                .Select(o => typeof(T).IsString() ? o.Convert<T>() : o.ToObject<T>())
                .ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override async Task<List<T>> ListAsync<T>(string pattern = "*", int? dbIndex = null)
    {
        var result = new List<T>();
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            var redisResult = await db.ScriptEvaluateAsync(LuaScript.Prepare("return redis.call('KEYS', @keypattern)"), new { @keypattern = pattern });
            if (redisResult.IsNull) return result;

            var keys = (RedisKey[])redisResult;
            if (keys.IsEmpty()) return result;

            result = db
                .StringGet(keys)
                .Select(o => o.ToString())
                .Select(o => o.IsEmptyString() ? string.Empty : o)
                .Select(o => typeof(T).IsString() ? o.Convert<T>() : o.ToObject<T>())
                .ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override bool ListRightPush(string key, string obj, DateTime? expiredTime = null, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.ListRightPush(key, obj) == 1;

            if (expiredTime.HasValue)
                db.KeyExpire(key, expiredTime.Value);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override async Task<bool> ListRightPushAsync(string key, string obj, DateTime? expiredTime = null, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.ListRightPushAsync(key, obj) == 1;

            if (expiredTime.HasValue)
                await db.KeyExpireAsync(key, expiredTime.Value);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override bool ListRemove(string key, string obj, long count = 0, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.ListRemove(key, obj, count) != default;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override async Task<bool> ListRemoveAsync(string key, string obj, long count = 0, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.ListRemoveAsync(key, obj, count) != default;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override List<T> ListRange<T>(string key, int? dbIndex = null)
    {
        var result = new List<T>();
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var values = db.ListRange(key, 0).Select(o => o.ToString()).ToArray();
            foreach (var value in values)
            {
                var item = value.IsEmptyString() ? default : (typeof(T).IsString() ? value.Convert<T>() : value.ToObject<T>());
                result.Add(item);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override async Task<List<T>> ListRangeAsync<T>(string key, int? dbIndex = null)
    {
        var result = new List<T>();
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var values = (await db.ListRangeAsync(key, 0)).Select(o => o.ToString()).ToArray();
            foreach (var value in values)
            {
                var item = value.IsEmptyString() ? default : (typeof(T).IsString() ? value.Convert<T>() : value.ToObject<T>());
                result.Add(item);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override bool Set<T>(string key, T value, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var data = value is string ? value.ToString() : value.ToJson();
            result = db.StringSet(key, data);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override async Task<bool> SetAsync<T>(string key, T value, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var data = value is string ? value.ToString() : value.ToJson();
            result = await db.StringSetAsync(key, data);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override bool Set<T>(string key, T value, TimeSpan ts, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var data = value is string ? value.ToString() : value.ToJson();
            result = db.StringSet(key, data, ts);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override async Task<bool> SetAsync<T>(string key, T value, TimeSpan ts, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var data = value is string ? value.ToString() : value.ToJson();
            result = await db.StringSetAsync(key, data, ts);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override bool Del(string key, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.KeyDelete(key);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override async Task<bool> DelAsync(string key, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.KeyDeleteAsync(key);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override T Get<T>(string key, int? dbIndex = null)
    {
        var result = default(T);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var value = db.StringGet(key);

            result = !value.HasValue || value.ToString().IsEmptyString() ? default(T) : (typeof(T).IsString() ? value.ToString().Convert<T>() : value.ToString().ToObject<T>());
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override async Task<T> GetAsync<T>(string key, int? dbIndex = null)
    {
        var result = default(T);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var value = await db.StringGetAsync(key);

            result = !value.HasValue || value.ToString().IsEmptyString() ? default(T) : (typeof(T).IsString() ? value.ToString().Convert<T>() : value.ToString().ToObject<T>());
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override bool Exists(string key, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.KeyExists(key);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override async Task<bool> ExistsAsync(string key, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.KeyExistsAsync(key);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override long Increase(string key, long increment = 1, int? dbIndex = null)
    {
        var result = default(long);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.StringIncrement(key, increment);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override async Task<long> IncreaseAsync(string key, long increment = 1, int? dbIndex = null)
    {
        var result = default(long);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.StringIncrementAsync(key, increment);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override long Decrease(string key, long decrement = 1, int? dbIndex = null)
    {
        var result = default(long);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.StringDecrement(key, decrement);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override async Task<long> DecreaseAsync(string key, long decrement = 1, int? dbIndex = null)
    {
        var result = default(long);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.StringDecrementAsync(key, decrement);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override bool HSet<T>(string key, string field, T value, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var data = value is string ? value.ToString() : value.ToJson();
            db.HashSet(key, field, data);
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override async Task<bool> HSetAsync<T>(string key, string field, T value, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var data = value is string ? value.ToString() : value.ToJson();
            await db.HashSetAsync(key, field, data);
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override bool HSet<T>(string key, Dictionary<string, T> fields, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var data = fields.Select(o =>
            {
                var value = o.Value is string ? o.Value.ToString() : o.Value.ToJson();
                return new HashEntry(o.Key, value);
            }).ToArray();
            db.HashSet(key, data);

            result = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override async Task<bool> HSetAsync<T>(string key, Dictionary<string, T> fields, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var data = fields.Select(o =>
            {
                var value = o.Value is string ? o.Value.ToString() : o.Value.ToJson();
                return new HashEntry(o.Key, value);
            }).ToArray();
            await db.HashSetAsync(key, data);

            result = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override long HDel(string key, int? dbIndex = null, params string[] fields)
    {
        var result = default(long);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.HashDelete(key, fields.Select(o => new RedisValue(o)).ToArray());
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override async Task<long> HDelAsync(string key, int? dbIndex = null, params string[] fields)
    {
        var result = default(long);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.HashDeleteAsync(key, fields.Select(o => new RedisValue(o)).ToArray());
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override T HGet<T>(string key, string field, int? dbIndex = null)
    {
        var result = default(T);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var value = db.HashGet(key, field);

            result = !value.HasValue || value.ToString().IsEmptyString() ? default(T) : (typeof(T).IsString() ? value.ToString().Convert<T>() : value.ToString().ToObject<T>());
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override async Task<T> HGetAsync<T>(string key, string field, int? dbIndex = null)
    {
        var result = default(T);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var value = await db.HashGetAsync(key, field);

            result = !value.HasValue || value.ToString().IsEmptyString() ? default(T) : (typeof(T).IsString() ? value.ToString().Convert<T>() : value.ToString().ToObject<T>());
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override Dictionary<string, T> HGet<T>(string key, int? dbIndex = null)
    {
        var result = new Dictionary<string, T>();
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var dic = db.HashGetAll(key);
            if (dic.IsEmpty()) return result;

            foreach (var kv in dic)
            {
                var field = kv.Key;
                var value = kv.Value;

                result[field] = !value.HasValue || value.ToString().IsEmptyString() ? default(T) : (typeof(T).IsString() ? value.ToString().Convert<T>() : value.ToString().ToObject<T>());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override async Task<Dictionary<string, T>> HGetAsync<T>(string key, int? dbIndex = null)
    {
        var result = new Dictionary<string, T>();
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var dic = await db.HashGetAllAsync(key);
            if (dic.IsEmpty()) return result;

            foreach (var kv in dic)
            {
                var field = kv.Key;
                var value = kv.Value;

                result[field] = !value.HasValue || value.ToString().IsEmptyString() ? default(T) : (typeof(T).IsString() ? value.ToString().Convert<T>() : value.ToString().ToObject<T>());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override Dictionary<string, T> HGet<T>(string key, int? dbIndex = null, params string[] fields)
    {
        var result = new Dictionary<string, T>();
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var values = db.HashGet(key, fields.Select(o => new RedisValue(o)).ToArray());
            if (values.IsEmpty()) return result;

            for (int i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                var value = values[i];

                result[field] = !value.HasValue || value.ToString().IsEmptyString() ? default(T) : (typeof(T).IsString() ? value.ToString().Convert<T>() : value.ToString().ToObject<T>());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override async Task<Dictionary<string, T>> HGetAsync<T>(string key, int? dbIndex = null, params string[] fields)
    {
        var result = new Dictionary<string, T>();
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var values = await db.HashGetAsync(key, fields.Select(o => new RedisValue(o)).ToArray());
            if (values.IsEmpty()) return result;

            for (int i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                var value = values[i];

                result[field] = !value.HasValue || value.ToString().IsEmptyString() ? default(T) : (typeof(T).IsString() ? value.ToString().Convert<T>() : value.ToString().ToObject<T>());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override bool HExists(string key, string field, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.HashExists(key, field);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override async Task<bool> HExistsAsync(string key, string field, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.HashExistsAsync(key, field);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override long HIncrease(string key, string field, long increment = 1, int? dbIndex = null)
    {
        var result = default(long);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.HashIncrement(key, field, increment);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override async Task<long> HIncreaseAsync(string key, string field, long increment = 1, int? dbIndex = null)
    {
        var result = default(long);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.HashIncrementAsync(key, field, increment);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override long HDecrease(string key, string field, long decrement = 1, int? dbIndex = null)
    {
        var result = default(long);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.HashDecrement(key, field, decrement);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }

    public override async Task<long> HDecreaseAsync(string key, string field, long decrement = 1, int? dbIndex = null)
    {
        var result = default(long);
        var db = default(IDatabase);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.HashDecrementAsync(key, field, decrement);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //if (db != null) db.Dispose();
        }

        return result;
    }
}