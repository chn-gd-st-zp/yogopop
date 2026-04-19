namespace YogoPop.DB.EFCore;

public class EFDBContextOptionsBuilder : DbContextOptionsBuilder, IDBContextOptionsBuilder { public OptionsBuilderAction<DbContextOptionsBuilder, DbContextOptions> BulidAction { get; set; } }

public class EFDBContextOptionsBuilder<TDBcontext> : EFDBContextOptionsBuilder { }