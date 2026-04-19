namespace DForge.Database.EF;

public class TBSysMenuMapping : EFDBEntityMapping<TBSysMenu>
{
    public override void Configure(EntityTypeBuilder<TBSysMenu> builder)
    {
        builder.HasKey(o => o.PrimaryKey);
        builder.Property(o => o.Status).HasConversion(v => v.ToString(), v => v.ToEnum<StatusEnum>());
        builder.Property(o => o.Category).HasConversion(v => v.ToString(), v => v.ToEnum<SysCategoryEnum>());
        builder.Property(o => o.Type).HasConversion(v => v.ToString(), v => v.ToEnum<SysMenuEnum>());
    }
}