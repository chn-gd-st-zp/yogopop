namespace YogoPop.Core.StateCode;

public interface IStateCode : IVEnum
{
    [Description("操作成功")]
    public IVEnumItem Success { get; }

    [Description("操作失败")]
    public IVEnumItem Fail { get; }

    [Description("系统错误")]
    public IVEnumItem SysError { get; }

    [Description("您所发送的内容中包含敏感或非法的文字或链接")]
    public IVEnumItem SensitiveContent { get; }

    [Description("Api版本限制")]
    public IVEnumItem ApiVersionRestrict { get; }

    [Description("参数校验失败")]
    public IVEnumItem ParamsValidationFailed { get; }

    [Description("空令牌")]
    public IVEnumItem EmptyToken { get; }

    [Description("未登录")]
    public IVEnumItem NoLogin { get; }

    [Description("没有权限")]
    public IVEnumItem NoPermission { get; }
}