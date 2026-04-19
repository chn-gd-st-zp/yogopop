namespace DForge.Core.Define;

public class TskSAdminSettings : DomainForgeStartupSettings
{
    public RenewRabbitMQSettings RabbitMQSettings { get; set; }

    public DispatcherSettings DispatcherSettings { get; set; }
}