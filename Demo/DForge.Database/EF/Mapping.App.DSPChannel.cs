namespace DForge.Database.EF;

public class TBAppDSPChannelMapping : EFDBEntityMapping<TBAppDSPChannel>
{
    public override void Configure(EntityTypeBuilder<TBAppDSPChannel> builder)
    {
        builder.HasKey(o => o.PrimaryKey);
        builder.Property(o => o.DSP).HasConversion(v => v.ToString(), v => v.ToEnum<DSPEnum>());
        builder.Property(o => o.Status).HasConversion(v => v.ToString(), v => v.ToEnum<StatusEnum>());
    }
}