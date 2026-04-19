namespace YogoPop.Component.Auth;

public interface IYogoSessionContext : ITransient
{
    public string ContextID { get; set; }

    public string TypeOfTokenProvider { get; set; }

    public OperationTypeEnum OperationType { get; set; }

    public void RestoreContextID();

    public Task SaveAsync();
}