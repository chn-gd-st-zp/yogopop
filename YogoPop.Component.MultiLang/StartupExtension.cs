namespace YogoPop.Component.MultiLang;

public static class StartupExtension
{
    public static ContainerBuilder MultilangFromFile(this ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisMultilang();
        containerBuilder.RegisterType<MultilangFileExchanger>().As<IMultilangExchanger>().InstancePerDependency();

        return containerBuilder;
    }

    public static ContainerBuilder MultilangFromFile(this ContainerBuilder containerBuilder, string defaultLanguage)
    {
        containerBuilder.RegisMultilang();
        containerBuilder.RegisterType<MultilangFileExchanger>().As<IMultilangExchanger>().InstancePerDependency();

        var defaultSettings = new MultilangDefaultSettings
        {
            Language = defaultLanguage,
        };
        containerBuilder.RegisterInstance(defaultSettings).AsSelf().SingleInstance();

        return containerBuilder;
    }

    private static ContainerBuilder RegisMultilang(this ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterGeneric(typeof(MultilangServiceResult<>)).As(typeof(IServiceResult<>)).InstancePerDependency();
        containerBuilder.RegisterGeneric(typeof(MultilangApiResult<>)).As(typeof(IApiResult<>)).InstancePerDependency();
        containerBuilder.RegisterType<MLActionResultExecutor>().As<IActionResultExecutor>().InstancePerDependency();

        return containerBuilder;
    }
}

public static class MultilangExtension
{
    public static Dictionary<MultiLangMappingEnum, List<MultilangMapping>> GetAllMapping(string language = default)
    {
        var result = new Dictionary<MultiLangMappingEnum, List<MultilangMapping>>();
        result.Add(MultiLangMappingEnum.Resource, new List<MultilangMapping>());
        result.Add(MultiLangMappingEnum.Alert, new List<MultilangMapping>());
        result.Add(MultiLangMappingEnum.DBData, new List<MultilangMapping>());

        var injectionSettings = InjectionContext.Resolve<InjectionSettings>();

        var allTypes = AppInitHelper.GetAllType(injectionSettings.Patterns, injectionSettings.Dlls);

        allTypes.Where(o => o.IsEnum)
            .ToList()
            .ForEach(o =>
            {
                var attrs = o.GetCustomAttributes<KVResourceForEnumAttribute>();
                if (attrs.IsNotEmpty())
                {
                    foreach (var enumField in o.ToDictionary())
                    {
                        var dataItem = InjectionContext.Resolve<MultilangMapping>();

                        dataItem.Type = MultiLangMappingEnum.Resource;
                        dataItem.GroupKey = o.Name;
                        dataItem.GroupDescription = o.GetDesc();
                        dataItem.ItemKey = enumField.Value[0];
                        dataItem.ItemDescription = enumField.Value[1];

                        dataItem.GroupDescription = dataItem.GroupDescription.IsNotEmptyString() ? dataItem.GroupDescription : $"{MultiLangMappingEnum.Resource.GetDesc()}-{dataItem.GroupKey}";

                        if (language.IsNotEmptyString())
                        {
                            if (language.ToLower().In("cn", "ch", "zh"))
                            {
                                var attr = o.GetAttr<DescriptionAttribute>(dataItem.ItemKey);
                                dataItem.DestContent = attr != null ? attr.Description : dataItem.DestContent;
                            }

                            if (language.ToLower().In("en"))
                                dataItem.DestContent = dataItem.ItemKey;
                        }

                        result[MultiLangMappingEnum.Resource].Add(dataItem);
                    }
                }
            });

        //allTypes.Where(o => o.IsClass && !o.IsAbstract && !o.IsImplementedOf<IInjection>() && (o.IsImplementedOf<IMultiLanguageObject>() || o.IsImplementedOf<IVException>()))
        allTypes.Where(o => o.IsClass && !o.IsAbstract && !o.IsImplementedOf<IInjection>() && (o.IsImplementedOf<IMultiLanguageObject>()))
            .ToList()
            .ForEach(o =>
            {
                //if (o.IsImplementedOf<IVException>())
                //{
                //    var obj = InstanceCreator.Create(o) as IVException;
                //    if (obj != null)
                //    {
                //        var dataItem = InjectionContext.Resolve<MultilangMapping>();

                //        dataItem.GroupKey = nameof(IVException);
                //        dataItem.GroupDescription = typeof(IVException).GetDesc();
                //        dataItem.ItemKey = obj.Code.Value.ToString();
                //        dataItem.ItemDescription = o.GetDesc();

                //        dataItem.ItemDescription = dataItem.ItemDescription.IsNotEmptyString() ? dataItem.ItemDescription : obj.GetMessage();

                //        result[MultiLangMappingEnum.Alert].Add(dataItem);
                //    }
                //}

                if (o.IsImplementedOf<IMultiLanguageObject>())
                {
                    var obj = InstanceCreator.Create(o) as IMultiLanguageObject;
                    if (obj != null)
                    {
                        var dataItem = InjectionContext.Resolve<MultilangMapping>();

                        dataItem.Type = MultiLangMappingEnum.Alert;
                        dataItem.GroupKey = obj.GroupKey;
                        dataItem.GroupDescription = o.IsImplementedOf<IDBEntity>() ? nameof(IDBEntity) : MultiLangMappingEnum.Alert.GetDesc();
                        dataItem.ItemKey = obj.ItemKey;
                        dataItem.ItemDescription = o.GetDesc();

                        if (language.IsNotEmpty())
                        {
                            if (language.ToLower().In("cn", "ch", "zh"))
                                dataItem.DestContent = o.GetDesc();

                            if (language.ToLower().In("en"))
                                dataItem.DestContent = dataItem.ItemKey;
                        }

                        result[MultiLangMappingEnum.Resource].Add(dataItem);
                    }
                }
            });

        allTypes.Where(o => o.IsClass && !o.IsAbstract && o.IsImplementedOf<IDBEntity>()).Where(o => o.GetProperties().Any(oo => oo.GetCustomAttribute<DBFieldMultiLangAttribute>() != null))
            .ToList()
            .ForEach(o =>
            {
                foreach (var property in o.GetProperties())
                {
                    var attr = property.GetCustomAttribute<DBFieldMultiLangAttribute>();
                    if (attr == null) continue;

                    var dataItem = InjectionContext.Resolve<MultilangMapping>();

                    dataItem.Type = MultiLangMappingEnum.DBData;
                    dataItem.GroupKey = attr.GroupKey;
                    dataItem.GroupDescription = attr.GroupDescription.IsNotEmptyString() ? attr.GroupDescription : o.GetDesc();
                    dataItem.ItemKey = property.Name;
                    dataItem.ItemDescription = o.GetDesc() + "-" + property.GetDesc();

                    result[MultiLangMappingEnum.DBData].Add(dataItem);
                }
            });

        result[MultiLangMappingEnum.Resource] = result[MultiLangMappingEnum.Resource].OrderBy(o => o.GroupKey).ThenBy(o => o.ItemKey).ToList();
        result[MultiLangMappingEnum.Alert] = result[MultiLangMappingEnum.Alert].OrderBy(o => o.GroupKey).ThenBy(o => o.ItemKey).ToList();
        result[MultiLangMappingEnum.DBData] = result[MultiLangMappingEnum.DBData].OrderBy(o => o.GroupKey).ThenBy(o => o.ItemKey).ToList();

        return result;
    }
}