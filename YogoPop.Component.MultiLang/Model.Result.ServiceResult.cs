namespace YogoPop.Component.MultiLang;

public class MultilangServiceResult<T> : IMultiLanguageObject, IMultiLanguageFmtObject, IServiceResult<T>, ITransient
{
    public bool IsSuccess { get; set; }

    public Exception ExInfo { get; set; }

    public IVEnumItem Code { get; set; }

    public string Msg { get; set; }

    public T Data { get; set; }

    public string GroupKey { get; set; }

    public string ItemKey { get; set; }

    public string AddlMsg { get; set; }

    public object[] FmtArgs { get; set; }
}