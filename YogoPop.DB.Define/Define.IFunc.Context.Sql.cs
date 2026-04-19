namespace YogoPop.DB.Define;

public partial interface IDBContextFunc
{
    public int ExecuteBySql(string sql, params IDBParameter[] paramArray);

    public Task<int> ExecuteBySqlAsync(string sql, params IDBParameter[] paramArray);

    public Task<int> ExecuteBySqlAsync(CancellationToken cancellationToken, string sql, params IDBParameter[] paramArray);


    public List<TEntity> QueryBySql<TEntity>(string sql, params IDBParameter[] paramArray) where TEntity : class, IDBEntity, new();

    public Task<List<TEntity>> QueryBySqlAsync<TEntity>(string sql, params IDBParameter[] paramArray) where TEntity : class, IDBEntity, new();

    public Task<List<TEntity>> QueryBySqlAsync<TEntity>(CancellationToken cancellationToken, string sql, params IDBParameter[] paramArray) where TEntity : class, IDBEntity, new();


    public int ExecuteByStoredProcedure(string sql, params IDBParameter[] paramArray);

    public Task<int> ExecuteByStoredProcedureAsync(string sql, params IDBParameter[] paramArray);

    public Task<int> ExecuteByStoredProcedureAsync(CancellationToken cancellationToken, string sql, params IDBParameter[] paramArray);


    public List<TEntity> QueryByStoredProcedure<TEntity>(string sql, params IDBParameter[] paramArray) where TEntity : class, IDBEntity, new();

    public Task<List<TEntity>> QueryByStoredProcedureAsync<TEntity>(string sql, params IDBParameter[] paramArray) where TEntity : class, IDBEntity, new();

    public Task<List<TEntity>> QueryByStoredProcedureAsync<TEntity>(CancellationToken cancellationToken, string sql, params IDBParameter[] paramArray) where TEntity : class, IDBEntity, new();
}