namespace DForge.Database.EF;

public class TBAccountAdminMapping : EFDBEntityMapping<TBAccountAdmin>
{
    public override void Configure(EntityTypeBuilder<TBAccountAdmin> builder)
    {
        builder.HasKey(o => o.PrimaryKey);
        builder.Property(o => o.Status).HasConversion(v => v.ToString(), v => v.ToEnum<StatusEnum>());
    }
}