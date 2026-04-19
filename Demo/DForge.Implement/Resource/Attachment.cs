namespace DForge.Implement.Resource;

[DIModeForService(DIModeEnum.Exclusive, typeof(IAttachmentService<>))]
public partial class AttachmentService<TTokenProvider> : ApiResourceService<AttachmentService<TTokenProvider>, ICache, TTokenProvider>, IAttachmentService<TTokenProvider> where TTokenProvider : ITokenProvider
{
    [ActionPermission(GlobalPermissionEnum.Attachment_Upload, GlobalPermissionEnum.Attachment)]
    public async Task<IServiceResult<List<string>>> Uploads(AttachmentEnum key, IFormFile[] files) => await AnonymousUploads(key, files);

    public async Task<IServiceResult<List<string>>> AnonymousUploads(AttachmentEnum key, IFormFile[] files)
    {
        var result = new List<string>();

        foreach (var file in files)
        {
            if (file.Length <= 0)
                continue;

            var processResult = await Upload(key, new string[] { $"data:{file.ContentType};base64," + StreamExtension.ToBase64(file.OpenReadStream()) });
            if (!processResult.IsSuccess)
                return result.Fail<List<string>, LogicFailed>();

            processResult.Data.ForEach(o => result.Add("/resource" + o));
        }

        return result.Success<List<string>, LogicSucceed>();
    }
}