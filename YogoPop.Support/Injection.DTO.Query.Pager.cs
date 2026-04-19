namespace YogoPop.Support;

[DIModeForService(DIModeEnum.Exclusive, typeof(IDTOPager<>))]
public class DTOPager<TDTOSort> : DTOSearch<TDTOSort>, IDTOPager<TDTOSort>
    where TDTOSort : IDTOSort
{
    /// <summary>
    /// 每页大小
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// 当前页
    /// </summary>
    public int PageIndex { get; set; }
}