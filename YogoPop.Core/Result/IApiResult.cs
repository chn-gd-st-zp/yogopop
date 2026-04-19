namespace YogoPop.Core.Result;

public interface IApiResult
{
    public int Code { get; set; }

    public string Msg { get; set; }
}

public interface IApiResult<TData> : IApiResult
{
    public new int Code { get; set; }

    public new string Msg { get; set; }

    public TData Data { get; set; }
}