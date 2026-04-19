namespace YogoPop.DB.Define;

public abstract partial class DBRepository
{
    public virtual int ExecuteBySql(string sql, params IDBParameter[] paramArray)
    {
        return DBContext.ExecuteBySql(sql, paramArray);
    }

    public virtual async Task<int> ExecuteBySqlAsync(string sql, params IDBParameter[] paramArray)
    {
        return await DBContext.ExecuteBySqlAsync(sql, paramArray);
    }

    public virtual async Task<int> ExecuteBySqlAsync(CancellationToken cancellationToken, string sql, params IDBParameter[] paramArray)
    {
        return await DBContext.ExecuteBySqlAsync(cancellationToken, sql, paramArray);
    }


    public virtual List<TEntity> QueryBySql<TEntity>(string sql, params IDBParameter[] paramArray) where TEntity : class, IDBEntity, new()
    {
        return DBContext.QueryBySql<TEntity>(sql, paramArray);
    }

    public virtual async Task<List<TEntity>> QueryBySqlAsync<TEntity>(string sql, params IDBParameter[] paramArray) where TEntity : class, IDBEntity, new()
    {
        return await DBContext.QueryBySqlAsync<TEntity>(sql, paramArray);
    }

    public virtual async Task<List<TEntity>> QueryBySqlAsync<TEntity>(CancellationToken cancellationToken, string sql, params IDBParameter[] paramArray) where TEntity : class, IDBEntity, new()
    {
        return await DBContext.QueryBySqlAsync<TEntity>(cancellationToken, sql, paramArray);
    }


    public virtual int ExecuteByStoredProcedure(string sql, params IDBParameter[] paramArray)
    {
        return DBContext.ExecuteByStoredProcedure(sql, paramArray);
    }

    public virtual async Task<int> ExecuteByStoredProcedureAsync(string sql, params IDBParameter[] paramArray)
    {
        return await DBContext.ExecuteByStoredProcedureAsync(sql, paramArray);
    }

    public virtual async Task<int> ExecuteByStoredProcedureAsync(CancellationToken cancellationToken, string sql, params IDBParameter[] paramArray)
    {
        return await DBContext.ExecuteByStoredProcedureAsync(cancellationToken, sql, paramArray);
    }


    public virtual List<TEntity> QueryByStoredProcedure<TEntity>(string sql, params IDBParameter[] paramArray) where TEntity : class, IDBEntity, new()
    {
        return DBContext.QueryByStoredProcedure<TEntity>(sql, paramArray);
    }

    public virtual async Task<List<TEntity>> QueryByStoredProcedureAsync<TEntity>(string sql, params IDBParameter[] paramArray) where TEntity : class, IDBEntity, new()
    {
        return await DBContext.QueryByStoredProcedureAsync<TEntity>(sql, paramArray);
    }

    public virtual async Task<List<TEntity>> QueryByStoredProcedureAsync<TEntity>(CancellationToken cancellationToken, string sql, params IDBParameter[] paramArray) where TEntity : class, IDBEntity, new()
    {
        return await DBContext.QueryByStoredProcedureAsync<TEntity>(cancellationToken, sql, paramArray);
    }
}