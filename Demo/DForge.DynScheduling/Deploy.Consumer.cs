namespace DForge.DynScheduling;

[DIModeForService(DIModeEnum.AsSelf)]
public class DynSchConsumer : ITransient
{
    public async Task RunAsync(DynSchEnum dynSch)
    {
        if (InjectionContext.Resolve<RabbitMQSettings>() == null) return;

        using (var diScope = InjectionContext.Root.CreateScope())
        {
            var _logger = InjectionContext.Resolve<IYogoLogger<DynSchConsumer>>();

            var _mqConsumer = InjectionContext.Resolve<IMQService<RouteConsumer, RouteConsumerParams>>();
            if (_mqConsumer == null)
            {
                _logger.Error($"Created Fail");
                return;
            }

            _logger.Info($"Start Listening");

            try
            {
                _mqConsumer.Run(MQFuncEnum.Exec, new RouteConsumerParams
                {
                    Topic = DynSchMQSettings.Topic,
                    RoutingKey = dynSch.ToString(),
                    AutoAck = true,
                    BusinessFunc = MQReceive,
                });
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}", ex);
            }
        }
    }

    private async Task MQReceive(IModel channel, object sender, BasicDeliverEventArgs deliverEvent)
    {
        using (var diScope = InjectionContext.Root.CreateScope())
        {
            var _logger = diScope.Resolve<IYogoLogger<DynSchConsumer>>();

            _logger.Info($"Process Start");

            try
            {
                var message = Encoding.UTF8.GetString(deliverEvent.Body.ToArray());
                if (message.IsEmptyString()) return;

                var msg = diScope.Resolve<IDynSchMQMsgConvertor>().Restore(message);
                if (msg == null) return;

                _logger.Info(msg);

                await diScope.ResolveByKeyed<IDynSchActuator>(msg.Record.MainType).RunAsync(msg);
            }
            catch (Exception ex)
            {
                _logger.Info($"Has Error");
                _logger.Error($"{ex.Message}", ex);
            }

            _logger.Info($"Process End");
        }
    }
}