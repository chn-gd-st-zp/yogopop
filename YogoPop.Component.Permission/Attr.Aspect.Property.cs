namespace YogoPop.Component.Permission;

[Aspect(Scope.PerInstance)]
public class PermissionAspectForProperty : AOPAspectAsyncBase
{
    private IServiceScope _diScope;
    private IYogoLogger<PermissionAspectForProperty> _logger;
    private IPermissionEnum _permissionEnum;
    private PropertyPermissionBaseAttribute _attr;
    private IPermission _permission;
    private Exception _ex;

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
        _logger = _diScope.Resolve<IYogoLogger<PermissionAspectForProperty>>();
        _permissionEnum = _diScope.Resolve<IPermissionEnum>();

        return base.HandleMethod(source, method, triggers, actionName, actionParams);
    }

    protected override async Task Before(object source, MethodInfo methodInfo, Attribute[] triggers, string actionName, object[] actionParams)
    {
        if (AppInitHelper.IsTestMode)
            return;

        try
        {
            var sessionContext = await YogoSessionContextFactory.RestoreSessionContext();
            if (sessionContext == null) return;

            var session = await sessionContext.RestoreSession();
            if (session == null) return;

            _attr = triggers.Where(o => o.GetType().IsExtendOf<PropertyPermissionBaseAttribute>())
                .Select(o => o as PropertyPermissionBaseAttribute)
                .Where(o => o.Type == PermissionTypeEnum.Property)
                .Select(o => new
                {
                    CurrentAttr = o,
                    OperationAttr = _permissionEnum.EnumType.GetAttr<OperationAttribute>(o.Code)
                })
                .Select(o => new
                {
                    CurrentAttr = o.CurrentAttr,
                    EOperationType = o.OperationAttr == null ? OperationTypeEnum.None : o.OperationAttr.OperationType,
                })
                .Where(o => o.EOperationType == sessionContext.OperationType)
                .Select(o => o.CurrentAttr)
                .FirstOrDefault();
            if (_attr == null)
                return;

            using (var repository = _diScope.Resolve<IPermissionRepository>())
            {
                _permission = repository.Permission(_attr.Code);
                if (_permission == null) return;
            }

            session.VerifyPermission(_attr.Code);
        }
        catch (VEEmptyToken ex)
        {
            _ex = ex;
        }
        catch (VENoLogin ex)
        {
            _ex = ex;
        }
        catch (VENoPermission ex)
        {
            _ex = ex;
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
            throw ex;
        }
    }

    protected override async Task<bool> Error(object source, MethodInfo methodInfo, Attribute[] triggers, string actionName, object[] actionParams, Exception error)
    {
        var throwException = true;

        // 不是特殊方法（get|set）
        // 没有打标签
        // 没有发生异常（表示权限验证通过）
        // 直接返回结果
        if (!methodInfo.IsSpecialName || _attr == null || _ex == null) return throwException;

        if (methodInfo.Name.Contains("_set_", StringComparison.OrdinalIgnoreCase))
        {
            switch (_attr.FailHandle)
            {
                //新增的时候没权限
                case PermissionPropertyFailHandleEnum.Throw:
                    return throwException;
            }
        }

        throwException = false;
        return throwException;
    }

    protected override async Task<object> After(object source, MethodInfo methodInfo, Attribute[] triggers, string actionName, object[] actionParams, object actionResult)
    {
        // 不是特殊方法（get|set）
        // 没有打标签
        // 没有发生异常（表示权限验证通过）
        // 直接返回结果
        if (!methodInfo.IsSpecialName || _attr == null || _ex == null)
            return actionResult;

        if (methodInfo.Name.Contains("_get_", StringComparison.OrdinalIgnoreCase))
        {
            switch (_attr.FailHandle)
            {
                //查询的时候没权限
                case PermissionPropertyFailHandleEnum.Mosaic:
                    if (actionResult != null)
                    {
                        using (var diScope = InjectionContext.Root.CreateScope())
                        {
                            var service = diScope.ResolveByKeyed<IPermissionPropertyFailHandler>(_attr.FailHandle);
                            if (service == null)
                                actionResult = "******";
                            else
                                actionResult = service.Progress(_permission, _attr, actionResult);
                        }
                    }
                    break;
                default:
                    return actionResult;
            }
        }

        if (methodInfo.Name.Contains("_set_", StringComparison.OrdinalIgnoreCase))
        {
            //如果设置的值【为空】，直接忽略
            if (actionParams[0] is null)
                return actionResult;

            //如果设置的值【匹配到默认值】，直接忽略
            //if (actionParams[0].In(_attr.DefaultValues))
            //    return actionResult;

            switch (_attr.FailHandle)
            {
                //修改的时候没权限
                case PermissionPropertyFailHandleEnum.Ignore:
                    return _attr.DefaultValues[0];
                default:
                    return actionResult;
            }
        }

        return actionResult;
    }
}