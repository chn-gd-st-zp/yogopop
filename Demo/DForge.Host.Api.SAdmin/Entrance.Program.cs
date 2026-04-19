namespace DForge.Host.Api.SAdmin;

public class Program
{
    public static void Main(string[] args)
    {
        var hostBuilder = CreateHostBuilder(args);
        var host = hostBuilder.Build();
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        var result = Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.LoadConfiguration();
            })
            .UseSeriLogger()
            .ConfigureWebHostDefaults(hostBuilder =>
            {
                hostBuilder.UseKestrel();
                hostBuilder.UseStartup<Startup>();
            })
            .UseServiceProviderFactory(new AutofacServiceProviderFactory());

        return result;
    }
}