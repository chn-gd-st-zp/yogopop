namespace YogoPop.Component.MultiLang;

public static class ServiceResultExtension
{
    public static IServiceResult<TResult> Success<TResult, TMultiLanguageObject>(this TResult data) where TMultiLanguageObject : IMultiLanguageObject
    {
        var mlo = InstanceCreator.Create<TMultiLanguageObject>();

        var result = new MultilangServiceResult<TResult>()
        {
            IsSuccess = true,
            Code = IVEnum.Restore<IStateCode>().Success,
            Msg = string.Empty,
            Data = data,
            ExInfo = null,

            GroupKey = mlo != null ? mlo.GroupKey : string.Empty,
            ItemKey = mlo != null ? mlo.ItemKey : string.Empty,
        };

        return result;
    }

    public static IServiceResult<TResult> Success<TResult, TMultiLanguageObject>(this TResult data, string addlMsg = default, params object[] fmtArgs) where TMultiLanguageObject : IMultiLanguageObject, IMultiLanguageFmtObject
    {
        var mlo = InstanceCreator.Create<TMultiLanguageObject>();

        var result = new MultilangServiceResult<TResult>()
        {
            IsSuccess = true,
            Code = IVEnum.Restore<IStateCode>().Success,
            Msg = string.Empty,
            Data = data,
            ExInfo = null,

            GroupKey = mlo != null ? mlo.GroupKey : string.Empty,
            ItemKey = mlo != null ? mlo.ItemKey : string.Empty,
            AddlMsg = addlMsg,
            FmtArgs = fmtArgs,
        };

        return result;
    }

    public static IServiceResult<TResult> Fail<TResult, TMultiLanguageObject>(this TResult data, string msg = default) where TMultiLanguageObject : IMultiLanguageObject
    {
        var mlo = InstanceCreator.Create<TMultiLanguageObject>();

        var result = new MultilangServiceResult<TResult>()
        {
            IsSuccess = false,
            Code = IVEnum.Restore<IStateCode>().Fail,
            Msg = msg,
            Data = data,
            ExInfo = null,

            GroupKey = mlo != null ? mlo.GroupKey : string.Empty,
            ItemKey = mlo != null ? mlo.ItemKey : string.Empty,
        };

        return result;
    }

    public static IServiceResult<TResult> Fail<TResult, TMultiLanguageObject>(this TResult data, string addlMsg = default, params object[] fmtArgs) where TMultiLanguageObject : IMultiLanguageObject, IMultiLanguageFmtObject
    {
        var mlo = InstanceCreator.Create<TMultiLanguageObject>();

        var result = new MultilangServiceResult<TResult>()
        {
            IsSuccess = false,
            Code = IVEnum.Restore<IStateCode>().Fail,
            Msg = addlMsg,
            Data = data,
            ExInfo = null,

            GroupKey = mlo != null ? mlo.GroupKey : string.Empty,
            ItemKey = mlo != null ? mlo.ItemKey : string.Empty,
            AddlMsg = addlMsg,
            FmtArgs = fmtArgs,
        };

        return result;
    }

    public static IServiceResult<TResult2> MLTransfer<TResult1, TResult2>(this IServiceResult<TResult1> serviceResult, TResult2 newData = default)
    {
        var mlo1 = serviceResult as IMultiLanguageObject;
        var mlo2 = serviceResult as IMultiLanguageFmtObject;

        var result = new MultilangServiceResult<TResult2>
        {
            IsSuccess = serviceResult.IsSuccess,
            Code = serviceResult.Code,
            Msg = serviceResult.Msg,
            Data = newData,
            ExInfo = serviceResult.ExInfo,

            GroupKey = mlo1 != null ? mlo1.GroupKey : string.Empty,
            ItemKey = mlo1 != null ? mlo1.ItemKey : string.Empty,
            AddlMsg = mlo2 != null ? mlo2.AddlMsg : string.Empty,
            FmtArgs = mlo2 != null ? mlo2.FmtArgs : default,
        };

        return result;
    }

    public static IApiResult<TResult> ToMLApiResult<TResult>(this IServiceResult<TResult> serviceResult)
    {
        var mlo1 = serviceResult as IMultiLanguageObject;
        var mlo2 = serviceResult as IMultiLanguageFmtObject;

        var result = new MultilangApiResult<TResult>
        {
            Code = serviceResult.Code.Value,
            Msg = serviceResult.Msg,
            Data = serviceResult.Data,

            GroupKey = mlo1 != null ? mlo1.GroupKey : string.Empty,
            ItemKey = mlo1 != null ? mlo1.ItemKey : string.Empty,
            AddlMsg = mlo2 != null ? mlo2.AddlMsg : string.Empty,
            FmtArgs = mlo2 != null ? mlo2.FmtArgs : default,
        };

        return result;
    }
}