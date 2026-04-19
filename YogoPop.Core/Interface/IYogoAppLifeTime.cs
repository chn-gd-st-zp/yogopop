namespace YogoPop.Core.Interface;

public interface IYogoAppLifeTime
{
    public Task Started(params object[] args);

    public Task Stopping(params object[] args);

    public Task Stopped(params object[] args);
}