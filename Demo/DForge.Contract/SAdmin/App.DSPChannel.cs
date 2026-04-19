namespace DForge.Contract.SAdmin;

public interface IAppDSPChannelService<TTokenProvider> : ITransient where TTokenProvider : ITokenProvider
{
    public Task<IServiceResult<bool>> Create(DTOAppDSPChannelCreate input);

    public Task<IServiceResult<bool>> Delete(DTOPrimaryKeyRequired<string> input);

    public Task<IServiceResult<bool>> Update(DTOAppDSPChannelUpdate input);

    public Task<IServiceResult<bool>> Status(DTOAppDSPChannelStatus input);

    public Task<IServiceResult<DTOAppDSPChannelSingleResult>> Single(DTOPrimaryKeyRequired<string> input);

    public Task<IServiceResult<DTOPageObj<DTOAppDSPChannelPageResult>>> Page(DTOAppDSPChannelPage input);

    public Task<IServiceResult<bool>> Create(DTOPrimaryKeyRequired<string> input);

    public Task<IServiceResult<DTOPageObj<DTOAppDSPChannelSyncPageResult>>> Page(DTOAppDSPChannelSyncPage input);
}