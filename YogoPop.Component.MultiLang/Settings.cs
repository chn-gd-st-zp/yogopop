namespace YogoPop.Component.MultiLang;

[DIModeForSettings("MultilangSettings", typeof(MultilangSettings))]
public class MultilangSettings : ICacheSettings
{
    /// <summary>
    /// 超时时间（分钟）
    /// </summary>
    public int TimeOutMinutes { get; set; }

    /// <summary>
    /// 缓存库
    /// </summary>
    public int DBIndex { get; set; }

    /// <summary>
    /// 前缀
    /// </summary>
    public string Prefix { get; set; }

    /// <summary>
    /// 数据源类型
    /// </summary>
    public MultiLangSourceEnum Type { get; set; }

    /// <summary>
    /// 数据源地址（配置文件地址 或 数据库链接地址）
    /// </summary>
    //[Decrypt(EncryptionNDecryptEnum.AES, EnvironmentEnum.DEV, EnvironmentEnum.SIT, EnvironmentEnum.PROD)]
    public virtual string Address { get; set; }
}

public class MultilangDefaultSettings : ISettings
{
    public string Language { get; set; }
}