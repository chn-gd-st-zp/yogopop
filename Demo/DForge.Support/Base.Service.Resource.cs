namespace DForge.Support;

public class ApiResourceService<TService, TCache, TTokenProvider> : DomainForgeService<TService, TCache, TTokenProvider>
    where TCache : ICache
    where TService : class, IYogoService
    where TTokenProvider : ITokenProvider
{
    protected async Task<IServiceResult<List<string>>> Upload(AttachmentEnum key, string[] base64DataArray)
    {
        var result = new List<string>();

        var attachmentResult = AttachmentHandlerHelper.Operation(key.ToString(), base64DataArray);
        if (attachmentResult.State != AttachmentResultEnum.Success)
            return result.Fail<List<string>, LogicFailed>();

        attachmentResult.Items.ForEach(o => result.Add(SystemSettings.AttachmentDirectory + o.FilePath));

        return result.Success<List<string>, LogicSucceed>();
    }
}