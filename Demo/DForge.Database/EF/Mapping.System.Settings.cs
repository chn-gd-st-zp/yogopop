namespace DForge.Database.EF;

public class TBSysSettingsMapping : EFDBEntityMapping<TBSysSettings>
{
    public override void Configure(EntityTypeBuilder<TBSysSettings> builder)
    {
        builder.HasKey(o => o.PrimaryKey);
        builder.Property(o => o.Status).HasConversion(v => v.ToString(), v => v.ToEnum<StatusEnum>());
        builder.Property(o => o.Type).HasConversion(v => v.ToString(), v => v.ToEnum<SysSettingsEnum>());
    }
}