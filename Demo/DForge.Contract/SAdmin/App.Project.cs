namespace DForge.Contract.SAdmin;

public interface IAppProjectService<TTokenProvider> : ITransient where TTokenProvider : ITokenProvider
{
    public Task<IServiceResult<bool>> Create(DTOAppProjectCreate input);

    public Task<IServiceResult<bool>> Delete(DTOPrimaryKeyRequired<string> input);

    public Task<IServiceResult<bool>> Update(DTOAppProjectUpdate input);

    public Task<IServiceResult<bool>> Status(DTOAppProjectStatus input);

    public Task<IServiceResult<DTOAppProjectSingleResult>> Single(DTOPrimaryKeyRequired<string> input);

    public Task<IServiceResult<DTOPageObj<DTOAppProjectPageResult>>> Page(DTOAppProjectPage input);
}