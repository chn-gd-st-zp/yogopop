namespace DForge.Infrastructure;

[DIModeForService(DIModeEnum.AsSelf)]
public class MenuInitialization : IYogoPopInitialization
{
    public async Task Run()
    {
        using (var repository = InjectionContext.Resolve<ISysMenuRepository>())
        {
            var category = SysCategoryEnum.None;
            var obj = default(TBSysMenu);

            category = SysCategoryEnum.SAdmin;
            if (!await repository.AnyAsync(o => o.Category == category && o.Type == SysMenuEnum.None))
            {
                obj = new TBSysMenu
                {
                    Category = category,
                    Type = SysMenuEnum.None,
                    Name = category.GetDesc(),
                    CurNode = category.ToString(),
                };
                obj.SetSequence("1");

                await repository.CreateAsync(obj);
            }

            category = SysCategoryEnum.MAdmin;
            if (!await repository.AnyAsync(o => o.Category == category && o.Type == SysMenuEnum.None))
            {
                obj = new TBSysMenu
                {
                    Category = category,
                    Type = SysMenuEnum.None,
                    Name = category.GetDesc(),
                    CurNode = category.ToString(),
                };
                obj.SetSequence("1");

                await repository.CreateAsync(obj);
            }
        }
    }
}