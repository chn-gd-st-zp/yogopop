namespace DForge.Database.EF;

public class TBSysRoleMapping : EFDBEntityMapping<TBSysRole>
{
    public override void Configure(EntityTypeBuilder<TBSysRole> builder)
    {
        builder.HasKey(o => o.PrimaryKey);
        builder.Property(o => o.Status).HasConversion(v => v.ToString(), v => v.ToEnum<StatusEnum>());
        builder.Property(o => o.Type).HasConversion(v => v.ToString(), v => v.ToEnum<RoleEnum>());
        builder.Property(o => o.SubType).HasConversion(v => v.ToString(), v => v.ToEnum<UserTypeEnum>());
    }
}