namespace YogoPop.Support;

[DIModeForService(DIModeEnum.Exclusive, typeof(IDTOPrimaryKey<>))]
public class DTOPrimaryKey<T> : IDTOPrimaryKey<T>, IDTOInput, IDTOOutput
{
    /// <summary>
    /// ID
    /// </summary>
    [Description("ID")]
    [JsonProperty("ID"), PropertyRename("ID")]
    public virtual T? PrimaryKey { get; set; }

    [JsonIgnore, PropertyHidden]
    public virtual long? Timestamp { get; set; }

    /// <summary>
    /// 验证方法
    /// </summary>
    /// <param name="errorMsg"></param>
    /// <returns></returns>
    public virtual bool Validation(out string errorMsg) { errorMsg = string.Empty; return true; }
}

public class DTOPrimaryKeyRequired<T> : DTOPrimaryKey<T>
{
    /// <summary>
    /// ID
    /// </summary>
    [Description("ID")]
    [JsonProperty("ID"), PropertyRename("ID")]
    [Required]
    public override T PrimaryKey { get; set; }
}