namespace YogoPop.Core.VException;

[LogIgnore]
public class VEEmptyToken : VEBase
{
    public VEEmptyToken()
    {
        BaseType = IVEnum.Restore<IStateCode>();
        Code = BaseType.EmptyToken;
    }
}