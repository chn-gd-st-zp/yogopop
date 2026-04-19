namespace DForge.Core.Define;

public abstract class DomainForgeStartupSettings : StartupSettings
{
    public SwaggerSettings SwaggerSettings { get; set; }

    public RenewRabbitMQSettings RabbitMQSettings { get; set; }
}