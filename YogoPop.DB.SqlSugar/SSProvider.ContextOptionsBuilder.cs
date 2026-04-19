namespace YogoPop.DB.SqlSugar;

public class SSDBContextOptionBuilder : ConnectionConfig, IDBContextOptionsBuilder { public OptionsBuilderAction<ConnectionConfig> BulidAction { get; set; } }

public class SSDBContextOptionsBuilder : List<ConnectionConfig>, IDBContextOptionsBuilder { public OptionsBuilderAction<List<ConnectionConfig>> BulidAction { get; set; } }