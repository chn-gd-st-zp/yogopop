namespace YogoPop.Core.Injection.Settings;

public interface ISettings : IInjection, ISingleton
{
    //
}

public interface IStartupSettings : ISettings
{
    public InjectionSettings InjectionSettings { get; set; }
}

public class InjectionSettings : ISettings
{
    public string[] Patterns { get; set; }

    public string[] Dlls { get; set; }
}

public class StartupSettings : IStartupSettings
{
    public InjectionSettings InjectionSettings { get; set; }
}