namespace YogoPop.Core.Injection.Settings;

public static class SettingsGeneratorFactory
{
    private static ISettingsGenerator _settingGenerator;
    private static ISettingsGenerator SettingGenerator
    {
        get
        {
            if (_settingGenerator == null)
                _settingGenerator = new RegularSettingsGenerator();

            return _settingGenerator;
        }
    }

    public static bool ContainsNode(this IConfigurationRoot configRoot, string nodeName)
    {
        return configRoot.AsEnumerable().Where(o => o.Key == nodeName).IsNotEmpty();
    }

    public static IConfigurationBuilder SetGenerator(this IConfigurationBuilder configBuilder, ISettingsGenerator settingsGenerator)
    {
        _settingGenerator = settingsGenerator;

        return configBuilder;
    }

    public static string GetSetting(this IConfiguration config, string rootName)
    {
        return SettingGenerator.GetSetting(config, rootName);
    }

    public static T GetSetting<T>(this IConfiguration config, string rootName) where T : ISettings
    {
        return SettingGenerator.GetSetting<T>(config, rootName);
    }

    public static T GetSetting<T>(this IConfiguration config) where T : ISettings
    {
        return SettingGenerator.GetSetting<T>(config);
    }

    public static object GetSetting(this IConfiguration config, string rootName, Type type)
    {
        return SettingGenerator.GetSetting(config, rootName, type);
    }
}