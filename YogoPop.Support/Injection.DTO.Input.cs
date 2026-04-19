namespace YogoPop.Support;

[DIModeForService(DIModeEnum.Exclusive, typeof(IDTOInput))]
public class DTOInput : IDTOInput, ITransient
{
    [JsonIgnore, PropertyHidden]
    public virtual long? Timestamp { get; set; }

    /// <summary>
    /// 验证方法
    /// </summary>
    /// <param name="errorMsg"></param>
    /// <returns></returns>
    public virtual bool Validation(out string errorMsg) { errorMsg = string.Empty; return true; }
}