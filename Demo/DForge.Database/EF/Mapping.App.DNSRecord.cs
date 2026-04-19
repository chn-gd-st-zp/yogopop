namespace DForge.Database.EF;

public class TBAppDNSRecordMapping : EFDBEntityMapping<TBAppDNSRecord>
{
    public override void Configure(EntityTypeBuilder<TBAppDNSRecord> builder)
    {
        builder.HasKey(o => o.PrimaryKey);
        builder.Property(o => o.Type).HasConversion(v => v.ToString(), v => v.ToEnum<DNSRecordEnum>());
    }
}