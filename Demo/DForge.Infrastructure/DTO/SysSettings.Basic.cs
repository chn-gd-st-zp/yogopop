namespace DForge.Infrastructure.DTO;

/// <summary>
/// 系统设置
/// </summary>
public abstract class DTOSysSettingsInput : DTOPrimaryKeyRequired<string>
{
    /// <summary>
    /// 分类
    /// </summary>
    [Description("分类")]
    [JsonIgnore, PropertyHidden]
    public abstract SysSettingsEnum Type { get; }

    /// <summary>
    /// 标题
    /// </summary>
    [Description("标题")]
    public virtual string? Title { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [Description("排序号")]
    [JsonProperty("Sequence"), PropertyRename("Sequence")]
    public virtual string? CurSequence { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [Description("状态")]
    public virtual StatusEnum Status { get; set; } = StatusEnum.Normal;

    /// <summary>
    /// 参数校验
    /// </summary>
    /// <param name="errorMsg"></param>
    /// <returns></returns>
    public override bool Validation(out string errorMsg)
    {
        errorMsg = string.Empty;

        if (Type == SysSettingsEnum.None)
        {
            errorMsg = GlobalSupport.CurLanguage.ToString().GetDestContent<ParameterError>();
            return false;
        }

        if (Status == StatusEnum.None)
        {
            errorMsg = GlobalSupport.CurLanguage.ToString().GetDestContent<ParameterError>();
            return false;
        }

        return true;
    }
}

/// <summary>
/// 系统设置
/// </summary>
public abstract class DTOSysSettingsResult : DTOOutput, IDTOPrimaryKey<string>
{
    /// <summary>
    /// ID
    /// </summary>
    [Description("ID")]
    [JsonProperty("ID"), PropertyRename("ID")]
    public virtual string PrimaryKey { get; set; }

    /// <summary>
    /// 分类
    /// </summary>
    [Description("分类")]
    public virtual SysSettingsEnum Type { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    [Description("标题")]
    public virtual string Title { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [Description("排序号")]
    [JsonProperty("Sequence"), PropertyRename("Sequence")]
    public virtual string CurSequence { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [Description("创建时间")]
    public virtual DateTime CreateTime { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [Description("状态")]
    public virtual StatusEnum Status { get; set; }
}