namespace YogoPop.DB.EFCore;

public abstract class EFDBEntityMapping<TDBEntity> : IEntityTypeConfiguration<TDBEntity> where TDBEntity : class, IDBEntity
{
    public abstract void Configure(EntityTypeBuilder<TDBEntity> builder);
}