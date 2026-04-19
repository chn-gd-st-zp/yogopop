namespace YogoPop.Component.Cache;

public abstract class RedisBasic : ICache4Redis
{
    private readonly int _defaultDatabase;
    protected int CurrentDatabase;
    protected readonly RedisSettings RedisSettings;

    public RedisBasic(RedisSettings redisSettings, int defaultDatabase = -1)
    {
        _defaultDatabase = defaultDatabase != -1 ? defaultDatabase : redisSettings.DBIndex;
        CurrentDatabase = _defaultDatabase;
        RedisSettings = redisSettings;
    }

    protected string FillKey(string key = default) => key.IsEmptyString() ? RedisSettings.Prefix : (key.StartsWith(RedisSettings.Prefix) ? key : $"{RedisSettings.Prefix}{key}");

    public void Dispose() => DisposeClient();

    public abstract void DisposeClient();

    public abstract T GetClient<T>(int? dbIndex = null);

    public RLock Acquire => InjectionContext.Resolve<RLock>();

    #region ICache

    public List<string> Keys(string pattern = "*") => Keys(pattern, null);

    public List<T> List<T>(string pattern = "*") => List<T>(pattern, null);

    public bool Expire(string key) => Expire(key, null);

    public bool Expire(string key, TimeSpan ts) => Expire(key, ts, null);

    public bool Expire(string key, DateTime dt) => Expire(key, dt, null);

    public bool Expire(string key, int second) => Expire(key, second, null);

    public bool Set<T>(string key, T value) => Set(key, value, null);

    public bool Set<T>(string key, T value, TimeSpan ts) => Set(key, value, ts, null);

    public bool Set<T>(string key, T value, DateTime dt) => Set(key, value, dt, null);

    public bool Set<T>(string key, T value, int seconds) => Set(key, value, seconds, null);

    public bool Del(string key) => Del(key, null);

    public T Get<T>(string key) => Get<T>(key, null);

    public bool Exists(string key) => Exists(key, null);

    #endregion

    #region ICache4Redis

    public abstract bool Expire(string key, int? dbIndex = null);

    public abstract Task<bool> ExpireAsync(string key, int? dbIndex = null);

    public abstract bool Expire(string key, TimeSpan ts, int? dbIndex = null);

    public abstract Task<bool> ExpireAsync(string key, TimeSpan ts, int? dbIndex = null);

    public virtual bool Expire(string key, DateTime dt, int? dbIndex = null) => Expire(key, dt - DateTimeExtension.Now, dbIndex);

    public virtual async Task<bool> ExpireAsync(string key, DateTime dt, int? dbIndex = null) => await ExpireAsync(key, dt - DateTimeExtension.Now, dbIndex);

    public virtual bool Expire(string key, int seconds, int? dbIndex = null) => Expire(key, DateTimeExtension.Now.AddSeconds(seconds), dbIndex);

    public virtual async Task<bool> ExpireAsync(string key, int seconds, int? dbIndex = null) => await ExpireAsync(key, DateTimeExtension.Now.AddSeconds(seconds), dbIndex);

    public abstract List<string> Keys(string pattern = "*", int? dbIndex = null);

    public abstract Task<List<string>> KeysAsync(string pattern = "*", int? dbIndex = null);

    public abstract List<T> List<T>(string pattern = "*", int? dbIndex = null);

    public abstract Task<List<T>> ListAsync<T>(string pattern = "*", int? dbIndex = null);

    public abstract bool ListRightPush(string key, string obj, DateTime? expiredTime = null, int? dbIndex = null);

    public abstract Task<bool> ListRightPushAsync(string key, string obj, DateTime? expiredTime = null, int? dbIndex = null);

    public abstract bool ListRemove(string key, string obj, long count = 0, int? dbIndex = null);

    public abstract Task<bool> ListRemoveAsync(string key, string obj, long count = 0, int? dbIndex = null);

