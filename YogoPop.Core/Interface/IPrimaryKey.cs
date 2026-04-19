namespace YogoPop.Core.Interface;

public interface IPrimaryKey
{
    //
}

public interface IPrimaryKey<T> : IPrimaryKey
{
    public T PrimaryKey { get; set; }
}