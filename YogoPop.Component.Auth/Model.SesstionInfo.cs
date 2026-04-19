namespace YogoPop.Component.Auth;

public class YogoSessionInfo
{
    public YogoSessionInfo()
    {
        CreateTime = DateTimeExtension.Now;
        UpdateTime = CreateTime;
        ExpiredTime = CreateTime;
    }

    /// <summary>
    /// AccessToken
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// 账号ID（从 AccountInfo 获取）
    /// </summary>
    public string AccountID => AccountInfo?.AccountID ?? string.Empty;

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName => AccountInfo?.UserName ?? string.Empty;

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName => AccountInfo?.NickName ?? string.Empty;

    /// <summary>
    /// 权限代码
    /// </summary>
    public string[] PermissionCodes { get; set; } = new string[0];

    /// <summary>
    /// 创建时间
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime { get { return DeviceInfo.CreateTime; } set { DeviceInfo.CreateTime = value; AccountInfo.CreateTime = value; } }

    /// <summary>
    /// 刷新时间
    /// </summary>
    [JsonIgnore]
    public DateTime UpdateTime { get { return DeviceInfo.UpdateTime; } set { DeviceInfo.UpdateTime = value; AccountInfo.UpdateTime = value; } }

    /// <summary>
    /// 过期时间
    /// </summary>
    [JsonIgnore]
    public DateTime ExpiredTime { get { return DeviceInfo.ExpiredTime; } set { DeviceInfo.ExpiredTime = value; AccountInfo.ExpiredTime = value; } }

    /// <summary>
    /// 设备信息
    /// </summary>
    public YogoSessionDevice DeviceInfo { get; set; } = new YogoSessionDevice();

    /// <summary>
    /// 账号信息
    /// </summary>
    public YogoSessionAccount AccountInfo { get; set; } = new YogoSessionAccount();

    /// <summary>
    /// 续期
    /// </summary>
    /// <param name="cacheMaintainMinutes">minutes</param>
    public void Extenstion(int cacheMaintainMinutes)
    {
        DateTime now = DateTimeExtension.Now;

        DeviceInfo.UpdateTime = now;
        DeviceInfo.ExpiredTime = now.AddMinutes(cacheMaintainMinutes);
        AccountInfo.UpdateTime = now;
        AccountInfo.ExpiredTime = now.AddMinutes(cacheMaintainMinutes);
    }
}

public static class YogoSessionInfoExtension
{
    public static void SetExpiredTime(this YogoSessionInfo info)
    {
        var time = TimeSpan.FromMinutes(InjectionContext.Resolve<AuthSettings>().TimeOutMinutes);
        info.ExpiredTime = DateTimeExtension.Now.AddMinutes(time.TotalMinutes);
    }
}