namespace YogoPop.Component.MultiLang;

public class MLActionResultExecutor : IActionResultExecutor
{
    public object Execute(object data)
    {
        if (data == null) return data;

        var type = data.GetType();
        if (!type.IsImplementedOf<IApiResult>()) return data;
        if (!type.IsImplementedOf<IMultiLanguageObject>()) return data;

        var data_api = data as IApiResult;
        if (data_api == null) return data;

        var data_mlo1 = data as IMultiLanguageObject;
        if (data_mlo1 == null) return data;

        var data_mlo2 = data as IMultiLanguageFmtObject;

        var groupKey = data_mlo1.GroupKey.IsNotEmptyString() ? data_mlo1.GroupKey : string.Empty;
        var itemKey = data_mlo1.ItemKey.IsNotEmptyString() ? data_mlo1.ItemKey : string.Empty;

        using (var diScope = InjectionContext.Root.CreateScope())
        {
            var defaultSettings = diScope.Resolve<MultilangDefaultSettings>();
            if (defaultSettings == null) return data;

            var language = defaultSettings.Language;

            var hca = diScope.Resolve<IHttpContextAccessor>();
            if (hca != null && hca.HttpContext != null)
                language = hca.HttpContext.Request.Headers.GetValue(AppInitHelper.LanguageKeyInHeader, defaultSettings.Language, true);

            var mlMapping = default(MultilangMapping);
            try
            {
                mlMapping = diScope.Resolve<IMultilangExchanger>().GetAsync(language, groupKey, itemKey).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                mlMapping = null;
            }

            var msg = data_api.Msg;
            msg = mlMapping != null ? mlMapping.DestContent : msg;
            msg = msg.IsNotEmptyString() ? msg : $"{groupKey}-{itemKey}-MLNoMapping";

            if (data_mlo2 != null)
            {
                try
                {
                    msg += data_mlo2.AddlMsg.IsNotEmptyString() ? $" -> {data_mlo2.AddlMsg}" : string.Empty;
                    msg = data_mlo2.FmtArgs.IsNotEmpty() ? msg.Format(data_mlo2.FmtArgs) : msg;
                }
                catch { }
            }

            data_api.Msg = msg;
        }

        return data_api;
    }
}