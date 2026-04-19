namespace YogoPop.Core.VException;

[LogIgnore]
public class VEParamsValidation : VEBase
{
    public VEParamsValidation()
    {
        BaseType = IVEnum.Restore<IStateCode>();
        Code = BaseType.ParamsValidationFailed;
    }

    public VEParamsValidation(string msg)
    {
        BaseType = IVEnum.Restore<IStateCode>();
        Code = BaseType.ParamsValidationFailed;
        VMessage = msg;
    }
}