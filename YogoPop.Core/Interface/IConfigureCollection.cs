namespace YogoPop.Core.Interface;

public interface IConfigureCollection
{
    public IApplicationBuilder App { get; set; }

    public IHostEnvironment Env { get; set; }

    public IHostApplicationLifetime Lifetime { get; set; }

    public ILoggerFactory LoggerFactory { get; set; }

    public IApiVersionDescriptionProvider ApiVerDescProvider { get; set; }
}