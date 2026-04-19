namespace DForge.Core.Define;

public static class GlobalSettings
{
    public const int Unlimited = -1;

    public static string SystemName = "domainforge";

    public const string SysStatusCacheKey = "SysStatus";
}

public static class GlobalSupport
{
    public static string CurMerchantFromUrl
    {
        get
        {
            try
            {
                var httpContextAccessor = InjectionContext.Resolve<IHttpContextAccessor>();
                if (httpContextAccessor == null || httpContextAccessor.HttpContext == null || httpContextAccessor.HttpContext.Request == null) return "";

                var url = httpContextAccessor.HttpContext.Request.GetUrl();
                url = url.ToLower();
                url = url.Replace("http://", "");
                url = url.Replace("https://", "");
                var merchantCode = url.SplitRemoveEmptyEntries(".")[0];
                return merchantCode;
            }
            catch
            {
                return "";
            }
        }
    }

    public static LanguageEnum CurLanguage
    {
        get
        {
            var defaultLanguage = InjectionContext.Resolve<MultilangDefaultSettings>().Language.ToEnum<LanguageEnum>();

            try
            {

                var httpContextAccessor = InjectionContext.Resolve<IHttpContextAccessor>();
                if (httpContextAccessor == null || httpContextAccessor.HttpContext == null || httpContextAccessor.HttpContext.Request == null) return defaultLanguage;

                var requestHeaders = httpContextAccessor.HttpContext.Request.Headers;
                if (!requestHeaders.ContainsKey(AppInitHelper.LanguageKeyInHeader, true)) return defaultLanguage;

                return requestHeaders.GetValue(AppInitHelper.LanguageKeyInHeader, true).ToString().ToEnum<LanguageEnum>();
            }
            catch
            {
                return defaultLanguage;
            }
        }
    }

    public static string[] FormatIPs(params string[] datas)
    {
        var result = new List<string>();

        if (datas.IsEmpty()) return result.ToArray();

        foreach (var data in datas)
        {
            var data_new = data;
            data_new = data_new.Replace("\r\n", ",");
            data_new = data_new.Replace("\n", ",");
            data_new = data_new.Replace(" ", ",");
            data_new = data_new.Replace("，", ",");

            var ips = data_new.SplitRemoveEmptyEntries(",");
            if (ips.IsNotEmpty()) result.AddRange(ips);
        }

        return result.ToArray();
    }
}