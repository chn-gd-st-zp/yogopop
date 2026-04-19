namespace YogoPop.Core.DTO;

public interface IDTOPageObj<T> : IDTOOutput
{
    public int PageSize { get; set; }

    public int PageIndex { get; set; }

    public int TotalPageQty { get; set; }

    public int TotalRowQty { get; set; }

    public List<T> Data { get; set; }
}