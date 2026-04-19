namespace YogoPop.Component.MQ.RabbitMQ;

public static class StartupExtension
{
    public static ContainerBuilder RegisRabbitMQ(this ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterGeneric(typeof(RabbitMQService<,>)).As(typeof(IMQService<,>)).InstancePerDependency();

        return containerBuilder;
    }
}