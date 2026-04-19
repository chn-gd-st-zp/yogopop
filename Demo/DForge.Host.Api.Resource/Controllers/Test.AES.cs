namespace DForge.Host.Api.Resource.Controllers;

#if DEBUG

/// <summary>
/// 加解密
/// </summary>
[LogIgnore, AllowAnonymous]
public class AESTestController : DomainForgeController
{
    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Encrypt")]
    public async Task<IApiResult<string>> Encrypt(DTOAESEncrypt input) => AES.Encrypt($"{input.Env}_{GlobalSettings.SystemName}_{input.Secret}", input.IV, input.CipherMode, input.PaddingMode, input.Content).Success<string, LogicSucceed>().ToApiResult();

    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Decrypt")]
    public async Task<IApiResult<string>> Decrypt(DTOAESDecrypt input) => AES.Decrypt($"{input.Env}_{GlobalSettings.SystemName}_{input.Secret}", input.IV, input.CipherMode, input.PaddingMode, input.Content).Success<string, LogicSucceed>().ToApiResult();
}

#endif