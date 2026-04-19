namespace DForge.Infrastructure.DBSupport.Base;

[DefaultSort("ID", SortDirectionEnum.ASC)]
public class DBTree : DBFStatus, IDBFPrimaryKey<string>, IDBTree<string>
{
    [NotMapped]
    public string Text { get { return Name; } }

    [NotMapped]
    public string Value { get { return CurNode; } }

    [Column("ID")]
    [Sort("ID")]
    public string PrimaryKey { get; set; } = Unique.GetID();

    [Column("Name")]
    [Sort("Name")]
    public virtual string Name { get; set; } = string.Empty;

    [Column("Code")]
    [Sort("Code")]
    public virtual string CurNode { get; set; } = string.Empty;

    [Column("ParentCode")]
    [Sort("ParentCode")]
    public virtual string ParentNode { get; set; } = string.Empty;

    [Column("FullCode")]
    [Sort("FullCode")]
    public virtual string FullNode
    {
        get { return _fullNode; }
        set
        {
            string data = value;

            if (!data.StartsWith(","))
                data = "," + data;

            if (!data.EndsWith(","))
                data = data + ",";

            _fullNode = data;
        }
    }

    private string _fullNode;
}