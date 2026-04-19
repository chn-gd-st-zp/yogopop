namespace YogoPop.DB.Define;

public interface IDBParameter
{
    public string Name { get; set; }

    public object Value { get; set; }

    public bool IsNullable { get; set; }

    public ParameterDirection Direction { get; set; }

    public DbType DBType { get; set; }

    public int Size { get; set; }
}