namespace DForge.Database.EF;

public class TBAppProjectMapping : EFDBEntityMapping<TBAppProject>
{
    public override void Configure(EntityTypeBuilder<TBAppProject> builder)
    {
        builder.HasKey(o => o.PrimaryKey);
        builder.Property(o => o.Status).HasConversion(v => v.ToString(), v => v.ToEnum<StatusEnum>());
    }
}