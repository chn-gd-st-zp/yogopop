namespace DForge.Contract.Resource;

public interface IKVResourceService<TTokenProvider> : ITransient where TTokenProvider : ITokenProvider
{
    public Task<IServiceResult<SortedDictionary<string, object>>> ResourceData(LanguageEnum language = LanguageEnum.EN);

    public Task<IServiceResult<bool>> Refresh();
}