namespace YogoPop.Support;

[DIModeForService(DIModeEnum.Exclusive, typeof(IApiResult<>))]
public class ApiResult<T> : IApiResult<T>, ITransient
{
    public int Code { get; set; }

    public string Msg { get; set; }

    public T Data { get; set; }
}