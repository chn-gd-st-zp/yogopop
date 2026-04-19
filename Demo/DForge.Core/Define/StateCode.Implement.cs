namespace DForge.Infrastructure.Define;

[DIModeForService(DIModeEnum.Exclusive, typeof(IStateCode))]
[DIModeForService(DIModeEnum.Exclusive, typeof(IRenewStateCode))]
public class RenewStateCode : StateCodeEnum, IRenewStateCode
{
    public RenewStateCode()
    {
        _sysConfigError = Factory.NewEnum(nameof(IRenewStateCode.SysConfigError), 11001);
        _sysMaintaining = Factory.NewEnum(nameof(IRenewStateCode.SysMaintaining), 11002);
    }

    public IVEnumItem SysConfigError { get { return _sysConfigError; } }
    private IVEnumItem _sysConfigError;

    public IVEnumItem SysMaintaining { get { return _sysMaintaining; } }
    private IVEnumItem _sysMaintaining;
}