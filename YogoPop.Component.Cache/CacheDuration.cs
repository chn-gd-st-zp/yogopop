namespace YogoPop.Component.Cache;

public struct CacheDuration
{
    public string Key { get; set; }

    public int Timeout { get; set; }

    public CacheDuration(string key, int timeout) { Key = key; Timeout = timeout; }
}