namespace YogoPop.Core.DTO;

public interface IDTOSearch<TDTOSort> : IDTOInput where TDTOSort : IDTOSort
{
    public List<TDTOSort>? Sorts { get; set; }
}