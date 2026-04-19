namespace DForge.Host.Api.Resource.Controllers;

/// <summary>
/// 附件
/// </summary>
[LogIgnore]
public class AttachmentController : DomainForgeController
{
    private IAttachmentService<HTTPTokenProvider> _service = InjectionContext.Resolve<IAttachmentService<HTTPTokenProvider>>();

    /// <summary>
    /// 上传多个
    /// </summary>
    /// <param name="key"></param>
    /// <param name="files"></param>
    /// <returns></returns>
    [HttpPost, Route("Uploads")]
    public async Task<IApiResult<List<string>>> Uploads([FromForm] AttachmentEnum key, [FromForm] IFormFile[] files) => (await _service.Uploads(key, files)).ToMLApiResult();

    /// <summary>
    /// 上传多个
    /// </summary>
    /// <param name="key"></param>
    /// <param name="files"></param>
    /// <returns></returns>
    [HttpPost, Route("AnonymousUploads"), ApiHidden]
    public async Task<IApiResult<List<string>>> AnonymousUploads([FromForm] AttachmentEnum key, [FromForm] IFormFile[] files) => (await _service.AnonymousUploads(key, files)).ToMLApiResult();
}