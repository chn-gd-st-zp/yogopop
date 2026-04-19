namespace YogoPop.Support;

public interface IRequestFilterItem : ITransient
{
    string Entrance { get; set; }
    string Action { get; set; }
    object Header { get; set; }
    object ReqParams { get; set; }
    object FuncParams { get; set; }
    object Result4Log { get; set; }
    object Result4Return { get; set; }
    Exception Exception { get; set; }
    DateTime? RequestTime { get; set; }
    DateTime? ResponseTime { get; set; }

    void OnExecuting(object context);

    void OnExecuted(object context);

    void OnException(object context, Exception exception);

    void OnExit(object context);
}