namespace DForge.Contract.Common;

public interface ISessionService<TTokenProvider, TSignIn, TSignInResult> : ITransient
    where TTokenProvider : ITokenProvider
    where TSignIn : DTOSessionSignIn
    where TSignInResult : DTOSessionResult
{
    public Task<IServiceResult<TSignInResult>> SignIn(TSignIn input);

    public Task<IServiceResult<bool>> SignOut();

    public Task<IServiceResult<TSignInResult>> Get();

    public Task<IServiceResult<bool>> UpdatePassword(DTOSessionUpdatePassword input);
}