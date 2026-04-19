namespace YogoPop.Core.DTO;

public interface IDTOInput : IDTO
{
    public long? Timestamp { get; set; }

    public bool Validation(out string errorMsg);
}