namespace YogoPop.Core.Interface;

public interface IKVResource
{
    //
}

public interface IKVResourceForEnum : IKVResource
{
    //
}

public interface IKVResourceForEntity : IKVResource
{
    public string PropertyName { get; }

    public string Language { get; }

    public string GetKey();
}

public interface IKVResourceRepository : ITransient
{
    public string GetLanguage(params object[] args);
}