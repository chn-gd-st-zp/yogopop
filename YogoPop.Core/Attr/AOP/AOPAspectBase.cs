namespace YogoPop.Core.Attr.AOP;

public abstract class AOPAspectBase
{
    public virtual object HandleMethod(object source, Func<object[], object> method, Attribute[] triggers, string actionName, object[] actionParams)
    {
        var result = default(object);

        var methodInfo = method.GetMethodInfo();

        try
        {
            Before(source, methodInfo, triggers, actionName, actionParams);
            result = method(actionParams);
        }
        catch (Exception e)
        {
            bool throwException = Error(source, methodInfo, triggers, actionName, actionParams, e);
            if (throwException) throw;
        }
        finally
        {
            result = After(source, methodInfo, triggers, actionName, actionParams, result);
        }

        return result;
    }

    protected virtual void Before(object source, MethodInfo methodInfo, Attribute[] triggers, string actionName, object[] actionParams) { }

    protected virtual bool Error(object source, MethodInfo methodInfo, Attribute[] triggers, string actionName, object[] actionParams, Exception error) => true;

    protected virtual object After(object source, MethodInfo methodInfo, Attribute[] triggers, string actionName, object[] actionParams, object actionResult) { return actionResult; }
}

public abstract class AOPAspectBase<T> where T : class
{
    public virtual T HandleMethod(object source, Func<object[], object> method, Attribute[] triggers, string actionName, object[] actionParams)
    {
        var result = default(T);

        var methodInfo = method.GetMethodInfo();

        try
        {
            result = Before(source, methodInfo, triggers, actionName, actionParams);
            if (result == default)
            {
                result = method(actionParams) as T;
            }
        }
        catch (Exception e)
        {
            bool throwException = Error(source, methodInfo, triggers, actionName, actionParams, e);
            if (throwException) throw;
        }
        finally
        {
            result = After(source, methodInfo, triggers, actionName, actionParams, result);
        }

        return result;
    }

    protected virtual T Before(object source, MethodInfo methodInfo, Attribute[] triggers, string actionName, object[] actionParams) { return default(T); }

    protected virtual bool Error(object source, MethodInfo methodInfo, Attribute[] triggers, string actionName, object[] actionParams, Exception error) => true;

    protected virtual T After(object source, MethodInfo methodInfo, Attribute[] triggers, string actionName, object[] actionParams, T actionResult) { return actionResult; }
}

public abstract class AOPAspectAsyncBase
{
    public virtual object HandleMethod(object source, Func<object[], object> method, Attribute[] triggers, string actionName, object[] actionParams)
    {
        var result = default(object);

        var methodInfo = method.GetMethodInfo();

        try
        {
            Before(source, methodInfo, triggers, actionName, actionParams).GetAwaiter().GetResult();

            result = method(actionParams);
        }
        catch (Exception e)
        {
            bool throwException = Error(source, methodInfo, triggers, actionName, actionParams, e).GetAwaiter().GetResult();
            if (throwException) throw;
        }
        finally
        {
            result = After(source, methodInfo, triggers, actionName, actionParams, result).GetAwaiter().GetResult();
        }

        return result;
    }

    protected virtual async Task Before(object source, MethodInfo methodInfo, Attribute[] triggers, string actionName, object[] actionParams) { }

    protected virtual async Task<bool> Error(object source, MethodInfo methodInfo, Attribute[] triggers, string actionName, object[] actionParams, Exception error) => true;

    protected virtual async Task<object> After(object source, MethodInfo methodInfo, Attribute[] triggers, string actionName, object[] actionParams, object actionResult) { return actionResult; }
}

public abstract class AOPAspectAsyncBase<T> where T : class
{
    public virtual T HandleMethod(object source, Func<object[], object> method, Attribute[] triggers, string actionName, object[] actionParams)
    {
        var result = default(T);

        var methodInfo = method.GetMethodInfo();

        try
        {
            result = Before(source, methodInfo, triggers, actionName, actionParams).GetAwaiter().GetResult();
            if (result == default)
            {
                var sw = Stopwatch.StartNew();
                result = method(actionParams) as T;
                sw.Stop();
            }
        }
        catch (Exception e)
        {
            bool throwException = Error(source, methodInfo, triggers, actionName, actionParams, e).GetAwaiter().GetResult();
            if (throwException)
                throw;
        }
        finally
        {
            result = After(source, methodInfo, triggers, actionName, actionParams, result).GetAwaiter().GetResult();
        }

        return result;
    }

    protected virtual async Task<T> Before(object source, MethodInfo methodInfo, Attribute[] triggers, string actionName, object[] actionParams) { return default(T); }

    protected virtual async Task<bool> Error(object source, MethodInfo methodInfo, Attribute[] triggers, string actionName, object[] actionParams, Exception error) => true;

    protected virtual async Task<T> After(object source, MethodInfo methodInfo, Attribute[] triggers, string actionName, object[] actionParams, T actionResult) { return actionResult; }
}