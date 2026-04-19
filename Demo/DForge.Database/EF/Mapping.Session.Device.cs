namespace DForge.Database.EF;

public class TBSessionDeviceMapping : EFDBEntityMapping<TBSessionDevice>
{
    public override void Configure(EntityTypeBuilder<TBSessionDevice> builder)
    {
        builder.HasKey(o => o.PrimaryKey);
        builder.Property(o => o.Entry).HasConversion(v => v.ToString(), v => v.ToEnum<EntryEnum>());
    }
}