namespace YogoPop.Core.Tool;

public delegate void LoadRunningSettingsDelegate(IConfigurationBuilder configBuilder, string[] args);

public static class AppInitHelper
{
    public const string LanguageKeyInHeader = "language";

    public static EnvironmentEnum Environment
    {
        get
        {
            EnvironmentEnum environment = EnvironmentEnum.None;

            try
            {
                string env = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                if (!env.IsEmptyString())
                    environment = env.ToEnum<EnvironmentEnum>();
            }
            catch (Exception)
            {
                throw new Exception("无效的环境变量[Environment]");
            }

            return environment;
        }
    }

    public static bool IsOnDev
    {
        get
        {
            bool isOnDev = false;

            try
            {
                var value = System.Environment.GetEnvironmentVariable("ASPNETCORE_ISONDEV");
                if (!value.IsEmptyString())
                    isOnDev = bool.Parse(value);
            }
            catch (Exception)
            {
                throw new Exception("无效的环境变量[IsOnDev]");
            }

            return isOnDev;
        }
    }

    public static bool IsTestMode
    {
        get
        {
            bool isTest = Environment.In(EnvironmentEnum.UAT, EnvironmentEnum.SANDBOX, EnvironmentEnum.PROD) ? false : true;

            try
            {
                var value = System.Environment.GetEnvironmentVariable("ASPNETCORE_ISTESTMODE");
                if (!value.IsEmptyString())
                    isTest = bool.Parse(value);
            }
            catch (Exception)
            {
                throw new Exception("无效的环境变量[IsTestMode]");
            }

            return isTest;
        }
    }

    public static int TimeZone
    {
        get
        {
            int result = 8;

            try
            {
                var value = System.Environment.GetEnvironmentVariable("ASPNETCORE_TIMEZONE");
                if (!value.IsEmptyString())
                    result = int.Parse(value);
            }
            catch (Exception)
            {
                throw new Exception("无效的环境变量[TimeZone]");
            }

            return result;
        }
    }

    public static string RootPath
    {
        get
        {
            string rootPath = AppDomain.CurrentDomain.BaseDirectory.ToPath();
            return rootPath.EndsWith("/") ? rootPath.Remove(rootPath.Length - 1) : rootPath;
        }
    }

    public static string GeneratePath(PathModeEnum pathMode, string path)
    {
        var rootPath = pathMode == PathModeEnum.ABS ?
            path
            :
            (RootPath + "/" + (path.StartsWith("/") ? path.Substring(1) : path))
            ;

        return rootPath.EndsWith("/") ? rootPath.Remove(rootPath.Length - 1) : rootPath;
    }

    public static List<string> GetPaths(ExternalFileEnum externalFileEnum, string[] patterns = null, string[] fileNames = null)
    {
        string suffix = "." + externalFileEnum.ToString().ToLower();

        var filePaths = new List<string>();

        if ((patterns == null || patterns.Length == 0) && (fileNames == null || fileNames.Length == 0))
            return filePaths;

        if (patterns == null || patterns.Length == 0)
        {
            Directory.GetFiles(RootPath)
                .ToList()
                .ForEach(o =>
                {
                    var fileName = fileNames.Where(oo => o.EndsWith(oo + suffix, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
                    if (!fileName.IsEmptyString())
                        filePaths.Add(o);
                });
        }
        else
        {
            foreach (var pattern in patterns)
                filePaths.AddRange(Directory.GetFiles(RootPath, pattern + suffix).ToList());
        }

        return filePaths;
    }

    public static List<Assembly> GetAllAssemblies(string[] patterns = null, string[] dlls = null)
    {
        List<Assembly> result = new List<Assembly>();

        if (patterns != null && patterns.Length > 0)
            patterns = patterns.Select(o => o.Replace("*", string.Empty)).ToArray();

        var assemblyNameList_source = DependencyContext.Default.RuntimeLibraries.Select(o => o.Name).ToList();
        var assemblyNameList_target = new List<string>();

        if (assemblyNameList_target.IsEmpty() && dlls.IsNotEmpty())
        {
            foreach (var dll in dlls)
            {
                var sourceName = assemblyNameList_source
                    .Where(sourceName => dll.IsEquals(sourceName))
                    .FirstOrDefault();

                if (!sourceName.IsEmptyString() && !assemblyNameList_target.Any(targetName => targetName.IsEquals(sourceName)))
                    assemblyNameList_target.Add(sourceName);
            }
        }

        if (assemblyNameList_target.IsEmpty() && patterns.IsNotEmpty())
        {
            foreach (var pattern in patterns)
            {
                var pat = pattern.Replace("*", string.Empty);
                pat = pat.EndsWith(".") ? pat : pat + ".";

                assemblyNameList_source
                    .Where(sourceName => sourceName.StartsWith(pat, StringComparison.OrdinalIgnoreCase))
                    .ToList()
                    .ForEach(sourceName =>
                    {
                        if (!assemblyNameList_target.Any(targetName => targetName.IsEquals(sourceName)))
                            assemblyNameList_target.Add(sourceName);
                    });
            }
        }

        foreach (var assemblyName in assemblyNameList_target)
        {
            if (!File.Exists(RootPath + "/" + assemblyName + ".dll"))
                continue;

            result.Add(Assembly.Load(new AssemblyName(assemblyName)));
        }

        return result;
    }

    public static List<Type> GetAllType(string[] patterns = null, string[] dlls = null)
    {
        List<Type> result = new List<Type>();

        GetAllAssemblies(patterns, dlls)
            .ForEach(assembly =>
            {
                var types = assembly.GetTypes();
                result.AddRange(types);
            });

        return result.OrderBy(o => o.FullName).ToList();
    }

    public static IConfigurationBuilder LoadConfiguration(this IConfigurationBuilder configBuilder, params string[] configFiles)
    {
        var envName = Environment.ToString();
        Printor.PrintText("环境变量: " + envName);

        configBuilder
            .SetBasePath(RootPath)
            .AddJsonFile($"appsettings.json", true, true);

        var midName = $"{envName.ToLower()}{(IsOnDev ? ".local" : "")}";

        if (Environment != EnvironmentEnum.None)
            configBuilder.AddJsonFile($"appsettings.{midName}.json", true, true);

        if (configFiles != null)
        {
            foreach (var file in configFiles)
                configBuilder.AddJsonFile(file, true, true);
        }

        return configBuilder;
    }

    public static IConfigurationBuilder LoadRunningSettings(this IConfigurationBuilder configBuilder, string[] args, params LoadRunningSettingsDelegate[] funcArray)
    {
        args = args.IsNotEmpty() ? args : new string[0];

        Printor.PrintText("启动参数: ");
        Printor.PrintText("{");
        args.ToList().ForEach(o => { Printor.PrintText("  " + o); });
        Printor.PrintText("}");
        Printor.PrintLine();

        foreach (var func in funcArray)
            func(configBuilder, args);

        return configBuilder;
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        List<Type> allTypeInApp = new List<Type>();

        GetAllAssemblies()
            .ForEach(o =>
            {
                try
                {
                    var types = o.GetTypes().Where(o => o.IsClass && !o.IsAbstract && o.IsImplementedOf<IAutoMapperProfile>()).ToList();
                    if (types.IsNotEmpty())
                        allTypeInApp.AddRange(types);
                }
                catch
                {
                    //
                }
            });

        if (allTypeInApp.IsNotEmpty())
            services.AddAutoMapper(allTypeInApp.ToArray());

        return services;
    }
}