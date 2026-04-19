namespace YogoPop.Component.MQ.RabbitMQ;

/// <summary>
/// MQ接收消息时，所要执行的业务逻辑
/// </summary>
/// <param name="channel"></param>
/// <param name="sender"></param>
/// <param name="deliverEvent"></param>
public delegate Task MQReceive(IModel channel, object sender, BasicDeliverEventArgs deliverEvent);

public class RabbitMQService<THandle, TParam> : IMQService<THandle, TParam>
    where THandle : RabbitMQHandle<TParam>, new()
    where TParam : class, IMQParams
{
    private readonly THandle _service;
    private TParam _mqParam;

    public RabbitMQService()
    {
        _service = new THandle();
    }

    /// <summary>
    /// 释放服务
    /// </summary>
    public void Dispose()
    {
        if (_service == null || _service.Status == MQProcessEnum.Finished)
            return;

        _service.Dispose();
    }

    /// <summary>
    /// 运行
    /// </summary>
    /// <param name="eMQFunc"></param>
    /// <param name="mqParam"></param>
    public void Run(MQFuncEnum eMQFunc, TParam mqParam = null)
    {
        if (_service == null || _service.Status == MQProcessEnum.Finished)
            return;

        _mqParam = mqParam == null ? _mqParam : mqParam;

        _service.Run(eMQFunc, _mqParam);
    }
}