namespace YogoPop.Component.Cache;

[DIModeForService(DIModeEnum.AsSelf)]
public class FRedisProvider : ISingleton
{
    private readonly object _lock = new();
    private RedisClient _client { get; set; }

    public RedisClient GetClient()
    {
        var redisSettings = InjectionContext.Resolve<RedisSettings>();
        if (redisSettings == null) return default;
        if (redisSettings.ConnectionList.IsEmpty()) return default;

        var options = redisSettings.ConnectionList.Select(o =>
        {
            var option = new ConnectionStringBuilder
            {
                Host = o,
                Database = redisSettings.DBIndex,
                IdleTimeout = TimeSpan.FromSeconds(redisSettings.IdelTimeout),
                ConnectTimeout = TimeSpan.FromSeconds(30),
                SendTimeout = TimeSpan.FromSeconds(30),
                ReceiveTimeout = TimeSpan.FromSeconds(30),
            };

            if (redisSettings.PoolSize != default)
            {
                option.MaxPoolSize = redisSettings.PoolSize;
                option.MinPoolSize = (int)(redisSettings.PoolSize * 0.6);
            }

            return option;

        }).ToArray();
        if (options.IsEmpty()) return default;

        if (redisSettings.PoolSize == default)
        {
            return options.Length == 1 ? new RedisClient(options[0]) : new RedisClient(options);
        }
        else
        {
            lock (_lock)
            {
                if (_client != null) return _client;
                _client = options.Length == 1 ? new RedisClient(options[0]) : new RedisClient(options);
                return _client;
            }
        }
    }
}

public class FRedis : RedisBasic
{
    public FRedis(RedisSettings redisSettings, int defaultDatabase = -1) : base(redisSettings, defaultDatabase) { }

    private RedisClient _client { get; set; }

    public override void DisposeClient() { if (_client != null) { _client.Dispose(); _client = null; } }

    private RedisClient getClient(int? dbIndex = null)
    {
        var result = default(RedisClient);

        if (RedisSettings.PoolSize == default)
        {
            if (_client == null) _client = InjectionContext.Resolve<FRedisProvider>().GetClient();
            result = _client;
        }
        else
        {
            result = InjectionContext.Resolve<FRedisProvider>().GetClient();
        }

        return result.GetDatabase(dbIndex.HasValue ? dbIndex.Value : CurrentDatabase);
    }

    public override T GetClient<T>(int? dbIndex = null) => getClient(dbIndex).Convert<T>();

