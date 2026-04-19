namespace DForge.Infrastructure.Helper;

public class RunningTaskManager
{
    public static bool IsExist(string taskName)
    {
        using (var scope = InjectionContext.Root.CreateScope())
        using (var cache = scope.Resolve<ICache4Redis>())
            return cache.Exists(taskName);
    }

    public static void Add(string taskName)
    {
        if (IsExist(taskName)) return;

        using (var scope = InjectionContext.Root.CreateScope())
        using (var cache = scope.Resolve<ICache4Redis>())
            cache.Set(taskName, DateTimeExtension.Now, DateTimeExtension.Now.AddMinutes(1));
    }

    public static void Remove(string taskName)
    {
        if (!IsExist(taskName)) return;

        using (var scope = InjectionContext.Root.CreateScope())
        using (var cache = scope.Resolve<ICache4Redis>())
            cache.Del(taskName);
    }
}