namespace YogoPop.Support;

[DIModeForService(DIModeEnum.Exclusive, typeof(IDTOSearch<>))]
public class DTOSearch<TDTOSort> : DTOInput, IDTOSearch<TDTOSort>, ITransient
    where TDTOSort : IDTOSort
{
    /// <summary>
    /// 时间戳
    /// </summary>
    [Description("时间戳")]
    public override long? Timestamp { get; set; }

    /// <summary>
    /// 排序设置
    /// </summary>
    public virtual List<TDTOSort>? Sorts { get; set; }
}