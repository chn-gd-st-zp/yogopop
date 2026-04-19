namespace YogoPop.DB.Define;

public interface IDBFJson : IDBField
{
    public string JsonData { get; set; }
}

public interface IDBFJsonContent
{
    //
}

public static class DBFJsonExtension
{
    public static TOutput Restore<TEntity, TOutput>(this TEntity obj)
        where TEntity : IDBEntity, IDBFJson
        where TOutput : IDBFJsonContent
    {
        return obj.JsonData.ToObject<TOutput>();
    }

    public static TOutput Restore<TEntity, TDBField, TOutput>(this TEntity obj)
        where TEntity : IDBEntity, IDBFJson
        where TDBField : IDBFJsonContent
        where TOutput : class
    {
        var jsonObj = obj.JsonData.ToObject<TDBField>();
        var result = obj.MapTo<TOutput>();
        result = jsonObj.AdaptTo(result);
        return result;
    }
}