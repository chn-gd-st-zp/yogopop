namespace YogoPop.Support;

public class AppConfigure : IConfigureCollection
{
    public IApplicationBuilder App { get; set; }

    public IHostEnvironment Env { get; set; }

    public IHostApplicationLifetime Lifetime { get; set; }

    public ILoggerFactory LoggerFactory { get; set; }

    public IApiVersionDescriptionProvider ApiVerDescProvider { get; set; }
}