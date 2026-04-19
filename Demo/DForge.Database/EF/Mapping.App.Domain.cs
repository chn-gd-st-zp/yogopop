namespace DForge.Database.EF;

public class TBAppDomainMapping : EFDBEntityMapping<TBAppDomain>
{
    public override void Configure(EntityTypeBuilder<TBAppDomain> builder)
    {
        builder.HasKey(o => o.PrimaryKey);
        builder.Property(o => o.MainStatus).HasConversion(v => v.ToString(), v => v.ToEnum<DMainStatusEnum>());
        builder.Property(o => o.SubStatus).HasConversion(v => v.ToString(), v => v.ToEnum<DSubStatusEnum>());
        builder.Property(o => o.Status).HasConversion(v => v.ToString(), v => v.ToEnum<StatusEnum>());
    }
}