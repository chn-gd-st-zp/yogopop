namespace DForge.Contract.Resource;

public interface IDropListService<TTokenProvider> : ITransient where TTokenProvider : ITokenProvider
{
    public Task<IServiceResult<List<DTODropItem>>> Role();

    public Task<IServiceResult<List<DTODropItem>>> Project();

    public Task<IServiceResult<DTOPageObj<DTOAppDSPChannelDropListResult>>> Channel(DTOAppDSPChannelDropList input);
}