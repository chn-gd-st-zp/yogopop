namespace DForge.Core.Define;

[DIModeForSettings("RedisSettings", typeof(RedisSettings))]
public class RenewRedisSetting : RedisSettings
{
    [Decrypt(EncryptionNDecryptEnum.AES, EnvironmentEnum.DEV, EnvironmentEnum.SIT, EnvironmentEnum.PROD)]
    public override string Connection { get; set; }
}

[DIModeForSettings("LogicSupportSettings", typeof(LogicSupportSettings))]
public class LogicSupportSettings : ICacheSettings
{
    public int DBIndex { get; set; }

    public string Prefix { get; set; }

    /// <summary>
    /// field: 币种 <br />
    /// value: TBAppCurrency
    /// </summary>
    public static CacheDuration Currency => new CacheDuration("System:Currency", CacheExpiredDefine.Forever);
}

public static class RedisLock
{
    public static string IdenLock(string key, string code) => string.Format("lock:IdenLock:{0}_{1}", key, code);

    public static string LSLock(string key) => string.Format("lock:LSLock:{0}", key);
}

public static class CacheExpiredDefine
{
    public static int Forever => GlobalSettings.Unlimited;

    public static int OneDay => 24 * 60 * 60;
}

public static class CacheObjectExtension
{
    public static async Task<bool> DelAllAsync(this CacheDuration cacheObject, string tag = default)
    {
        var result = false;

        using (var cache = InjectionContext.Resolve<LogicSupportSettings>().ResolveCache<ICache4Redis>())
        {
            var key = tag.IsEmptyString() ? cacheObject.Key : cacheObject.Key.Format(tag);

            using (var client = cache.GetClient<RedisClient>())
            using (var lsLock = client.Lock(RedisLock.LSLock(key), 1, false))
            {
                if (lsLock == null) return result;

                await cache.DelAsync(key);
            }
        }

        result = true;
        return result;
    }

    public static async Task<bool> HDelAsync(this CacheDuration cacheObject, string field)
    {
        var result = false;

        using (var cache = InjectionContext.Resolve<LogicSupportSettings>().ResolveCache<ICache4Redis>())
        {
            var key = cacheObject.Key;

            using (var client = cache.GetClient<RedisClient>())
            using (var lsLock = client.Lock(RedisLock.LSLock(key), 1, false))
            {
                if (lsLock == null) return result;

                await cache.HDelAsync(key, fields: field);
            }
        }

        result = true;
        return result;
    }

    public static async Task<bool> HDelAsync(this CacheDuration cacheObject, string tag, string field)
    {
        var result = false;

        using (var cache = InjectionContext.Resolve<LogicSupportSettings>().ResolveCache<ICache4Redis>())
        {
            var key = cacheObject.Key.Format(tag);

            using (var client = cache.GetClient<RedisClient>())
            using (var lsLock = client.Lock(RedisLock.LSLock(key), 1, false))
            {
                if (lsLock == null) return result;

                await cache.HDelAsync(key, fields: field);
            }
        }

        result = true;
        return result;
    }

    public static async Task<bool> SetAsync<T>(this CacheDuration cacheObject, string tag, T obj)
    {
        var result = false;

        using (var cache = InjectionContext.Resolve<LogicSupportSettings>().ResolveCache<ICache4Redis>())
        {
            var key = tag.IsEmptyString() ? cacheObject.Key : cacheObject.Key.Format(tag);

            using (var client = cache.GetClient<RedisClient>())
            using (var lsLock = client.Lock(RedisLock.LSLock(key), 1, false))
            {
                if (lsLock == null) return result;

                if (cacheObject.Timeout == CacheExpiredDefine.Forever)
                    result = await cache.SetAsync(key, obj);
                else
                    result = await cache.SetAsync(key, obj, cacheObject.Timeout);
            }
        }

        return result;
    }

    public static async Task<bool> HSetAsync<T>(this CacheDuration cacheObject, string field, T obj)
    {
        var result = false;

        using (var cache = InjectionContext.Resolve<LogicSupportSettings>().ResolveCache<ICache4Redis>())
        {
            var key = cacheObject.Key;

            using (var client = cache.GetClient<RedisClient>())
            using (var lsLock = client.Lock(RedisLock.LSLock(key), 1, false))
            {
                if (lsLock == null) return result;

                if (obj == null)
                {
                    await cache.HDelAsync(key, fields: field);
                    result = true;
                }
                else
                {
                    if (cacheObject.Timeout == CacheExpiredDefine.Forever)
                        result = await cache.HSetAsync(key, field, obj);
                    else
                    {
                        result = await cache.HSetAsync(key, field, obj);
                        await cache.ExpireAsync(key, cacheObject.Timeout);
                    }
                }
            }
        }

        return result;
    }

