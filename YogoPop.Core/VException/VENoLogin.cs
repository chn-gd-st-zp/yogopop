namespace YogoPop.Core.VException;

[LogIgnore]
public class VENoLogin : VEBase
{
    public VENoLogin()
    {
        BaseType = IVEnum.Restore<IStateCode>();
        Code = BaseType.NoLogin;
    }
}