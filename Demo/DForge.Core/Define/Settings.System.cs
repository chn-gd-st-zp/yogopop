namespace DForge.Core.Define;

[DIModeForSettings("SystemSettings", typeof(SystemSettings))]
[DIModeForSettings("SystemSettings", typeof(IAccountRegex))]
public class SystemSettings : IAccountRegex
{
    /// <summary>
    /// 附件路径
    /// </summary>
    public string AttachmentDirectory { get; set; }

    /// <summary>
    /// 密码正则
    /// </summary>
    public string PasswordRegex { get; set; }

    /// <summary>
    /// 用户名正则
    /// </summary>
    public string UserNameRegex { get; set; }

    /// <summary>
    /// IP正则
    /// </summary>
    public string IPRegex { get; set; }

    /// <summary>
    /// 默认密码
    /// </summary>
    public string DefaultPassword { get; set; }

    /// <summary>
    /// 字典数据缓存标识
    /// </summary>
    public string KVDataCacheKey { get; set; }

    /// <summary>
    /// 字段数据缓存时长
    /// </summary>
    public int KVDataCacheMaintainMinutes { get; set; }
}