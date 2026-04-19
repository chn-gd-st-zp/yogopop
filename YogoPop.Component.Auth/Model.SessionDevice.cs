namespace YogoPop.Component.Auth;

public class YogoSessionDevice
{
    /// <summary>
    /// 登录设备
    /// </summary>
    public EntryEnum Entry { get; set; } = EntryEnum.None;

    /// <summary>
    /// IP地址
    /// </summary>
    public string IP { get; set; } = string.Empty;

    /// <summary>
    /// 推送标识
    /// </summary>
    public string PushToken { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; } = DateTimeExtension.Now;

    /// <summary>
    /// 刷新时间
    /// </summary>
    public DateTime UpdateTime { get; set; } = DateTimeExtension.Now;

    /// <summary>
    /// 过期时间
    /// </summary>
    public DateTime ExpiredTime { get; set; } = DateTimeExtension.Now;
}