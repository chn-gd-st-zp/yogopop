namespace YogoPop.Core.VException;

[LogIgnore]
public class VESensitiveContent : VEBase
{
    public VESensitiveContent()
    {
        BaseType = IVEnum.Restore<IStateCode>();
        Code = BaseType.SensitiveContent;
    }
}