namespace YogoPop.Component.MultiLang;

public class MultilangApiResult<T> : IMultiLanguageObject, IMultiLanguageFmtObject, IApiResult<T>, ITransient
{
    public int Code { get; set; }

    public string Msg { get; set; }

    public T Data { get; set; }

    [JsonIgnore]
    public string GroupKey { get; set; }

    [JsonIgnore]
    public string ItemKey { get; set; }

    [JsonIgnore]
    public string AddlMsg { get; set; }

    [JsonIgnore]
    public object[] FmtArgs { get; set; }
}