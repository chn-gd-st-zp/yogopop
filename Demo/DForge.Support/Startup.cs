namespace DForge.Support;

public abstract class DomainForgeStartup<TStartupSettings> : StartupBase<TStartupSettings, AppConfigure> where TStartupSettings : DomainForgeStartupSettings
{
    public DomainForgeStartup(IConfiguration configuration) : base(configuration) { }

    protected override JsonSerializerSettings SetJsonSerializerSettings()
    {
        return new JsonSerializerSettings()
        {
            Formatting = Formatting.None,
            //DateFormatString = "yyyy-MM-dd HH:mm:ss.fff",
            DateFormatString = "yyyy-MM-dd HH:mm:ss",
            //ContractResolver = new DefaultContractResolver(),
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new List<JsonConverter> {
                new StringEnumConverter(),
            },
        };
    }

    protected void RegisEF(ContainerBuilder containerBuilder)
    {
        var efOptionsBuilder = new EFDBContextOptionsBuilder<DomainForgeEFDBContext>()
        {
            BulidAction = (optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(InjectionContext.Resolve<DBConnectionSettings>().Main);
                if (AppInitHelper.Environment == EnvironmentEnum.PROD)
                {
                    // 禁用日志记录
                    optionsBuilder.UseLoggerFactory(NullLoggerFactory.Instance);
                }

                return optionsBuilder.Options;
            }
        };
        containerBuilder.Register(o => efOptionsBuilder).AsSelf().InstancePerDependency();

        containerBuilder.RegisterType<DomainForgeEFDBContext>().As<IDBContext>().InstancePerDependency();
        containerBuilder.RegisterType<DomainForgeEFDBContext>().As<DomainForgeEFDBContext>().InstancePerDependency();
        containerBuilder.RegisterType<DomainForgeEFDBContext>().Keyed<IDBContext>(ORMTypeEnum.EF).InstancePerDependency();
        containerBuilder.RegisterType<DomainForgeEFDBContext>().Keyed<DomainForgeEFDBContext>(ORMTypeEnum.EF).InstancePerDependency();
    }

    protected string PrintResponseCode(params int[] ignores)
    {
        var printList = new SortedDictionary<int, string>();

        var statCodeType = typeof(RenewStateCode);
        var stateCodeObj = InstanceCreator.Create<RenewStateCode>();
        var properties = statCodeType.GetProperties();

        foreach (var property in properties.Where(o => o.PropertyType.IsImplementedOf<IVEnumItem>()))
        {
            var ve = property.GetValue(stateCodeObj) as IVEnumItem;
            printList.Add(ve.Value, property.Name);
        }

        var result = "";
        var maxLength = printList.Max(o => o.Key).ToString().Length;

        foreach (var kv in printList)
        {
            if (kv.Key.In(ignores)) continue;

            var code = kv.Key.ToString().PadLeft(maxLength, '*');
            var text = kv.Value;

            code = code.Replace("*", Printor.PrintHtmlSpace(1));

            result += $"<br/>{code}：{text}";
        }

        return result;
    }
}

public class CustomAuthorizeFilter : IDashboardAuthorizationFilter
{
    public bool Authorize([NotNull] DashboardContext context)
    {
        //var httpcontext = context.GetHttpContext();
        //return httpcontext.User.Identity.IsAuthenticated;
        return true;
    }
}