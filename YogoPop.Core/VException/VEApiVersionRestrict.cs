namespace YogoPop.Core.VException;

[LogIgnore]
public class VEApiVersionRestrict : VEBase
{
    public VEApiVersionRestrict()
    {
        BaseType = IVEnum.Restore<IStateCode>();
        Code = BaseType.ApiVersionRestrict;
    }
}