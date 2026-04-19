namespace YogoPop.Core.Tool;

public interface ICuncurrencyTaskResult<TTrigger, TData>
{
    public TTrigger Trigger { get; set; }
    public bool IsSuccess { get; set; }
    public string ErrorMsg { get; set; }
    public Exception ErrorObj { get; set; }
    public TData Data { get; set; }
}

public class CuncurrencyTaskResult<TTrigger, TData> : ICuncurrencyTaskResult<TTrigger, TData>
{
    public TTrigger Trigger { get; set; } = default;
    public bool IsSuccess { get; set; } = default;
    public string ErrorMsg { get; set; } = string.Empty;
    public Exception ErrorObj { get; set; } = default;
    public TData Data { get; set; } = default;
}

public static class Concurrency
{
    public static async Task<IEnumerable<TOutput>> Run<TOutput, TOutputData, TInput>(
        int maxConcurrency, TimeSpan timeout, IEnumerable<TInput> triggerObjs,
        Func<TInput, Task<TOutput>> action
    ) where TOutput : ICuncurrencyTaskResult<TInput, TOutputData>
    {
        var result = new List<TOutput>();
        var successCount = 0;

        using var diScope = InjectionContext.Root.CreateScope();
        using var semaphore = new SemaphoreSlim(maxConcurrency);
        var tasks = triggerObjs.Select(async triggerObj =>
        {
            await semaphore.WaitAsync();
            try
            {
                var actionTask = action(triggerObj);
                var timerTask = Task.Delay(timeout);
                if (await Task.WhenAny(actionTask, timerTask) != timerTask)
                {
                    var execResult = await actionTask;
                    if (execResult.IsSuccess)
                        Interlocked.Increment(ref successCount);

                    result.Add(execResult);
                }
                else
                {
                    var o = diScope.Resolve<TOutput>();
                    if (o != null)
                    {
                        var obj = o as ICuncurrencyTaskResult<TInput, TOutputData>;
                        if (obj != null)
                        {
                            obj.Trigger = triggerObj;
                            obj.IsSuccess = false;
                            obj.ErrorMsg = "timeout";

                            result.Add((TOutput)obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var o = diScope.Resolve<TOutput>();
                if (o != null)
                {
                    var obj = o as ICuncurrencyTaskResult<TInput, TOutputData>;
                    if (obj != null)
                    {
                        obj.Trigger = triggerObj;
                        obj.IsSuccess = false;
                        obj.ErrorMsg = ex.Message;
                        obj.ErrorObj = ex;

                        result.Add((TOutput)obj);
                    }
                }
            }
            finally
            {
                semaphore.Release();
            }
        });
        await Task.WhenAll(tasks);

        return result;
    }

    public static async Task<IEnumerable<TOutput>> Run<TOutput, TOutputData, TInput, TInputExt1>(
        int maxConcurrency, TimeSpan timeout, IEnumerable<TInput> triggerObjs,
        Func<TInput, TInputExt1, Task<TOutput>> action,
        TInputExt1 inputExt1
    ) where TOutput : ICuncurrencyTaskResult<TInput, TOutputData>
    {
        var result = new List<TOutput>();
        var successCount = 0;

        using var diScope = InjectionContext.Root.CreateScope();
        using var semaphore = new SemaphoreSlim(maxConcurrency);
        var tasks = triggerObjs.Select(async triggerObj =>
        {
            await semaphore.WaitAsync();
            try
            {
                var actionTask = action(triggerObj, inputExt1);
                var timerTask = Task.Delay(timeout);
                if (await Task.WhenAny(actionTask, timerTask) != timerTask)
                {
                    var execResult = await actionTask;
                    if (execResult.IsSuccess)
                        Interlocked.Increment(ref successCount);

                    result.Add(execResult);
                }
                else
                {
                    var o = diScope.Resolve<TOutput>();
                    if (o != null)
                    {
                        var obj = o as ICuncurrencyTaskResult<TInput, TOutputData>;
                        if (obj != null)
                        {
                            obj.Trigger = triggerObj;
                            obj.IsSuccess = false;
                            obj.ErrorMsg = "timeout";

                            result.Add((TOutput)obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var o = diScope.Resolve<TOutput>();
                if (o != null)
                {
                    var obj = o as ICuncurrencyTaskResult<TInput, TOutputData>;
                    if (obj != null)
                    {
                        obj.Trigger = triggerObj;
                        obj.IsSuccess = false;
                        obj.ErrorMsg = ex.Message;
                        obj.ErrorObj = ex;

                        result.Add((TOutput)obj);
                    }
                }
            }
            finally
            {
                semaphore.Release();
            }
        });
        await Task.WhenAll(tasks);

        return result;
    }

    public static async Task<IEnumerable<TOutput>> Run<TOutput, TOutputData, TInput, TInputExt1, TInputExt2>(
        int maxConcurrency, TimeSpan timeout, IEnumerable<TInput> triggerObjs,
        Func<TInput, TInputExt1, TInputExt2, Task<TOutput>> action,
        TInputExt1 inputExt1, TInputExt2 inputExt2
    ) where TOutput : ICuncurrencyTaskResult<TInput, TOutputData>, new()
    {
        var result = new List<TOutput>();
        var successCount = 0;

        using var diScope = InjectionContext.Root.CreateScope();
        using var semaphore = new SemaphoreSlim(maxConcurrency);
        var tasks = triggerObjs.Select(async triggerObj =>
        {
            await semaphore.WaitAsync();
            try
            {
                var actionTask = action(triggerObj, inputExt1, inputExt2);
                var timerTask = Task.Delay(timeout);
                if (await Task.WhenAny(actionTask, timerTask) != timerTask)
                {
                    var execResult = await actionTask;
                    if (execResult.IsSuccess)
                        Interlocked.Increment(ref successCount);

                    result.Add(execResult);
                }
                else
                {
                    var o = diScope.Resolve<TOutput>();
                    if (o != null)
                    {
                        var obj = o as ICuncurrencyTaskResult<TInput, TOutputData>;
                        if (obj != null)
                        {
                            obj.Trigger = triggerObj;
                            obj.IsSuccess = false;
                            obj.ErrorMsg = "timeout";

                            result.Add((TOutput)obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var o = diScope.Resolve<TOutput>();
                if (o != null)
                {
                    var obj = o as ICuncurrencyTaskResult<TInput, TOutputData>;
                    if (obj != null)
                    {
                        obj.Trigger = triggerObj;
                        obj.IsSuccess = false;
                        obj.ErrorMsg = ex.Message;
                        obj.ErrorObj = ex;

                        result.Add((TOutput)obj);
                    }
                }
            }
            finally
            {
                semaphore.Release();
            }
        });
        await Task.WhenAll(tasks);

        return result;
    }

    public static async Task<IEnumerable<TOutput>> Run<TOutput, TOutputData, TInput, TInputExt1, TInputExt2, TInputExt3>(
        int maxConcurrency, TimeSpan timeout, IEnumerable<TInput> triggerObjs,
        Func<TInput, TInputExt1, TInputExt2, TInputExt3, Task<TOutput>> action,
        TInputExt1 inputExt1, TInputExt2 inputExt2, TInputExt3 inputExt3
    ) where TOutput : ICuncurrencyTaskResult<TInput, TOutputData>, new()
    {
        var result = new List<TOutput>();
        var successCount = 0;

        using var diScope = InjectionContext.Root.CreateScope();
        using var semaphore = new SemaphoreSlim(maxConcurrency);
        var tasks = triggerObjs.Select(async triggerObj =>
        {
            await semaphore.WaitAsync();
            try
            {
                var actionTask = action(triggerObj, inputExt1, inputExt2, inputExt3);
                var timerTask = Task.Delay(timeout);
                if (await Task.WhenAny(actionTask, timerTask) != timerTask)
                {
                    var execResult = await actionTask;
                    if (execResult.IsSuccess)
                        Interlocked.Increment(ref successCount);

                    result.Add(execResult);
                }
                else
                {
                    var o = diScope.Resolve<TOutput>();
                    if (o != null)
                    {
                        var obj = o as ICuncurrencyTaskResult<TInput, TOutputData>;
                        if (obj != null)
                        {
                            obj.Trigger = triggerObj;
                            obj.IsSuccess = false;
                            obj.ErrorMsg = "timeout";

                            result.Add((TOutput)obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var o = diScope.Resolve<TOutput>();
                if (o != null)
                {
                    var obj = o as ICuncurrencyTaskResult<TInput, TOutputData>;
                    if (obj != null)
                    {
                        obj.Trigger = triggerObj;
                        obj.IsSuccess = false;
                        obj.ErrorMsg = ex.Message;
                        obj.ErrorObj = ex;

                        result.Add((TOutput)obj);
                    }
                }
            }
            finally
            {
                semaphore.Release();
            }
        });
        await Task.WhenAll(tasks);

        return result;
    }
}