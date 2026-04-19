namespace YogoPop.Component.Cache;

[DIModeForService(DIModeEnum.AsSelf)]
public class RLock : ISingleton
{
    public RedLockFactory Factory { get; private set; }

    public RLock(RedisSettings redisSettings)
    {
        var multiplexers = new List<RedLockMultiplexer>();

        foreach (var connection in redisSettings.ConnectionList)
            multiplexers.Add(ConnectionMultiplexer.Connect(connection));

        Factory = RedLockFactory.Create(multiplexers);
    }

    public IRedLock Lock(string key, int waitSecond) => Lock(key, waitSecond, cancellationToken: null);

    public IRedLock Lock(string key, int waitSecond = 3, int expirySecond = 30, int retryMSecond = 200, CancellationToken? cancellationToken = null) => Factory.CreateLock(key, TimeSpan.FromSeconds(expirySecond), TimeSpan.FromSeconds(waitSecond), TimeSpan.FromMilliseconds(retryMSecond), cancellationToken);

    public async Task<IRedLock> LockAsync(string key, int waitSecond) => await LockAsync(key, waitSecond, cancellationToken: null);

    public async Task<IRedLock> LockAsync(string key, int waitSecond = 3, int expirySecond = 30, int retryMSecond = 200, CancellationToken? cancellationToken = null) => await Factory.CreateLockAsync(key, TimeSpan.FromSeconds(expirySecond), TimeSpan.FromSeconds(waitSecond), TimeSpan.FromMilliseconds(retryMSecond), cancellationToken);
}

public static class RLockExtension
{
    public static bool IsAcquired(this IRedLock? rlock) => !rlock.IsNotAcquired();

    public static bool IsNotAcquired(this IRedLock? rlock) => rlock == null || !rlock.IsAcquired;
}