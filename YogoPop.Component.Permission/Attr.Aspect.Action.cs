namespace YogoPop.Component.Permission;

[Aspect(Scope.PerInstance)]
public class PermissionAspectForAction : AOPAspectAsyncBase
{
    private IServiceScope _diScope;
    private IYogoLogger<PermissionAspectForAction> _logger;
    private IPermissionEnum _permissionEnum;
    private AccessRecord _accessRecord;

    [Advice(Kind.Around)]
    public new object HandleMethod(
       [Argument(Source.Instance)] object source,
       [Argument(Source.Target)] Func<object[], object> method,
       [Argument(Source.Triggers)] Attribute[] triggers,
       [Argument(Source.Name)] string actionName,
       [Argument(Source.Arguments)] object[] actionParams
    )
    {
        using var diScope = InjectionContext.Root.CreateScope();
        _diScope = diScope;
        _logger = _diScope.Resolve<IYogoLogger<PermissionAspectForAction>>();
        _permissionEnum = _diScope.Resolve<IPermissionEnum>();

        return base.HandleMethod(source, method, triggers, actionName, actionParams);
    }

    protected override async Task Before(object source, MethodInfo methodInfo, Attribute[] triggers, string actionName, object[] actionParams)
    {
        if (AppInitHelper.IsTestMode)
            return;

        try
        {

            var permission = default(IPermission);

            var type = source.GetType();
            if (!type.IsImplementedOf(typeof(IYogoService)))
                throw new Exception("运行出错[权限认证1]");

            var attr = triggers.Where(o => o.GetType().IsExtendOf<ActionPermissionBaseAttribute>())
                .Select(o => o as ActionPermissionBaseAttribute)
                .Where(o => o.Type == PermissionTypeEnum.Action)
                .FirstOrDefault();
            if (attr == null)
                throw new Exception("运行出错[权限认证2]");

            var tokenProvider = InjectionContext.Resolve(type.GetGenericArguments()[0]) as ITokenProvider;
            if (tokenProvider == null)
                throw new Exception("运行出错[权限认证3]");

            var session = InjectionContext.ResolveByKeyed<IYogoSession>(tokenProvider.Protocol);
            if (session == null)
                throw new Exception("运行出错[权限认证4]");

            if (session.SessionContext == null || _permissionEnum == null)
                throw new Exception("运行出错[权限认证5]");

            var otAttr = _permissionEnum.EnumType.GetAttr<OperationAttribute>(attr.Code);
            if (otAttr == null || otAttr.OperationType == OperationTypeEnum.None)
                throw new Exception("运行出错[权限认证6]");

            using (var repository = InjectionContext.Resolve<IPermissionRepository>())
            {
                if (repository == null) throw new Exception("运行出错[权限认证7]");

                permission = repository.Permission(attr.Code);
                if (permission == null) throw new Exception("运行出错[权限认证8]");
            }

            var sessionContext = session.SessionContext;
            sessionContext.OperationType = otAttr.OperationType;
            await sessionContext.SaveAsync();

            session.VerifyPermission(attr.Code);

            #region 记录

            var inputObj = actionParams.Where(o => o.GetType().IsImplementedOf<IDTOInput>()).FirstOrDefault();

            if (
                inputObj == null
                || permission.AccessLogger == false
                || attr.MappingType == null
                || attr.MappingType.DBDestinationType.IsImplementedOf<IAccessRecordTrigger>() == false
                || attr.MappingType.DBDestinationType.IsGenericOf(typeof(IDBFPrimaryKey<>)) == false
                || attr.MappingType.InputType.IsImplementedOf<IDTOInput>() == false
            )
                return;

            using (var repo_AccessRecordTrigger = InjectionContext.ResolveByKeyed<IAccessRecordTriggerRepository>(attr.MappingType.DBDestinationType))
            {
                if (repo_AccessRecordTrigger == null)
                    return;

                var pk = DTOExtension.GetPKValue(inputObj);
                var primaryKey = pk == null ? string.Empty : pk.ToString();
                var dbObj = primaryKey.IsEmptyString() ? null : repo_AccessRecordTrigger.GetTriggerObj(primaryKey);

                _accessRecord = new AccessRecord
                {
                    Code = attr.Code,
                    RoleType = session.CurrentAccount.AccountInfo.RoleType,
                    AccountID = session.CurrentAccount.AccountInfo.AccountID,
                    UserName = session.CurrentAccount.UserName,
                    OperationType = sessionContext.OperationType,
                    TBName = repo_AccessRecordTrigger.DBContext.GetTBName(attr.MappingType.DBDestinationType),
                    TBValue = attr.MappingType.DBDestinationType.GetDesc(),
                    PKName = repo_AccessRecordTrigger.DBContext.GetPKName(attr.MappingType.DBDestinationType),
                    PKValue = primaryKey,
                    TriggerName = dbObj == null ? string.Empty : dbObj.GetTriggerObjName(),
                    Descriptions = new List<AccessRecordDescription>(),
                };

                foreach (var inputProperty in attr.MappingType.InputType.GetProperties())
                {
                    if (inputProperty.GetCustomAttribute<LogIgnoreAttribute>() != null)
                        continue;

                    try
                    {
                        var record = new AccessRecordDescription
                        {
                            FieldName = inputProperty.Name,
                            FieldRemark = inputProperty.GetDesc(),
                            InputValue = inputProperty.GetValue(inputObj),
                            DBValue = dbObj.GetFieldValue(inputProperty.Name),
                        };

                        _accessRecord.Descriptions.Add(record);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }

            #endregion
        }
        catch (VEEmptyToken ex)
        {
            throw ex;
        }
        catch (VENoLogin ex)
        {
            throw ex;
        }
        catch (VENoPermission ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
            throw ex;
        }
    }

    protected override async Task<object> After(object source, MethodInfo methodInfo, Attribute[] triggers, string actionName, object[] actionParams, object actionResult)
    {
        try
        {
            if (_accessRecord == null) return actionResult;

            _accessRecord.ExecResult = actionResult;

            using (var repository = _diScope.Resolve<IAccessRecordRepository>())
                await repository.CreateAsync(_accessRecord);
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
        }

        return actionResult;
    }
}