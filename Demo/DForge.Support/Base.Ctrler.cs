namespace DForge.Support;

[ApiController, Route("api/[controller]")]
[CustomizeHeader(AppInitHelper.LanguageKeyInHeader)]
public abstract class DomainForgeController : CtrlerBase
{
    protected IHttpContextAccessor HttpContextAccessor = InjectionContext.Resolve<IHttpContextAccessor>();
}

[MaintainCtrl]
public abstract class DomainForgeFrontendController : DomainForgeController
{
    //
}

[ApiController, LogIgnore, AllowAnonymous]
public abstract class DomainForgeToolController : CtrlerBase
{
    /// <summary>
    /// 健康检查
    /// </summary>
    /// <returns></returns>
    [HttpGet, Route("HealthCheck")]
    public async Task<IApiResult<bool>> Go() => true.Success<bool, LogicSucceed>().ToMLApiResult();
}