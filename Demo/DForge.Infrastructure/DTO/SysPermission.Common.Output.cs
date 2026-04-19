namespace DForge.Infrastructure.DTO;

public class DTOSysPermissionResult : DTOTreeNSequenceResult, IDTOPrimaryKey<string>
{
    /// <summary>
    /// ID
    /// </summary>
    [Description("ID")]
    [JsonProperty("ID"), PropertyRename("ID")]
    public virtual string PrimaryKey { get; set; }

    /// <summary>
    /// 权限类型
    /// </summary>
    [Description("权限类型")]
    public PermissionTypeEnum Type { get; set; }

    /// <summary>
    /// 是否开启日志记录
    /// </summary>
    [Description("是否开启日志记录")]
    public virtual bool AccessLogger { get; set; }
}