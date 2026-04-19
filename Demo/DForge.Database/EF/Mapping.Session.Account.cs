namespace DForge.Database.EF;

public class TBSessionAccountMapping : EFDBEntityMapping<TBSessionAccount>
{
    public override void Configure(EntityTypeBuilder<TBSessionAccount> builder)
    {
        builder.HasKey(o => o.PrimaryKey);
        builder.Property(o => o.RoleType).HasConversion(v => v.ToString(), v => v.ToEnum<RoleEnum>());
    }
}