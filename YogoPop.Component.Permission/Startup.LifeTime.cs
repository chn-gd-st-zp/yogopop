namespace YogoPop.Component.Permission;

internal class PermissionLifeTime : IYogoAppLifeTime
{
    private IPermissionInitialization _lifeTime = InjectionContext.Resolve<IPermissionInitialization>();

    public async Task Started(params object[] args)
    {
        var attrs = new List<PermissionBaseAttribute>();

        var allTypeList = AppInitHelper.GetAllType(InjectionContext.Resolve<InjectionSettings>().Patterns);

        allTypeList
            .Select(o => new
            {
                Current = o,
                //PermissionAttrs = o.GetCustomAttributes<GroupPermissionBaseAttribute>().ToList(),
                PermissionAttrs = o.GetCustomAttributes().Where(oo => oo.GetType().IsExtendOf<GroupPermissionBaseAttribute>()).Select(oo => oo as GroupPermissionBaseAttribute).ToList(),
            })
            .Where(o => o.PermissionAttrs.IsNotEmpty())
            .ToList()
            .ForEach(classObj =>
            {
                foreach (var attr in classObj.PermissionAttrs)
                    attrs.Add(attr);

                classObj.Current.GetMethods()
                    .Select(o => new
                    {
                        Current = o,
                        PermissionAttrs = o.GetCustomAttributes().Where(oo => oo.GetType().IsExtendOf<ActionPermissionBaseAttribute>()).Select(ooo => ooo as ActionPermissionBaseAttribute).ToList()
                    })
                    .Where(o => o.PermissionAttrs.IsNotEmpty())
                    .ToList()
                    .ForEach(methodObj =>
                    {
                        foreach (var attr in methodObj.PermissionAttrs)
                            attrs.Add(attr);
                    });
            });

        allTypeList
            .Where(o => o.IsImplementedOf<IPermissionPropertyTag>())
            .Select(o => new
            {
                Current = o,
                PermissionAttrs = o.GetCustomAttributes<GroupPermissionBaseAttribute>().ToList(),
            })
            .ToList()
            .ForEach(classObj =>
            {
                foreach (var attr in classObj.PermissionAttrs)
                    attrs.Add(attr);

                foreach (var property in classObj.Current.GetProperties())
                {
                    foreach (var attr in property.GetCustomAttributes())
                    {
                        if (!attr.GetType().IsExtendOf<PropertyPermissionBaseAttribute>())
                            continue;

                        attrs.Add(attr as PropertyPermissionBaseAttribute);
                    }
                }
            });

        attrs = attrs.GroupBy(o => o.Code).Select(o => o.First()).ToList();

        using (var repository = InjectionContext.Resolve<IPermissionRepository>())
        {
            var deleteList = repository.AllPermission();
            var createList = attrs.Select(o => o.Convert());

            _lifeTime.Operation(ref deleteList, ref createList);

            if (deleteList.IsNotEmpty())
                repository.Delete(deleteList);

            if (createList.IsNotEmpty())
                repository.Create(createList);
        }
    }

    public async Task Stopping(params object[] args) { }

    public async Task Stopped(params object[] args) { }
}