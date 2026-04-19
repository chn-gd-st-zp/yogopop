namespace DForge.Database.EF;

public class TBAppDynSchRecordMapping : EFDBEntityMapping<TBAppDynSchRecord>
{
    public override void Configure(EntityTypeBuilder<TBAppDynSchRecord> builder)
    {
        builder.HasKey(o => o.PrimaryKey);
        builder.Property(o => o.MainType).HasConversion(v => v.ToString(), v => v.ToEnum<DynSchEnum>());
        builder.Property(o => o.Frequency).HasConversion(v => v.ToString(), v => v.ToEnum<DateCycleEnum>());
    }
}