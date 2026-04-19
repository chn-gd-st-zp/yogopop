namespace YogoPop.DB.Define;

public interface IDBFActionCtrl : IDBField
{
    [NotMapped]
    public bool IsNewData { get; set; }
}