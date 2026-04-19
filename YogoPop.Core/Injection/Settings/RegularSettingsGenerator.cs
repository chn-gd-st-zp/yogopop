namespace YogoPop.Core.Injection.Settings;

public class RegularSettingsGenerator : ISettingsGenerator
{
    public virtual string GetSetting(IConfiguration config, string rootName)
    {
        return config[rootName];
    }

    public virtual T GetSetting<T>(IConfiguration config, string rootName) where T : ISettings
    {
        return config.GetSection(rootName).Get<T>();
    }

    public virtual T GetSetting<T>(IConfiguration config) where T : ISettings
    {
        return config.Get<T>();
    }

    public virtual object GetSetting(IConfiguration config, string rootName, Type type)
    {
        return config.GetSection(rootName).Get(type);
    }
}