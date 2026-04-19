namespace YogoPop.Component.Cache;

public interface ICacheSettings : ISettings
{
    public int DBIndex { get; set; }

    public string Prefix { get; set; }
}

public interface ICache : IDisposable
{
    public List<string> Keys(string pattern = "*");

    public List<T> List<T>(string pattern = "*");

    public bool Expire(string key);

    public bool Expire(string key, TimeSpan ts);

    public bool Expire(string key, DateTime dt);

    public bool Expire(string key, int second);

    public bool Set<T>(string key, T value);

    public bool Set<T>(string key, T value, TimeSpan ts);

    public bool Set<T>(string key, T value, DateTime dt);

    public bool Set<T>(string key, T value, int second);

    public T Get<T>(string key);

    public bool Exists(string key);

    public bool Del(string key);
}

public interface ICache4Redis : ICache
{
    public RLock Acquire { get; }

    public T GetClient<T>(int? dbIndex = null);

    public bool Expire(string key, int? dbIndex = null);

    public Task<bool> ExpireAsync(string key, int? dbIndex = null);

    public bool Expire(string key, TimeSpan ts, int? dbIndex = null);

    public Task<bool> ExpireAsync(string key, TimeSpan ts, int? dbIndex = null);

    public bool Expire(string key, DateTime dt, int? dbIndex = null);

    public Task<bool> ExpireAsync(string key, DateTime dt, int? dbIndex = null);

    public bool Expire(string key, int seconds, int? dbIndex = null);

    public Task<bool> ExpireAsync(string key, int seconds, int? dbIndex = null);

    public List<string> Keys(string pattern = "*", int? dbIndex = null);

    public Task<List<string>> KeysAsync(string pattern = "*", int? dbIndex = null);

    public List<T> List<T>(string pattern = "*", int? dbIndex = null);

    public Task<List<T>> ListAsync<T>(string pattern = "*", int? dbIndex = null);

    public bool ListRightPush(string key, string obj, DateTime? expiredTime = null, int? dbIndex = null);

    public Task<bool> ListRightPushAsync(string key, string obj, DateTime? expiredTime = null, int? dbIndex = null);

    public bool ListRemove(string key, string obj, long count = 0, int? dbIndex = null);

    public Task<bool> ListRemoveAsync(string key, string obj, long count = 0, int? dbIndex = null);

    public List<T> ListRange<T>(string key, int? dbIndex = null);

    public Task<List<T>> ListRangeAsync<T>(string key, int? dbIndex = null);

    public bool Set<T>(string key, T value, int? dbIndex = null);

    public Task<bool> SetAsync<T>(string key, T value, int? dbIndex = null);

    public bool Set<T>(string key, T value, TimeSpan ts, int? dbIndex = null);

    public Task<bool> SetAsync<T>(string key, T value, TimeSpan ts, int? dbIndex = null);

    public bool Set<T>(string key, T value, DateTime dt, int? dbIndex = null);

    public Task<bool> SetAsync<T>(string key, T value, DateTime dt, int? dbIndex = null);

    public bool Set<T>(string key, T value, int seconds, int? dbIndex = null);

    public Task<bool> SetAsync<T>(string key, T value, int seconds, int? dbIndex = null);

    public bool Del(string key, int? dbIndex = null);

    public Task<bool> DelAsync(string key, int? dbIndex = null);

    public T Get<T>(string key, int? dbIndex = null);

    public Task<T> GetAsync<T>(string key, int? dbIndex = null);

    public bool Exists(string key, int? dbIndex = null);

    public Task<bool> ExistsAsync(string key, int? dbIndex = null);

    public long Increase(string key, long increment = 1, int? dbIndex = null);

    public Task<long> IncreaseAsync(string key, long increment = 1, int? dbIndex = null);

    public long Decrease(string key, long decrement = 1, int? dbIndex = null);

    public Task<long> DecreaseAsync(string key, long increment = 1, int? dbIndex = null);

    public bool HSet<T>(string key, string field, T value, int? dbIndex = null);

    public Task<bool> HSetAsync<T>(string key, string field, T value, int? dbIndex = null);

    public bool HSet<T>(string key, Dictionary<string, T> fields, int? dbIndex = null);

    public Task<bool> HSetAsync<T>(string key, Dictionary<string, T> fields, int? dbIndex = null);

    public long HDel(string key, int? dbIndex = null, params string[] fields);

    public Task<long> HDelAsync(string key, int? dbIndex = null, params string[] fields);

    public T HGet<T>(string key, string field, int? dbIndex = null);

    public Task<T> HGetAsync<T>(string key, string field, int? dbIndex = null);

    public Dictionary<string, T> HGet<T>(string key, int? dbIndex = null);

    public Task<Dictionary<string, T>> HGetAsync<T>(string key, int? dbIndex = null);

    public Dictionary<string, T> HGet<T>(string key, int? dbIndex = null, params string[] fields);

    public Task<Dictionary<string, T>> HGetAsync<T>(string key, int? dbIndex = null, params string[] fields);

    public bool HExists(string key, string field, int? dbIndex = null);

    public Task<bool> HExistsAsync(string key, string field, int? dbIndex = null);

    public long HIncrease(string key, string field, long increment = 1, int? dbIndex = null);

    public Task<long> HIncreaseAsync(string key, string field, long increment = 1, int? dbIndex = null);

    public long HDecrease(string key, string field, long decrement = 1, int? dbIndex = null);

    public Task<long> HDecreaseAsync(string key, string field, long increment = 1, int? dbIndex = null);
}