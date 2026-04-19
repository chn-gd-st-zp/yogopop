namespace YogoPop.Core.DTO;

public interface IDTOPrimaryKey : IPrimaryKey, IDTO
{
    //
}

public interface IDTOPrimaryKey<T> : IPrimaryKey<T>, IDTOPrimaryKey
{
    //
}