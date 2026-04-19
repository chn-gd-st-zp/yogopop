namespace DForge.Infrastructure.Helper;

public static class SysStatusManager
{
    public static async Task<DTOSystemStatusGetResult> GetAsync()
    {
        var result = default(DTOSystemStatusGetResult);

        try
        {
            using (var cache = InjectionContext.Resolve<ICache4Redis>())
            {
                result = await cache.GetAsync<DTOSystemStatusGetResult>(GlobalSettings.SysStatusCacheKey);
            }
        }
        catch
        {
            result = default;
        }

        result = result != default ? result : new DTOSystemStatusGetResult
        {
            Status = SysStatusEnum.Maintaining
        };

        return result;
    }

    public static async Task<IServiceResult<bool>> SetAsync<TLogger>(IYogoLogger<TLogger> _logger, DTOSystemStatusSet input) where TLogger : class
    {
        var result = false;
        var content = string.Empty;

        try
        {
            //var settings = InjectionContext.Resolve<MaintainSettings>();
            //var respMsg = await HttpHelper.PostAsync($"{settings.Url}/maintence", "application/json", null, new { status = input.Status.ToString().ToLower() }.ToJson());
            //result = respMsg.StatusCode == System.Net.HttpStatusCode.OK;
            //content = await respMsg.Content.ReadAsStringAsync();
            //content = content.Length <= 100 ? content : content.Substring(0, 100);

            result = true;
            if (!result) return result.Fail<bool, LogicFailed>();

            using (var cache = InjectionContext.Resolve<ICache4Redis>())
            {
                result = await cache.SetAsync(GlobalSettings.SysStatusCacheKey, input);
            }

            if (!result) return result.Fail<bool, LogicFailed>();
        }
        catch (Exception ex)
        {
            _logger.Error($"{ex.Message}", ex);
        }

        return result.Success<bool, LogicSucceed>();
    }

}