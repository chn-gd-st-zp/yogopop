namespace DForge.Infrastructure.DTO;

public abstract class DTOSysRoleResult : DTOPrimaryKey<string>
{
    /// <summary>
    /// 类型
    /// </summary>
    [Description("类型")]
    public RoleEnum Type { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Description("名称")]
    public string Name { get; set; }

    /// <summary>
    /// 级别
    /// </summary>
    [Description("级别")]
    public int Level { get; set; } = 0;

    /// <summary>
    /// 编码
    /// </summary>
    [Description("编码")]
    [JsonProperty("Code"), PropertyRename("Code")]
    public string CurNode { get; set; }

    /// <summary>
    /// 父节点编码
    /// </summary>
    [Description("父节点编码")]
    [JsonProperty("ParentCode"), PropertyRename("ParentCode")]
    public virtual string ParentNode { get; set; }

    /// <summary>
    /// 完整编码
    /// </summary>
    [Description("完整编码")]
    [JsonProperty("FullCode"), PropertyRename("FullCode")]
    public virtual string FullNode { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [Description("创建时间")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [Description("状态")]
    public string Status { get; set; }

    /// <summary>
    /// 权限集合
    /// </summary>
    [Description("权限集合")]
    public virtual string[] PermissionCodes { get; set; }
}