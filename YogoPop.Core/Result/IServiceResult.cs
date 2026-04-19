namespace YogoPop.Core.Result;

public interface IServiceResult<TData>
{
    public bool IsSuccess { get; set; }

    public Exception ExInfo { get; set; }

    public IVEnumItem Code { get; set; }

    public string Msg { get; set; }

    public TData Data { get; set; }
}