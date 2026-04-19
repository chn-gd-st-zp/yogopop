namespace DForge.DynScheduling;

[DIModeForService(DIModeEnum.AsSelf)]
public class DynSchPublisher : ITransient
{
    public async Task RunAsync<TDynSchRecordEntity>(DynSchEnum dynSch, params DynSchMQMsg[] msgs) where TDynSchRecordEntity : class, IDynSchRecordEntity, new()
    {
        if (msgs.IsEmpty()) return;

        if (InjectionContext.Resolve<RabbitMQSettings>() == null) return;

        using (var diScope = InjectionContext.Root.CreateScope())
        {
            var _logger = diScope.Resolve<IYogoLogger<DynSchPublisher>>();

            try
            {
                var repository = diScope.Resolve<IDBRepository>();
                if (!await repository.DBContext.CreateAsync(msgs.Select(o => o.Record as TDynSchRecordEntity)))
                {
                    _logger.Error($"Record Create Failed - {msgs.ToJson()}");
                    return;
                }

                using (var mqPublisher = diScope.Resolve<IMQService<RoutePublisher, RoutePublisherParams>>())
                {
                    if (mqPublisher == null)
                    {
                        _logger.Error($"Can Not Resolve Publisher");
                        return;
                    }

                    mqPublisher.Run(MQFuncEnum.Exec, new RoutePublisherParams
                    {
                        Topic = DynSchMQSettings.Topic,
                        RoutingKey = dynSch.ToString(),
                        MessageEntities = msgs.Select(o => o as IRabbitMQMessageEntity).ToList(),
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(new
                {
                    Msgs = msgs,
                }, ex);
            }
        }
    }
}