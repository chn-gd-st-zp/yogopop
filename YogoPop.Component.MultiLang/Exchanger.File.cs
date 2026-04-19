namespace YogoPop.Component.MultiLang;

public interface IMultilangFileExchanger : IMultilangExchanger
{
    //
}

[DIModeForService(DIModeEnum.Exclusive, typeof(IMultilangFileExchanger))]
public class MultilangFileExchanger : IMultilangFileExchanger
{
    private readonly MultilangSettings _multilangSettings;

    public MultilangFileExchanger()
    {
        _multilangSettings = InjectionContext.Resolve<MultilangSettings>();
        if (_multilangSettings.Address.IsNotEmptyString())
        {
            var fullPath = AppInitHelper.RootPath.CombinePath(_multilangSettings.Address);
            if (!Directory.Exists(fullPath)) Directory.CreateDirectory(fullPath);
        }
    }

    private string SolveFilePath(ref string destLanguage)
    {
        if (destLanguage.IsEmptyString()) return string.Empty;

        destLanguage = destLanguage.ToLower();

        var filePath = AppInitHelper.RootPath.CombinePath(_multilangSettings.Address, destLanguage + ".json");
        if (!File.Exists(filePath)) File.Create(filePath).Close();

        return filePath;
    }

    private async Task UpdateFile(string destLanguage, List<MultilangMapping> datas)
    {
        var filePath = SolveFilePath(ref destLanguage);
        if (filePath.IsEmptyString()) return;

        var now = DateTimeExtension.Now;
        await File.WriteAllTextAsync(filePath, datas.ToJson(Formatting.Indented));
        File.SetLastWriteTime(filePath, now);
    }

    public async Task<List<MultilangMapping>> LoadAsync(string destLanguage)
    {
        var filePath = SolveFilePath(ref destLanguage);
        if (filePath.IsEmptyString()) return default;

        var jsonData = await File.ReadAllTextAsync(filePath);
        var dataList = jsonData.ToObject<List<MultilangMapping>>();
        if (dataList.IsNotEmpty())
            dataList = dataList.Where(o => o.DestLanguage == destLanguage).ToList();
        else
        {
            dataList = new List<MultilangMapping>();

            foreach (var typeMapping in MultilangExtension.GetAllMapping(destLanguage))
                dataList.AddRange(typeMapping.Value);

            if (dataList.IsNotEmpty())
            {
                dataList.ForEach(o => o.DestLanguage = destLanguage);
                dataList = dataList
                    .OrderBy(o => o.Type)
                    .ThenBy(o => o.GroupKey)
                    .ThenBy(o => o.ItemKey)
                    .ToList();
                await UpdateFile(destLanguage, dataList);
            }
        }

        using var diScope = InjectionContext.Root.CreateScope();
        using var cache = diScope.ResolveCache<ICache4Redis>(_multilangSettings);
        await cache.SetAsync(destLanguage, dataList);

        return dataList;
    }

    public async Task<bool> SetAsync(string destLanguage, MultilangMapping updateItem)
    {
        var filePath = SolveFilePath(ref destLanguage);
        if (filePath.IsEmptyString()) return false;

        var mtlDataSet = await LoadAsync(destLanguage);

        var mtlDataItem = mtlDataSet.Where(o => o.GroupKey == updateItem.GroupKey && o.ItemKey == updateItem.ItemKey).SingleOrDefault();
        if (mtlDataItem == null)
            mtlDataSet.Add(mtlDataItem);
        else
            mtlDataItem = updateItem.AdaptTo(mtlDataItem);

        await UpdateFile(destLanguage, mtlDataSet);

        return true;
    }

    public async Task<List<MultilangMapping>> GetAsync(string destLanguage)
    {
        var filePath = SolveFilePath(ref destLanguage);
        if (filePath.IsEmptyString()) return new List<MultilangMapping>();

        var result = default(List<MultilangMapping>);

        using var diScope = InjectionContext.Root.CreateScope();
        using var cache = diScope.ResolveCache<ICache4Redis>(_multilangSettings);
        result = await cache.GetAsync<List<MultilangMapping>>(destLanguage);

        return result;
    }

    public async Task<MultilangMapping> GetAsync(string destLanguage, string groupKey, string itemKey)
    {
        var mtlDataSet = await GetAsync(destLanguage);
        if (mtlDataSet == null) return null;
        return mtlDataSet.Where(o => o.GroupKey == groupKey && o.ItemKey == itemKey).SingleOrDefault();
    }
}