namespace YogoPop.Core.DTO;

public class DTORange<T> : IDTO
{
    public T Begin { get; set; }

    public T End { get; set; }
}