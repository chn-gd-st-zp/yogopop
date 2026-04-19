namespace DForge.Database.EF;

public class TBSysPermissionMapping : EFDBEntityMapping<TBSysPermission>
{
    public override void Configure(EntityTypeBuilder<TBSysPermission> builder)
    {
        builder.HasKey(o => o.PrimaryKey);
        builder.Property(o => o.Type).HasConversion(v => v.ToString(), v => v.ToEnum<PermissionTypeEnum>());
    }
}