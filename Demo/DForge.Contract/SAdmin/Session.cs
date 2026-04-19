namespace DForge.Contract.SAdmin;

public interface ISessionService<TTokenProvider> : Common.ISessionService<TTokenProvider, DTOSessionSAdminSignIn, DTOSessionSAdminSignInResult> where TTokenProvider : ITokenProvider
{
    public Task<IServiceResult<DTOPageObj<DTOSessionPageResult>>> Page(DTOSessionPage input);

    public Task<IServiceResult<bool>> Kick(DTOSessionKick input);
}