    public abstract List<T> ListRange<T>(string key, int? dbIndex = null);

    public abstract Task<List<T>> ListRangeAsync<T>(string key, int? dbIndex = null);

    public abstract bool Set<T>(string key, T value, int? dbIndex = null);

    public abstract Task<bool> SetAsync<T>(string key, T value, int? dbIndex = null);

    public abstract bool Set<T>(string key, T value, TimeSpan ts, int? dbIndex = null);

    public abstract Task<bool> SetAsync<T>(string key, T value, TimeSpan ts, int? dbIndex = null);

    public virtual bool Set<T>(string key, T value, DateTime dt, int? dbIndex = null) => Set(key, value, dt - DateTimeExtension.Now, dbIndex);

    public virtual async Task<bool> SetAsync<T>(string key, T value, DateTime dt, int? dbIndex = null) => await SetAsync(key, value, dt - DateTimeExtension.Now, dbIndex);

    public virtual bool Set<T>(string key, T value, int seconds, int? dbIndex = null) => Set(key, value, DateTimeExtension.Now.AddSeconds(seconds), dbIndex);

    public virtual async Task<bool> SetAsync<T>(string key, T value, int seconds, int? dbIndex = null) => await SetAsync(key, value, DateTimeExtension.Now.AddSeconds(seconds), dbIndex);

    public abstract bool Del(string key, int? dbIndex = null);

    public abstract Task<bool> DelAsync(string key, int? dbIndex = null);

    public abstract T Get<T>(string key, int? dbIndex = null);

    public abstract Task<T> GetAsync<T>(string key, int? dbIndex = null);

    public abstract bool Exists(string key, int? dbIndex = null);

    public abstract Task<bool> ExistsAsync(string key, int? dbIndex = null);

    public abstract long Increase(string key, long increment = 1, int? dbIndex = null);

    public abstract Task<long> IncreaseAsync(string key, long increment = 1, int? dbIndex = null);

    public abstract long Decrease(string key, long decrement = 1, int? dbIndex = null);

    public abstract Task<long> DecreaseAsync(string key, long increment = 1, int? dbIndex = null);

    public abstract bool HSet<T>(string key, string field, T value, int? dbIndex = null);

    public abstract Task<bool> HSetAsync<T>(string key, string field, T value, int? dbIndex = null);

    public abstract bool HSet<T>(string key, Dictionary<string, T> fields, int? dbIndex = null);

    public abstract Task<bool> HSetAsync<T>(string key, Dictionary<string, T> fields, int? dbIndex = null);

    public abstract long HDel(string key, int? dbIndex = null, params string[] fields);

    public abstract Task<long> HDelAsync(string key, int? dbIndex = null, params string[] fields);

    public abstract T HGet<T>(string key, string field, int? dbIndex = null);

    public abstract Task<T> HGetAsync<T>(string key, string field, int? dbIndex = null);

    public abstract Dictionary<string, T> HGet<T>(string key, int? dbIndex = null);

    public abstract Task<Dictionary<string, T>> HGetAsync<T>(string key, int? dbIndex = null);

    public abstract Dictionary<string, T> HGet<T>(string key, int? dbIndex = null, params string[] fields);

    public abstract Task<Dictionary<string, T>> HGetAsync<T>(string key, int? dbIndex = null, params string[] fields);

    public abstract bool HExists(string key, string field, int? dbIndex = null);

    public abstract Task<bool> HExistsAsync(string key, string field, int? dbIndex = null);

    public abstract long HIncrease(string key, string field, long increment = 1, int? dbIndex = null);

    public abstract Task<long> HIncreaseAsync(string key, string field, long increment = 1, int? dbIndex = null);

    public abstract long HDecrease(string key, string field, long decrement = 1, int? dbIndex = null);

    public abstract Task<long> HDecreaseAsync(string key, string field, long increment = 1, int? dbIndex = null);

    #endregion
}