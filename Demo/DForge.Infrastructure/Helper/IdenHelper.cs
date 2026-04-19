namespace DForge.Infrastructure.Helper;

public delegate Task<bool> IdenExistenceunc(IDBRepository repository, string iden);
public delegate Task<string> IdenGenerateFunc(IDBRepository repository, params object[] args);

[DIModeForService(DIModeEnum.AsSelf)]
public class IdenHelper : ITransient
{
    public const string _keyPattern = "IdenOccupy:{0}:{1}";

    public async Task<string> CheckIdenCode(Enum cacheLockEnum, IdenExistenceunc idenExistenceFunc, string code)
    {
        using (var cache = InjectionContext.Resolve<ICache4Redis>())
        using (var client = cache.GetClient<RedisClient>())
        using (var repository = InjectionContext.Resolve<IDBRepository>())
        {
            if (await cache.ExistsAsync(_keyPattern.Format(cacheLockEnum, code)))
                return string.Empty;

            using (var idenLock = client.Lock(RedisLock.IdenLock(cacheLockEnum.ToString(), code), 1, false))
            {
                if (idenLock == null)
                    return string.Empty;

                if (await idenExistenceFunc(repository, code))
                    return string.Empty;

                await cache.SetAsync(_keyPattern.Format(cacheLockEnum, code), code, DateTimeExtension.Now.AddMinutes(5));
            }
        }

        return code;
    }

    public async Task<string> CheckIdenCode(Enum cacheLockEnum, IdenExistenceunc idenExistenceFunc, IdenGenerateFunc idenGenerateFunc, params object[] args)
    {
        var code = string.Empty;

        using (var cache = InjectionContext.Resolve<ICache4Redis>())
        using (var client = cache.GetClient<RedisClient>())
        using (var repository = InjectionContext.Resolve<IDBRepository>())
        {
            while (true)
            {
                code = await idenGenerateFunc(repository, args);

                if (await cache.ExistsAsync(_keyPattern.Format(cacheLockEnum, code)))
                {
                    Thread.Sleep(50);
                    continue;
                }
                ;

                using (var idenLock = client.Lock(RedisLock.IdenLock(cacheLockEnum.ToString(), code), 1, false))
                {
                    if (idenLock == null)
                    {
                        Thread.Sleep(50);
                        continue;
                    }

                    if (await idenExistenceFunc(repository, code))
                    {
                        Thread.Sleep(50);
                        continue;
                    }

                    await cache.SetAsync(_keyPattern.Format(cacheLockEnum, code), code, DateTimeExtension.Now.AddMinutes(5));
                    break;
                }
            }
        }

        return code;
    }

    public void ReleaseIdenCode(Enum cacheLockEnum, string code)
    {
        var key = _keyPattern.Format(cacheLockEnum, code);

        using (var cache = InjectionContext.Resolve<ICache>())
            if (cache.Exists(key)) cache.Del(key);
    }

    public async Task<bool> AccountSecretExistenceFunc(IDBRepository repository, string code) => await repository.DBContext.AnyAsync<TBAccountInfo>(o => o.Secret == code);

    public async Task<string> AccountSecretGenerateFunc(IDBRepository repository, params object[] args) => Unique.GetRandomCode4(6);

    public async Task<bool> UserNameExistenceFunc(IDBRepository repository, string code) => await repository.DBContext.AnyAsync<TBAccountInfo>(o => o.UserName == code);

    public async Task<string> GenerateCustomerSecret() => Unique.GetRandomCode3(32);
}