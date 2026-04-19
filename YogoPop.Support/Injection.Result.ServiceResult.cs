namespace YogoPop.Support;

[DIModeForService(DIModeEnum.Exclusive, typeof(IServiceResult<>))]
public class ServiceResult<T> : IServiceResult<T>, ITransient
{
    public bool IsSuccess { get; set; }

    public Exception ExInfo { get; set; }

    public IVEnumItem Code { get; set; }

    public string Msg { get; set; }

    public T Data { get; set; }
}