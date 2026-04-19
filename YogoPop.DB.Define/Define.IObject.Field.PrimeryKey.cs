namespace YogoPop.DB.Define;

public interface IDBFPrimaryKey : IPrimaryKey, IDBField
{
    //
}

public interface IDBFPrimaryKey<T> : IPrimaryKey<T>, IDBFPrimaryKey
{
    //
}