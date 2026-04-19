namespace YogoPop.DB.Define;

public interface IDBFStatus<T> : IDBField
    where T : Enum
{
    public T Status { get; set; }
}