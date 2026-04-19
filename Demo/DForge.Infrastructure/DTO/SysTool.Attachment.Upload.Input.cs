namespace DForge.Infrastructure.DTO;

/// <summary>
/// 附件上传
/// </summary>
public class DTOAttachmentUpload : DTOInput
{
    /// <summary>
    /// 标识
    /// </summary>
    [Required]
    public AttachmentEnum Key { get; set; }

    /// <summary>
    /// 文件数据（base64格式）
    /// StartWith => "data:ext;base64," or "data:type/ext;base64,"
    /// Sample => "data:png;base64," or "data:image/png;base64,"
    /// </summary>
    [Required]
    public string Base64Data { get; set; }
}