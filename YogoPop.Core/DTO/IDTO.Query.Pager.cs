namespace YogoPop.Core.DTO;

public interface IDTOPager<TDTOSort> : IDTOSearch<TDTOSort> where TDTOSort : IDTOSort
{
    public int PageSize { get; set; }

    public int PageIndex { get; set; }
}