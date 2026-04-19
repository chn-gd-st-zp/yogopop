namespace DForge.Contract.SAdmin;

public interface IAppDomainService<TTokenProvider> : ITransient where TTokenProvider : ITokenProvider
{
    public Task<IServiceResult<DTOPageObj<DTOAppDomainPageResult>>> Page(DTOAppDomainPage input);

    public Task<IServiceResult<bool>> Set(DTOAppDomainNSSet input);

    public Task<IServiceResult<bool>> Create(DTOAppDomainSyncCreate input);

    public Task<IServiceResult<DTOPageObj<DTOAppDomainSyncPageResult>>> Page(DTOAppDomainSyncPage input);
}