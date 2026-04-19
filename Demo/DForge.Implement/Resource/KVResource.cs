namespace DForge.Implement.Resource;

[DIModeForService(DIModeEnum.Exclusive, typeof(IKVResourceService<>))]
public partial class KVResourceService<TTokenProvider> : ApiResourceService<KVResourceService<TTokenProvider>, ICache, TTokenProvider>, IKVResourceService<TTokenProvider> where TTokenProvider : ITokenProvider
{
    private InjectionSettings _injectionSettings = InjectionContext.Resolve<InjectionSettings>();

    public async Task<IServiceResult<SortedDictionary<string, object>>> ResourceData(LanguageEnum language = LanguageEnum.EN)
    {
        SortedDictionary<string, object> result = null;
        IDictionary<string, string> resultItem = null;

        var iden = $"{UserTypeEnum.None}";
        var enumAttrList = new List<Type> { typeof(PublicEnumAttribute) };
        if (!Session.TokenProvider.CurrentToken.IsEmptyString())
        {
            using (var repository = InjectionContext.Resolve<IDBRepository>())
            {
                var accountInfo = await repository.DBContext.SingleAsync<TBAccountInfo>(o => o.PrimaryKey == CurAccountID);
                if (accountInfo == null) return result.Fail<SortedDictionary<string, object>, LogicFailed>();
                var role = await repository.DBContext.SingleAsync<TBSysRole>(o => o.PrimaryKey == accountInfo.RoleID);
                if (role == null) return result.Fail<SortedDictionary<string, object>, LogicFailed>();
                switch (role.Type)
                {
                    case RoleEnum.SuperAdmin:
                    case RoleEnum.Admin:
                        iden = $"{RoleEnum.SuperAdmin}&{RoleEnum.Admin}";
                        enumAttrList.Add(typeof(InternalEnumAttribute));
                        break;
                    case RoleEnum.User:
                        if (role.SubType.In(UserTypeEnum.Normal))
                            iden = $"{UserTypeEnum.Normal}";
                        enumAttrList.Add(typeof(UserVisibleEnumAttribute));
                        break;
                }
            }
        }

        var mloDatas = await InjectionContext.Resolve<IMultilangExchanger>().GetAsync(language.ToString());
        if (mloDatas.IsEmpty()) return result.Fail<SortedDictionary<string, object>, LogicFailed>();

        var key = SystemSettings.KVDataCacheKey + language.ToString() + ":" + iden;
        using (var cache = InjectionContext.Resolve<ICache4Redis>())
        {
            if (cache == null) return result.Fail<SortedDictionary<string, object>, LogicFailed>();
            result = (await cache.GetAsync<string>(key)).ToObject<SortedDictionary<string, object>>();
            if (result == null)
            {
                result = new SortedDictionary<string, object>();

                using (var client = cache.GetClient<RedisClient>())
                using (var redisLock = client.Lock(key, 1, false))
                {
                    if (redisLock == null) return result.Fail<SortedDictionary<string, object>, LogicFailed>();

                    result.Add("SystemSettings", new
                    {
                        TimeZone = AppInitHelper.TimeZone.ToString(),
                        PasswordRegex = SystemSettings.PasswordRegex,
                        UserNameRegex = SystemSettings.UserNameRegex,
                    });

                    resultItem = new FixedSortedDictionary<string, string>();
                    resultItem.Add("None", "默认、无");
                    PermissionExtension.AccessRecordCategory()
                        .ToList()
                        .ForEach(o =>
                        {
                            var groupKey = nameof(IAccessRecordTrigger);
                            var itemKey = o.Key;
                            var mlo = mloDatas.Where(o => o.GroupKey == groupKey && o.ItemKey == itemKey).SingleOrDefault();
                            var content = mlo != null ? mlo.DestContent : "";
                            content = content.IsNotEmptyString() ? content : $"{groupKey}-{itemKey}-MLNoMapping";
                            resultItem.Add(o.Key, content);
                            //resultItem.Add(o.Key, o.Value);
                        });
                    result.Add("AccressRecordCategory", resultItem);

                    AppInitHelper.GetAllType(_injectionSettings.Patterns, _injectionSettings.Dlls)
                        .Select(o => new
                        {
                            EnumType = o,
                            KVPublicAttr = o.GetCustomAttribute<PublicEnumAttribute>(),
                            KVAllAttrs = o.GetCustomAttributes<KVResourceForEnumAttribute>(true),
                        })
                        .Where(o => o.KVAllAttrs.IsNotEmpty())
                        .Select(o => new
                        {
                            EnumType = o.EnumType,
                            KVPublicAttr = o.KVPublicAttr,
                            KVAllAttrs = o.KVAllAttrs,
                            KVAllAttrTypeFullNameArray = o.KVAllAttrs.Select(oo => oo.GetType().FullName).ToArray(),
                        })
                        .Where(o => enumAttrList.Select(oo => oo.FullName).Any(oo => oo.In(o.KVAllAttrTypeFullNameArray)))
                        .ToList()
                        .ForEach(o =>
                        {
                            resultItem = new FixedSortedDictionary<string, string>();

                            var enumInfoDic = o.KVPublicAttr != null && o.KVPublicAttr.CheckItem ? o.EnumType.ToDictionary(enumAttrList) : o.EnumType.ToDictionary();
                            if (enumInfoDic.Values.Any())
                            {
                                foreach (var item in enumInfoDic.OrderBy(o => o.Key))
                                {
                                    try
                                    {
                                        var groupKey = o.EnumType.Name;
                                        var itemKey = item.Value[0];
                                        var mlo = mloDatas.Where(o => o.GroupKey == groupKey && o.ItemKey == itemKey).SingleOrDefault();
                                        var content = mlo != null ? mlo.DestContent : "";
                                        content = content.IsNotEmptyString() ? content : $"{groupKey}-{itemKey}-MLNoMapping";
                                        resultItem.Add(item.Value[0], content);
                                        //resultItem.Add(item.Value[0], item.Value[1]);
                                    }
                                    catch (Exception ex)
                                    {
                                        throw;
                                    }
                                }

                                result.Add(o.EnumType.Name, resultItem);
                            }
                        });
                }
            }

            if (result.IsNotEmpty())
                await cache.SetAsync(key, result, TimeSpan.FromMinutes(SystemSettings.KVDataCacheMaintainMinutes));
        }

        return result.Success<SortedDictionary<string, object>, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.SysTool_KVRefresh, GlobalPermissionEnum.SysTool)]
    public async Task<IServiceResult<bool>> Refresh()
    {
        var languageList = EnumExtension.ToEnumList<LanguageEnum>();
        languageList.Remove(LanguageEnum.None);
        languageList.Remove(LanguageEnum.Unknown);

        using (var scope = InjectionContext.Root.CreateScope())
        {
            var mlExchanger = scope.Resolve<IMultilangExchanger>();

            using (var cache = scope.Resolve<ICache4Redis>())
            {
                languageList.ForEach(async o =>
                {
                    await cache.DelAsync(SystemSettings.KVDataCacheKey + o.ToString() + $":{UserTypeEnum.None}");
                    await cache.DelAsync(SystemSettings.KVDataCacheKey + o.ToString() + $":{RoleEnum.SuperAdmin}&{RoleEnum.Admin}");
                    await cache.DelAsync(SystemSettings.KVDataCacheKey + o.ToString() + $":{UserTypeEnum.Normal}");
                    await mlExchanger.LoadAsync(o.ToString());
                });
            }

            //if (Session.CurrentAccount.AccountInfo.RoleType != RoleEnum.SuperAdmin)
            //    return true.Success<bool, LogicSucceed>();

            //if (SysStatus == SysStatusEnum.Running)
            //    return true.Success<bool, LogicSucceed>();
        }

        return true.Success<bool, LogicSucceed>();
    }
}