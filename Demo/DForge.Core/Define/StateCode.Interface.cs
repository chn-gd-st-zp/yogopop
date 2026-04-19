namespace DForge.Infrastructure.Define;

public interface IRenewStateCode : IStateCode
{
    [Description("系统配置错误")]
    public IVEnumItem SysConfigError { get; }

    [Description("系统维护中")]
    public IVEnumItem SysMaintaining { get; }
}