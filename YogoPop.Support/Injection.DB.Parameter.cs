namespace YogoPop.Support;

[DIModeForService(DIModeEnum.Exclusive, typeof(IDBParameter))]
public class DBParameter : IDBParameter
{
    public string Name { get; set; }

    public object Value { get; set; }

    public bool IsNullable { get; set; }

    public ParameterDirection Direction { get; set; }

    public DbType DBType { get; set; }

    public int Size { get; set; }
}