namespace YogoPop.Core.VException;

[LogIgnore]
public class VENoPermission : VEBase
{
    public VENoPermission()
    {
        BaseType = IVEnum.Restore<IStateCode>();
        Code = BaseType.NoPermission;
    }
}