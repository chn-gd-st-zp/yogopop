namespace DForge.Infrastructure.DTO;

/// <summary>
/// 系统日志
/// </summary>
public class DTOSysAccessRecordResult : DTOOutput, IDTOPrimaryKey<string>
{
    /// <summary>
    /// ID
    /// </summary>
    [Description("ID")]
    [JsonProperty("ID"), PropertyRename("ID")]
    public string PrimaryKey { get; set; }

    /// <summary>
    /// 角色类型
    /// </summary>
    [Description("角色类型")]
    public RoleEnum RoleType { get; set; }

    /// <summary>
    /// 账号ID
    /// </summary>
    [Description("账号ID")]
    public string AccountID { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [Description("角色类型")]
    public string UserName { get; set; }

    /// <summary>
    /// 操作类型
    /// </summary>
    [Description("操作类型")]
    public OperationTypeEnum OperationType { get; set; }

    /// <summary>
    /// 事件
    /// </summary>
    [Description("事件")]
    public string Action { get; set; }

    /// <summary>
    /// 数据表实际名
    /// </summary>
    [Description("数据表实际名")]
    public string TBName { get; set; }

    /// <summary>
    /// 数据表注释名
    /// </summary>
    [Description("数据表注释名")]
    public string TBValue { get; set; }

    /// <summary>
    /// 主键字段
    /// </summary>
    [Description("主键字段")]
    public string PKName { get; set; }

    /// <summary>
    /// 主键值
    /// </summary>
    [Description("主键值")]
    public string PKValue { get; set; }

    /// <summary>
    /// 触发对象名
    /// </summary>
    [Description("触发对象名")]
    public string TriggerName { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [Description("创建时间")]
    public DateTime CreateTime { get; set; }
}