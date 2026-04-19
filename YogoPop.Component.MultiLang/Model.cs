namespace YogoPop.Component.MultiLang;

public interface IMultiLanguageObject
{
    public string GroupKey { get; }

    public string ItemKey { get; }
}

public interface IMultiLanguageFmtObject
{
    public string AddlMsg { get; }

    public object[] FmtArgs { get; }
}

public abstract class MultiLanguageObject : IMultiLanguageObject, IMultiLanguageFmtObject
{
    public string GroupKey { get; set; }

    public string ItemKey { get; set; }

    public string AddlMsg { get; set; }

    public object[] FmtArgs { get; set; }
}