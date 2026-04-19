namespace YogoPop.Component.DataVisibility;

public interface IDataVisiAssign
{
    public DataVisionEnum DataVision { get; set; }

    public string IdentityKey { get; set; }

    public string SourceKey { get; set; }

    public string SourceVals { get; }
}

public interface IDataVisiSource<TPrimaryKey> : IDBFPrimaryKey<TPrimaryKey>
{
    //
}

public interface IDataVisiElement
{
    public string EleKey { get; }

    public string EleVal { get; }
}

public interface IDataVisiElementSource
{
    public string ID { get; }

    public string Text { get; }

    public bool Deleted { get; }
}