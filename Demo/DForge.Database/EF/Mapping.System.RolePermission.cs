namespace DForge.Database.EF;

public class TBSysRolePermissionMapping : EFDBEntityMapping<TBSysRolePermission>
{
    public override void Configure(EntityTypeBuilder<TBSysRolePermission> builder)
    {
        builder.HasKey(o => o.PrimaryKey);
    }
}