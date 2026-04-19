namespace DForge.Infrastructure.VException;

[LogIgnore]
[Description("提示信息-系统维护中")]
public class VESysMaintaining : VEBase
{
    public VESysMaintaining()
    {
        var baseType = IVEnum.Restore<IRenewStateCode>();
        BaseType = baseType;
        Code = baseType.SysMaintaining;
    }
}