namespace DForge.Database.EF;

public class TBSysAccessRecordMapping : EFDBEntityMapping<TBSysAccessRecord>
{
    public override void Configure(EntityTypeBuilder<TBSysAccessRecord> builder)
    {
        builder.HasKey(o => o.PrimaryKey);
        builder.Property(o => o.RoleType).HasConversion(v => v.ToString(), v => v.ToEnum<RoleEnum>());
        builder.Property(o => o.OperationType).HasConversion(v => v.ToString(), v => v.ToEnum<OperationTypeEnum>());
    }
}