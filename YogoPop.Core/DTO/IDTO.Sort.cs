namespace YogoPop.Core.DTO;

public interface IDTOSort : IDTO, ITransient
{
    public string FieldName { get; set; }

    public SortDirectionEnum Direction { get; set; }
}