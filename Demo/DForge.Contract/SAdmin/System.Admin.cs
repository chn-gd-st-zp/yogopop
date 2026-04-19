namespace DForge.Contract.SAdmin;

public interface IAdminService<TTokenProvider> : ITransient where TTokenProvider : ITokenProvider
{
    public Task<IServiceResult<bool>> Create(DTOSysAdminCreate input);

    public Task<IServiceResult<bool>> Delete(DTOPrimaryKeyRequired<string> input);

    public Task<IServiceResult<bool>> Update(DTOSysAdminUpdate input);

    public Task<IServiceResult<bool>> Status(DTOSysAdminStatus input);

    public Task<IServiceResult<bool>> ResetPassword(DTOPrimaryKeyRequired<string> input);

    public Task<IServiceResult<string>> ResetMFA(DTOPrimaryKeyRequired<string> input);

    public Task<IServiceResult<DTOSysAdminSingleResult>> Single(DTOPrimaryKeyRequired<string> input);

    public Task<IServiceResult<DTOPageObj<DTOSysAdminPageResult>>> Page(DTOSysAdminPage input);
}