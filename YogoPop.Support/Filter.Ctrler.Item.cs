namespace YogoPop.Support;

public class CtrlerFilterItem : IRequestFilterItem
{
    protected readonly IYogoLogger Logger;

    public CtrlerFilterItem() { Logger = InjectionContext.Resolve<IYogoLogger>(); }

    public string Entrance { get; set; }
    public string Action { get; set; }
    public object Header { get; set; }
    public object ReqParams { get; set; }
    public object FuncParams { get; set; }
    public object Result4Log { get; set; }
    public object Result4Return { get; set; }
    public Exception Exception { get; set; }
    public DateTime? RequestTime { get; set; }
    public DateTime? ResponseTime { get; set; }

    public virtual void OnExecuting(object context)
    {
        var realContext = context as ActionExecutingContext;

        RequestTime = DateTimeExtension.Now;

        var paramDic = realContext.ActionArguments.ToDictionary(k => k.Key, v => v.Value is string && v.Value.ToString().Length > 2000 ? v.Value.ToString().Substring(0, 100) + "..." : v.Value);

        Entrance = realContext.RouteData.Values["controller"].ToString();
        Action = realContext.RouteData.Values["action"].ToString();
        Header = realContext.HttpContext.Request.Headers.ToDictionary(k => k.Key, v => v.Value as object);
        ReqParams = realContext.HttpContext.Request.GetRequestValue().GetAwaiter().GetResult();
        FuncParams = paramDic;

        //自定义的参数验证
        paramDic.Select(o => o.Value).ToArray().Verify();
    }

    public virtual void OnExecuted(object context)
    {
        var realContext = context as ActionExecutedContext;

        ResponseTime = DateTimeExtension.Now;

        //出现异常直接去【OnException】里面处理
        if (realContext.Exception != null) return;

        if (realContext.Result is ObjectResult)
        {
            var contextResult = ((ObjectResult)realContext.Result).Value;
            var actionResultExecutor = InjectionContext.Resolve<IActionResultExecutor>();
            if (actionResultExecutor != null) contextResult = actionResultExecutor.Execute(contextResult);
            Result4Log = contextResult;
            Result4Return = contextResult.ToJsonResult();
        }
        else if (realContext.Result is FileResult)
        {
            var contextResult = realContext.Result as FileResult;
            Result4Log = new { Content = contextResult.GetType().FullName, ContentType = contextResult.ContentType }; ;
            Result4Return = contextResult;
        }
        else
        {
            Result4Return = realContext.Result;
        }

        //如果没标记【LogIgnore】，才记录日志
        if (
            1 == 1
            && realContext.Controller.GetType().GetCustomAttribute<LogIgnoreAttribute>() == null
            && ((ControllerActionDescriptor)realContext.ActionDescriptor).MethodInfo.GetCustomAttribute<LogIgnoreAttribute>() == null
        )
        {
            //记录每次请求的往返内容
            Logger.Info(new
            {
                Entrance,
                Action,
                Header,
                ReqParams,
                FuncParams,
                Result4Log,
                RequestTime,
                ResponseTime,
            });
        }
    }

    public virtual void OnException(object context, Exception exception)
    {
        var realContext = context as ExceptionContext;

        realContext.ExceptionHandled = true;

        Exception = exception;

        //获取最底层的错误
        while (Exception != null && Exception.InnerException != null)
            Exception = Exception.InnerException;

        var eventCode = Unique.GetRandomCode5(6);
        var exceptionType = Exception.GetType();

        var contextResult = (exceptionType.IsImplementedOf<IVException>() ? string.Empty.VException(Exception as IVException).ToApiResult() : string.Empty.Exception(Exception).ToApiResult()) as IApiResult;
        var actionResultExecutor = InjectionContext.Resolve<IActionResultExecutor>();
        if (actionResultExecutor != null) contextResult = actionResultExecutor.Execute(contextResult) as IApiResult;

        //如果是系统错误，就需要对错误消息做处理
        if (!exceptionType.IsImplementedOf<IVException>())
            contextResult.Msg = AppInitHelper.IsTestMode ? contextResult.Msg : $"system error [{eventCode}]";

        Result4Log = contextResult;
        Result4Return = contextResult.ToJsonResult();

        //如果没标记【LogIgnore】，才记录日志
        if (Exception.GetType().GetCustomAttribute<LogIgnoreAttribute>() == null)
        {
            //记录错误日志
            Logger.Error(new
            {
                EventCode = eventCode,
                Entrance,
                Action,
                Header,
                ReqParams,
                FuncParams,
                Result4Log,
                RequestTime,
                ResponseTime,
            }, Exception);
        }
    }

    public virtual void OnExit(object context)
    {
        #region 当 【SuppressModelStateInvalidFilter = false】 时才会进入

        if (context is ResultExecutingContext)
        {
            var realContext = context as ResultExecutingContext;

            if (realContext.Result is BadRequestObjectResult)
            {

                var error = (realContext.Result as BadRequestObjectResult).Value as ValidationProblemDetails;
                if (error == null) return;

                var msg = string.Empty;
                foreach (var itemItem in error.Errors)
                {
                    msg += msg.IsEmptyString() ? string.Empty : "|";
                    msg += itemItem.Value.ToString(';');
                }

                realContext.Result = error.Errors.VException(new VEParamsValidation(msg)).ToApiResult().ToJsonResult();
                return;
            }
        }

        #endregion

        if (context is ResultExecutingContext)
        {
            var realContext = context as ResultExecutingContext;

            if (realContext.Result is FileResult == false)
            {
                realContext.Result = Result4Return as IActionResult;
            }
        }
        else if (context is ExceptionContext)
        {
            var realContext = context as ExceptionContext;

            realContext.Result = Result4Return as IActionResult;
        }
    }
}