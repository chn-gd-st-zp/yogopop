namespace YogoPop.Core.Injection.Settings;

public interface ISettingsGenerator
{
    public string GetSetting(IConfiguration config, string rootName);

    public T GetSetting<T>(IConfiguration config, string rootName) where T : ISettings;

    public T GetSetting<T>(IConfiguration config) where T : ISettings;

    public object GetSetting(IConfiguration config, string rootName, Type type);
}