    public override bool Expire(string key, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.Expire(key, 0);
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.ExpireAsync(key, 0);
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.Expire(key, ts);
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.ExpireAsync(key, ts);
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            var keys = db.Keys(pattern);
            if (keys.IsEmpty()) return result;

            result = keys.ToList();
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            var keys = await db.KeysAsync(pattern);
            if (keys.IsEmpty()) return result;

            result = keys.ToList();
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            var keys = db.Keys(pattern);
            if (keys.IsEmpty()) return result;

            keys.ToList().ForEach(o => { result.Add(db.Get<T>(o)); });
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            var keys = await db.KeysAsync(pattern);
            if (keys.IsEmpty()) return result;

            keys.ToList().ForEach(o => { result.Add(db.Get<T>(o)); });
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.RPush(key, obj) == 1;

            if (expiredTime.HasValue)
                db.ExpireAt(key, expiredTime.Value);
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.RPushAsync(key, obj) == 1;

            if (expiredTime.HasValue)
                await db.ExpireAtAsync(key, expiredTime.Value);
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.LRem(key, count, obj) != default;
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.LRemAsync(key, count, obj) != default;
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var values = db.LRange(key, 0, -1);
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var values = await db.LRangeAsync(key, 0, -1);
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var data = value is string ? value.ToString() : value.ToJson();
            db.Set(key, data);

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

    public override async Task<bool> SetAsync<T>(string key, T value, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var data = value is string ? value.ToString() : value.ToJson();
            await db.SetAsync(key, data);

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

    public override bool Set<T>(string key, T value, TimeSpan ts, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var data = value is string ? value.ToString() : value.ToJson();
            db.Set(key, data, ts);

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

    public override async Task<bool> SetAsync<T>(string key, T value, TimeSpan ts, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var data = value is string ? value.ToString() : value.ToJson();
            await db.SetAsync(key, data, (int)ts.TotalSeconds);

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

    public override bool Del(string key, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.Del(key) != default;
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.DelAsync(key) != default;
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var value = db.Get(key);

            result = value.IsEmptyString() ? default(T) : (typeof(T).IsString() ? value.Convert<T>() : value.ToObject<T>());
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var value = await db.GetAsync(key);

            result = value.IsEmptyString() ? default(T) : (typeof(T).IsString() ? value.Convert<T>() : value.ToObject<T>());
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.Exists(key);
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.ExistsAsync(key);
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.IncrBy(key, increment);
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.IncrByAsync(key, increment);
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.IncrBy(key, MathHelper.ToNegative(decrement).Convert<long>());
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.IncrByAsync(key, MathHelper.ToNegative(decrement).Convert<long>());
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var data = value is string ? value.ToString() : value.ToJson();
            db.HSet(key, field, data);

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

    public override async Task<bool> HSetAsync<T>(string key, string field, T value, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var data = value is string ? value.ToString() : value.ToJson();
            await db.HSetAsync(key, field, data);

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

    public override bool HSet<T>(string key, Dictionary<string, T> fields, int? dbIndex = null)
    {
        var result = default(bool);
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var data = fields.ToDictionary(
                field => field.Key,
                field => field.Value is string ? field.Value.ToString() : field.Value.ToJson()
            );
            db.HMSet(key, data);

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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var data = fields.ToDictionary(
                field => field.Key,
                field => field.Value is string ? field.Value.ToString() : field.Value.ToJson()
            );
            await db.HMSetAsync(key, data);

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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.HDel(key, fields);
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.HDelAsync(key, fields);
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var value = db.HGet(key, field);

            result = value.IsEmptyString() ? default(T) : (typeof(T).IsString() ? value.Convert<T>() : value.ToObject<T>());
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var value = await db.HGetAsync(key, field);

            result = value.IsEmptyString() ? default(T) : (typeof(T).IsString() ? value.Convert<T>() : value.ToObject<T>());
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var dic = db.HGetAll(key);
            if (dic.IsEmpty()) return result;

            foreach (var kv in dic)
            {
                var field = kv.Key;
                var value = kv.Value;

                result[field] = value.IsEmptyString() ? default(T) : (typeof(T).IsString() ? value.Convert<T>() : value.ToObject<T>());
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var dic = await db.HGetAllAsync(key);
            if (dic.IsEmpty()) return result;

            foreach (var kv in dic)
            {
                var field = kv.Key;
                var value = kv.Value;

                result[field] = value.IsEmptyString() ? default(T) : (typeof(T).IsString() ? value.Convert<T>() : value.ToObject<T>());
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var values = db.HMGet(key, fields);
            if (values.IsEmpty()) return result;

            for (int i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                var value = values[i];

                result[field] = value.IsEmptyString() ? default(T) : (typeof(T).IsString() ? value.Convert<T>() : value.ToObject<T>());
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            var values = await db.HMGetAsync(key, fields);
            if (values.IsEmpty()) return result;

            for (int i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                var value = values[i];

                result[field] = value.IsEmptyString() ? default(T) : (typeof(T).IsString() ? value.Convert<T>() : value.ToObject<T>());
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.HExists(key, field);
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.HExistsAsync(key, field);
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.HIncrBy(key, field, increment);
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.HIncrByAsync(key, field, increment);
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = db.HIncrBy(key, field, MathHelper.ToNegative(decrement).Convert<long>());
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
        var db = default(RedisClient);

        try
        {
            db = getClient(dbIndex);
            if (db == null) return result;

            key = FillKey(key);

            result = await db.HIncrByAsync(key, field, MathHelper.ToNegative(decrement).Convert<long>());
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