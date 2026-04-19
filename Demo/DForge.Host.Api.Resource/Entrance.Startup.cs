namespace DForge.Host.Api.Resource;

public class Startup : DomainForgeApiStartup<ApiResourceSettings>
{
    public Startup(IConfiguration configuration) : base(configuration) { }

    protected override void Extend_ConfigureServices(IServiceCollection services)
    {
        base.Extend_ConfigureServices(services);
    }

    protected override void Extend_ConfigureContainer(ContainerBuilder containerBuilder)
    {
        base.Extend_ConfigureContainer(containerBuilder);
    }

    protected override void Extend_Configure(AppConfigure configures)
    {
        base.Extend_Configure(configures);

        var path = AppInitHelper.RootPath + CurConfig.SystemSettings.AttachmentDirectory;
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        configures.App.UseStaticFiles(new StaticFileOptions()
        {
            RequestPath = new PathString(CurConfig.SystemSettings.AttachmentDirectory),
            FileProvider = new PhysicalFileProvider(path),
            ServeUnknownFileTypes = true,
        });
    }
}