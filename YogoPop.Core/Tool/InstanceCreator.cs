namespace YogoPop.Core.Tool;

public static class InstanceCreator
{
    public static T Create<T>()
    {
        return Activator.CreateInstance<T>();
    }

    public static object Create(Type type, params object[] args)
    {
        return Activator.CreateInstance(type, args);
    }

    public static object CreateGenericType(Type type, params Type[] genericTypes)
    {
        type = type.MakeGenericType(genericTypes);
        return Activator.CreateInstance(type);
    }

    public static object CreateGenericType(Type type, Type[] genericTypes, params object[] args)
    {
        type = type.MakeGenericType(genericTypes);
        return Activator.CreateInstance(type, args);
    }

    public static object Create(string assemblyName, string className)
    {
        try
        {
            Assembly assembly = Assembly.Load(assemblyName);
            if (assembly == null)
                throw new ApplicationException("找不到应用程序: " + assemblyName);

            object obj = assembly.CreateInstance(className);
            if (obj == null)
                throw new ApplicationException("找不到类: " + className);

            return obj;
        }
        catch
        {
            throw;
        }
    }

    public static T DeepCopy<T>(this T obj)
    {
        try
        {
            return obj.ToJson().ToObject<T>();
        }
        catch
        {
            throw;
        }
    }

    public static T DeepCopy<T>(this object obj)
    {
        try
        {
            return obj.ToJson().ToObject<T>();
        }
        catch
        {
            throw;
        }
    }
}