    public static async Task<bool> HSetAsync<T>(this CacheDuration cacheObject, string tag, string field, T obj)
    {
        var result = false;

        using (var cache = InjectionContext.Resolve<LogicSupportSettings>().ResolveCache<ICache4Redis>())
        {
            var key = cacheObject.Key.Format(tag);

            using (var client = cache.GetClient<RedisClient>())
            using (var lsLock = client.Lock(RedisLock.LSLock(key), 1, false))
            {
                if (lsLock == null) return result;

                if (obj == null)
                {
                    await cache.HDelAsync(key, fields: field);
                    result = true;
                }
                else
                {
                    if (cacheObject.Timeout == CacheExpiredDefine.Forever)
                        result = await cache.HSetAsync(key, field, obj);
                    else
                    {
                        result = await cache.HSetAsync(key, field, obj);
                        await cache.ExpireAsync(key, cacheObject.Timeout);
                    }
                }

            }
        }

        return result;
    }

    public static async Task<T> GetAsync<T>(this CacheDuration cacheObject, string tag = default)
    {
        using (var cache = InjectionContext.Resolve<LogicSupportSettings>().ResolveCache<ICache4Redis>())
        {
            var key = tag.IsEmptyString() ? cacheObject.Key : cacheObject.Key.Format(tag);

            return await cache.GetAsync<T>(key);
        }
    }

    public static async Task<T> HGetAsync<T>(this CacheDuration cacheObject, string field)
    {
        using (var cache = InjectionContext.Resolve<LogicSupportSettings>().ResolveCache<ICache4Redis>())
        {
            var key = cacheObject.Key;

            return await cache.HGetAsync<T>(key, field);
        }
    }

    public static async Task<T> HGetAsync<T>(this CacheDuration cacheObject, string tag, string field)
    {
        using (var cache = InjectionContext.Resolve<LogicSupportSettings>().ResolveCache<ICache4Redis>())
        {
            var key = cacheObject.Key.Format(tag);

            return await cache.HGetAsync<T>(key, field);
        }
    }

    public static async Task<Dictionary<string, T>> HGetAllAsync<T>(this CacheDuration cacheObject, string tag = default)
    {
        using (var cache = InjectionContext.Resolve<LogicSupportSettings>().ResolveCache<ICache4Redis>())
        {
            var key = tag.IsEmptyString() ? cacheObject.Key : cacheObject.Key.Format(tag);

            return await cache.HGetAsync<T>(key);
        }
    }

    public static async Task<long> IncreaseAsync(this CacheDuration cacheObject, string tag = default, decimal value = 1)
    {
        using (var cache = InjectionContext.Resolve<LogicSupportSettings>().ResolveCache<ICache4Redis>())
        {
            var key = tag.IsEmptyString() ? cacheObject.Key : cacheObject.Key.Format(tag);

            return await cache.IncreaseAsync(key, (long)value);
        }
    }

    public static async Task<long> DecreaseAsync(this CacheDuration cacheObject, string tag = default, decimal value = 1)
    {
        using (var cache = InjectionContext.Resolve<LogicSupportSettings>().ResolveCache<ICache4Redis>())
        {
            var key = tag.IsEmptyString() ? cacheObject.Key : cacheObject.Key.Format(tag);

            return await cache.DecreaseAsync(key, (long)value);
        }
    }

    public static async Task<long> HIncreaseAsync(this CacheDuration cacheObject, string tag, string field, decimal value = 1)
    {
        using (var cache = InjectionContext.Resolve<LogicSupportSettings>().ResolveCache<ICache4Redis>())
        {
            var key = cacheObject.Key.Format(tag);

            return await cache.HIncreaseAsync(key, field, (long)value);
        }
    }

    public static async Task<long> HDecreaseAsync(this CacheDuration cacheObject, string tag, string field, decimal value = 1)
    {
        using (var cache = InjectionContext.Resolve<LogicSupportSettings>().ResolveCache<ICache4Redis>())
        {
            var key = cacheObject.Key.Format(tag);

            return await cache.HDecreaseAsync(key, field, (long)value);
        }
    }
}