namespace YogoPop.Component.MQ;

/// <summary>
/// 消息队列参数
/// </summary>
public interface IMQParams
{
    //
}

/// <summary>
/// 消息队列处理器
/// </summary>
/// <typeparam name="TParam"></typeparam>
public interface IMQHandle<TParam> : IDisposable
    where TParam : class, IMQParams
{
    /// <summary>
    /// 处理器状态
    /// </summary>
    MQProcessEnum Status { get; set; }
}

/// <summary>
/// 消息队列服务
/// </summary>
/// <typeparam name="THandle"></typeparam>
/// <typeparam name="TParam"></typeparam>
public interface IMQService<THandle, TParam> : IDisposable
    where THandle : IMQHandle<TParam>
    where TParam : class, IMQParams
{
    /// <summary>
    /// 运行
    /// </summary>
    /// <param name="mqFunc"></param>
    /// <param name="mqParam"></param>
    void Run(MQFuncEnum mqFunc, TParam mqParam = null);
}