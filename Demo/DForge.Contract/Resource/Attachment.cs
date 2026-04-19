namespace DForge.Contract.Resource;

public interface IAttachmentService<TTokenProvider> : ITransient where TTokenProvider : ITokenProvider
{
    public Task<IServiceResult<List<string>>> Uploads(AttachmentEnum key, IFormFile[] files);

    public Task<IServiceResult<List<string>>> AnonymousUploads(AttachmentEnum key, IFormFile[] files);
}