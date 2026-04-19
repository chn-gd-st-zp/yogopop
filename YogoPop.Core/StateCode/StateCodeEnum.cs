namespace YogoPop.Core.StateCode;

[DIModeForService(DIModeEnum.Exclusive, typeof(IStateCode))]
public class StateCodeEnum : VEnum, IStateCode
{
    public StateCodeEnum()
    {
        _success = Factory.NewEnum(nameof(IStateCode.Success), 200);
        _fail = Factory.NewEnum(nameof(IStateCode.Fail), 400);
        _sysError = Factory.NewEnum(nameof(IStateCode.SysError), 500);
        _sensitiveContent = Factory.NewEnum(nameof(IStateCode.SensitiveContent), 600);
        _apiVersionRestrict = Factory.NewEnum(nameof(IStateCode.ApiVersionRestrict), 700);

        _paramsValidationFailed = Factory.NewEnum(nameof(IStateCode.ParamsValidationFailed), 1001);
        _emptyToken = Factory.NewEnum(nameof(IStateCode.EmptyToken), 1002);
        _noLogin = Factory.NewEnum(nameof(IStateCode.NoLogin), 1003);
        _noPermission = Factory.NewEnum(nameof(IStateCode.NoPermission), 1004);
    }

    public IVEnumItem Success { get { return _success; } }
    private IVEnumItem _success;

    public IVEnumItem Fail { get { return _fail; } }
    private IVEnumItem _fail;

    public IVEnumItem SysError { get { return _sysError; } }
    private IVEnumItem _sysError;

    public IVEnumItem SensitiveContent { get { return _sensitiveContent; } }
    private IVEnumItem _sensitiveContent;

    public IVEnumItem ApiVersionRestrict { get { return _apiVersionRestrict; } }
    private IVEnumItem _apiVersionRestrict;


    public virtual IVEnumItem ParamsValidationFailed { get { return _paramsValidationFailed; } }
    private IVEnumItem _paramsValidationFailed;

    public virtual IVEnumItem EmptyToken { get { return _emptyToken; } }
    private IVEnumItem _emptyToken;

    public virtual IVEnumItem NoLogin { get { return _noLogin; } }
    private IVEnumItem _noLogin;

    public virtual IVEnumItem NoPermission { get { return _noPermission; } }
    private IVEnumItem _noPermission;
}