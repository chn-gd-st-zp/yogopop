namespace DForge.Contract.Resource;

public interface ISysToolService<TTokenProvider> : ITransient where TTokenProvider : ITokenProvider
{
    public Task<IServiceResult<DTOSystemStatusGetResult>> StatusGet();

    public Task<IServiceResult<bool>> StatusSet(DTOSystemStatusSet input);
}