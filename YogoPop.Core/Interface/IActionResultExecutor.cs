namespace YogoPop.Core.Interface;

public interface IActionResultExecutor : ITransient
{
    public object Execute(object data);
}