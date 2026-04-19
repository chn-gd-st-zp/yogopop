namespace YogoPop.Core.Result;

public static class ServiceResultExtension
{
    public static IServiceResult<TResult> Success<TResult>(this TResult data, string msg = "操作成功")
    {
        var result = InjectionContext.Resolve<IServiceResult<TResult>>();
        result.IsSuccess = true;
        result.ExInfo = null;
        result.Code = IVEnum.Restore<IStateCode>().Success;
        result.Msg = msg;
        result.Data = data;
        return result;
    }

    public static IServiceResult<TResult> Fail<TResult>(this TResult data, string msg = "操作失败")
    {
        var result = InjectionContext.Resolve<IServiceResult<TResult>>();
        result.IsSuccess = false;
        result.ExInfo = null;
        result.Code = IVEnum.Restore<IStateCode>().Fail;
        result.Msg = msg;
        result.Data = data;
        return result;
    }

    public static IServiceResult<TResult> Exception<TResult>(this TResult data, Exception exInfo, string msg = null)
    {
        var result = InjectionContext.Resolve<IServiceResult<TResult>>();
        result.IsSuccess = false;
        result.ExInfo = exInfo;
        result.Code = IVEnum.Restore<IStateCode>().SysError;
        result.Msg = !msg.IsEmptyString() ? msg : exInfo.Message;
        result.Data = data;
        return result;
    }

    public static IServiceResult<TResult> VException<TResult>(this TResult data, IVException exInfo, string msg = null)
    {
        var result = InjectionContext.Resolve<IServiceResult<TResult>>();
        result.IsSuccess = false;
        result.ExInfo = null;
        result.Code = exInfo.Code;
        result.Msg = !msg.IsEmptyString() ? msg : (exInfo.VMessage.IsNotEmptyString() ? exInfo.VMessage : exInfo.GetMessage());
        result.Data = data;
        return result;
    }

    public static IServiceResult<TResult> VException<TResult, TVException>(this TResult data, string msg = null) where TVException : class, IVException
    {
        var exInfo = InstanceCreator.Create<TVException>();

        var result = InjectionContext.Resolve<IServiceResult<TResult>>();
        result.IsSuccess = false;
        result.ExInfo = null;
        result.Code = exInfo.Code;
        result.Msg = !msg.IsEmptyString() ? msg : (exInfo.VMessage.IsNotEmptyString() ? exInfo.VMessage : exInfo.GetMessage());
        result.Data = data;
        return result;
    }

    public static IServiceResult<TResult2> Transfer<TResult1, TResult2>(this IServiceResult<TResult1> serviceResult, TResult2 newData = default)
    {
        var result = InjectionContext.Resolve<IServiceResult<TResult2>>();
        result.IsSuccess = serviceResult.IsSuccess;
        result.ExInfo = serviceResult.ExInfo;
        result.Code = serviceResult.Code;
        result.Msg = serviceResult.Msg;
        result.Data = newData;
        return result;
    }

    public static IApiResult<TResult> ToApiResult<TResult>(this IServiceResult<TResult> serviceResult)
    {
        var result = InjectionContext.Resolve<IApiResult<TResult>>();
        result.Code = serviceResult.Code.Value;
        result.Msg = serviceResult.Msg;
        result.Data = serviceResult.Data;
        return result;
    }
}