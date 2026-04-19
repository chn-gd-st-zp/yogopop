namespace YogoPop.Component.DataVisibility;

public static class DataVisibilityExtension
{
    private static IEnumerable<IDataVisiElement> _dataVisiElements;

    public static IEnumerable<IDataVisiElement> DataVisiElements()
    {
        if (_dataVisiElements.IsNotEmpty()) return _dataVisiElements;

        var injectionSettings = InjectionContext.Resolve<InjectionSettings>();
        var types = AppInitHelper.GetAllType(injectionSettings.Patterns, injectionSettings.Dlls);
        var result = types.Where(o => o.IsImplementedOf<IDataVisiElement>()).Select(o => InstanceCreator.Create(o) as IDataVisiElement).ToList();
        if (result.IsNotEmpty()) _dataVisiElements = result;

        return _dataVisiElements;
    }

    public static IQueryable<TDataVisiSource> DataVisibility<TDataVisiAssign, TDataVisiSource, TPrimaryKey>(this IQueryable<TDataVisiSource> query, string sourceKey, params string[] identityKeys)
        where TDataVisiAssign : class, IDataVisiAssign
        where TDataVisiSource : class, IDataVisiSource<TPrimaryKey>
    {
        using (var diScope = InjectionContext.Root.CreateScope())
        using (var repository = diScope.Resolve<IDataVisiAssignRepository<TDataVisiAssign>>())
        {
            var dataVisiAssigns = repository.LoadAsync(sourceKey: sourceKey, identityKeys: identityKeys).GetAwaiter().GetResult();
            if (dataVisiAssigns.IsEmpty()) return query;

            var dataVisiAssign = dataVisiAssigns.FirstOrDefault();
            if (dataVisiAssign == null || dataVisiAssign.DataVision.In(DataVisionEnum.None, DataVisionEnum.All)) return query;

            var exp = diScope.ResolveByKeyed<IDataVisiExpression<TDataVisiAssign, TDataVisiSource, TPrimaryKey>>(dataVisiAssign.DataVision, sourceKey);
            if (exp == null) return query;

            var predicate = exp.ToExpression(dataVisiAssign);

            return query.Where(predicate);
        }
    }
}