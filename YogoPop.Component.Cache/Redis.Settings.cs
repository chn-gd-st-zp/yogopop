namespace YogoPop.Component.Cache;

public class RedisSettings : ICacheSettings
{
    /// <summary>
    /// 默认缓存库
    /// </summary>
    public int DefaultDatabase { get; set; } = 0;

    /// <summary>
    /// 实例名
    /// </summary>
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// 缓存库
    /// </summary>
    public virtual int DBIndex { get { return DefaultDatabase; } set { DefaultDatabase = value; } }

    /// <summary>
    /// 前缀
    /// </summary>
    public virtual string Prefix { get { return InstanceName; } set { InstanceName = value; } }

    /// <summary>
    /// 闲置链接超时
    /// </summary>
    public virtual int IdelTimeout { get; set; } = 1;

    /// <summary>
    /// 链接池大小
    /// </summary>
    public virtual int PoolSize { get; set; } = default;

    /// <summary>
    /// 链接
    /// </summary>
    public virtual string Connection { get; set; }

    /// <summary>
    /// 链接
    /// </summary>
    public virtual string[] Connections { get; set; }

    /// <summary>
    /// 链接
    /// </summary>
    public List<string> ConnectionList
    {
        get
        {
            var result = new List<string>();

            if (Connection.IsNotEmptyString())
                result.Add(Connection);

            if (Connections.IsNotEmpty())
                result.AddRange(Connections);

            return result;
        }
    }
}