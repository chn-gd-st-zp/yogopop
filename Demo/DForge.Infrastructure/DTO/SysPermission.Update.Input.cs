namespace DForge.Infrastructure.DTO;

public class DTOSysPermissionUpdate : DTOPrimaryKeyRequired<string>
{
    /// <summary>
    /// 是否启用日志记录
    /// </summary>
    [Description("是否启用日志记录")]
    [Required]
    public bool AccessLogger { get; set; }
}