namespace YogoPop.Component.MultiLang;

[DIModeForService(DIModeEnum.AsSelf)]
public sealed class MultilangMapping : ITransient
{
    public MultiLangMappingEnum Type { get; set; } = MultiLangMappingEnum.None;

    public string GroupKey { get; set; } = string.Empty;

    public string GroupDescription { get; set; } = string.Empty;

    public string ItemKey { get; set; } = string.Empty;

    public string ItemDescription { get; set; } = string.Empty;

    public string DestLanguage { get; set; } = string.Empty;

    public string DestContent { get; set; } = string.Empty;
}

public interface IMultilangExchanger : ITransient
{
    public Task<List<MultilangMapping>> LoadAsync(string destLanguage);

    public Task<bool> SetAsync(string destLanguage, MultilangMapping data);

    public Task<List<MultilangMapping>> GetAsync(string destLanguage);

    public Task<MultilangMapping> GetAsync(string destLanguage, string groupKey, string itemKey);
}

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class DBFieldMultiLangAttribute : Attribute
{
    public string GroupKey { get; private set; }

    public string GroupDescription { get; private set; }

    public DBFieldMultiLangAttribute(object groupKeyObj, string groupDescription = default)
    {
        GroupKey = groupKeyObj.ToString();
        GroupDescription = groupDescription.IsNotEmptyString() ? groupDescription : string.Empty;
        GroupDescription = GroupDescription.IsNotEmptyString() ? GroupDescription : groupKeyObj.GetType().GetDesc();
    }
}

public static class MultiLanguageObjectExtension
{
    public static string GetDestContent(this string language, string groupKey, string itemKey, string addlMsg = default, params object[] fmtArgs)
    {
        var result = string.Empty;
        try
        {
            using (var diScope = InjectionContext.Root.CreateScope())
            {
                var mlMapping = diScope.Resolve<IMultilangExchanger>().GetAsync(language, groupKey, itemKey).GetAwaiter().GetResult();
                result = mlMapping != null ? mlMapping.DestContent : string.Empty;

                result += addlMsg.IsNotEmptyString() ? $" -> {addlMsg}" : string.Empty;
                result = fmtArgs.IsNotEmpty() ? result.Format(fmtArgs) : result;
            }
        }
        catch { result = string.Empty; }

        return result;
    }

    public static string GetDestContent(this IMultiLanguageObject mlo, string language, string addlMsg = default, params object[] fmtArgs)
    {
        var result = string.Empty;
        try
        {
            using (var diScope = InjectionContext.Root.CreateScope())
            {
                var mlMapping = diScope.Resolve<IMultilangExchanger>().GetAsync(language, mlo.GroupKey, mlo.ItemKey).GetAwaiter().GetResult();
                result = mlMapping != null ? mlMapping.DestContent : "";

                result += addlMsg.IsNotEmptyString() ? $" -> {addlMsg}" : string.Empty;
                result = fmtArgs.IsNotEmpty() ? result.Format(fmtArgs) : result;
            }
        }
        catch { result = string.Empty; }
        return result;
    }

    public static string GetDestContent<TIMultiLanguageObject>(this string language, string addlMsg = default, params object[] fmtArgs) where TIMultiLanguageObject : IMultiLanguageObject
    {
        var mlo = InstanceCreator.Create<TIMultiLanguageObject>();
        if (mlo == null) return string.Empty;
        return mlo.GetDestContent(language, addlMsg, fmtArgs);
    }
}