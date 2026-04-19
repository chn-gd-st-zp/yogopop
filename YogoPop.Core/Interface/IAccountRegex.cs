namespace YogoPop.Core.Interface;

public interface IAccountRegex : ISettings
{
    /// <summary>
    /// 用户名正则
    /// </summary>
    public string UserNameRegex { get; set; }

    /// <summary>
    /// 密码正则
    /// </summary>
    public string PasswordRegex { get; set; }
}