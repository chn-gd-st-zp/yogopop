namespace DForge.Core.Enum;

/// <summary>
/// 缓存锁
/// </summary>
public enum CacheLockEnum
{
    [Description("默认、无")]
    None,

    [Description("账号密钥")]
    AccoutSecretLock,

    [Description("用户名")]
    UserNameLock,
}