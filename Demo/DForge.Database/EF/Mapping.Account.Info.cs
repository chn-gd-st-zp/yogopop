namespace DForge.Database.EF;

public class TBAccountInfoMapping : EFDBEntityMapping<TBAccountInfo>
{
    public override void Configure(EntityTypeBuilder<TBAccountInfo> builder)
    {
        builder.HasKey(o => o.PrimaryKey);
        builder.Property(o => o.RoleType).HasConversion(v => v.ToString(), v => v.ToEnum<RoleEnum>());
        builder.Property(o => o.UserType).HasConversion(v => v.ToString(), v => v.ToEnum<UserTypeEnum>());
        builder.Property(o => o.Gender).HasConversion(v => v.ToString(), v => v.ToEnum<GenderEnum>());
        builder.Property(o => o.Language).HasConversion(v => v.ToString(), v => v.ToEnum<LanguageEnum>());
        builder.Property(o => o.Status).HasConversion(v => v.ToString(), v => v.ToEnum<StatusEnum>());
    }
}