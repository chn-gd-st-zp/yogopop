namespace YogoPop.DB.EFCore;

public abstract partial class EFDBContext : IDBContext
{
    public virtual int ExecuteBySql(string sql, params IDBParameter[] paramArray)
    {
        return Database.ExecuteSqlRaw(sql, paramArray.Parse());
    }

    public virtual async Task<int> ExecuteBySqlAsync(string sql, params IDBParameter[] paramArray)
    {
        return await Database.ExecuteSqlRawAsync(sql, paramArray.Parse());
    }

    public virtual async Task<int> ExecuteBySqlAsync(CancellationToken cancellationToken, string sql, params IDBParameter[] paramArray)
    {
        return await Database.ExecuteSqlRawAsync(sql, paramArray.Parse(), cancellationToken);
    }


    public virtual List<TEntity> QueryBySql<TEntity>(string sql, params IDBParameter[] paramArray) where TEntity : class, IDBEntity, new()
    {
        return Query<TEntity>(CommandType.Text, sql, paramArray.Parse());
    }

    public virtual async Task<List<TEntity>> QueryBySqlAsync<TEntity>(string sql, params IDBParameter[] paramArray) where TEntity : class, IDBEntity, new()
    {
        return await QueryAsync<TEntity>(CommandType.Text, sql, paramArray.Parse());
    }

    public virtual async Task<List<TEntity>> QueryBySqlAsync<TEntity>(CancellationToken cancellationToken, string sql, params IDBParameter[] paramArray) where TEntity : class, IDBEntity, new()
    {
        return await QueryAsync<TEntity>(cancellationToken, CommandType.Text, sql, paramArray.Parse());
    }


    public virtual int ExecuteByStoredProcedure(string sql, params IDBParameter[] paramArray)
    {
        return Database.ExecuteSqlRaw(sql, paramArray.Parse());
    }

    public virtual async Task<int> ExecuteByStoredProcedureAsync(string sql, params IDBParameter[] paramArray)
    {
        return await Database.ExecuteSqlRawAsync(sql, paramArray.Parse());
    }

    public virtual async Task<int> ExecuteByStoredProcedureAsync(CancellationToken cancellationToken, string sql, params IDBParameter[] paramArray)
    {
        return await Database.ExecuteSqlRawAsync(sql, paramArray.Parse(), cancellationToken);
    }


    public virtual List<TEntity> QueryByStoredProcedure<TEntity>(string sql, params IDBParameter[] paramArray) where TEntity : class, IDBEntity, new()
    {
        return Query<TEntity>(CommandType.StoredProcedure, sql, paramArray.Parse());
    }

    public virtual async Task<List<TEntity>> QueryByStoredProcedureAsync<TEntity>(string sql, params IDBParameter[] paramArray) where TEntity : class, IDBEntity, new()
    {
        return await QueryAsync<TEntity>(CommandType.StoredProcedure, sql, paramArray.Parse());
    }

    public virtual async Task<List<TEntity>> QueryByStoredProcedureAsync<TEntity>(CancellationToken cancellationToken, string sql, params IDBParameter[] paramArray) where TEntity : class, IDBEntity, new()
    {
        return await QueryAsync<TEntity>(cancellationToken, CommandType.StoredProcedure, sql, paramArray.Parse());
    }
}