namespace YogoPop.DB.EFCore;

public abstract partial class EFDBContext : IDBContext
{
    public virtual object OrderBy<TEntity, TSort>(object queryable, IDTOSearch<TSort> sorts) where TSort : IDTOSort, new()
    {
        var query = queryable as IQueryable<TEntity>;
        if (query == null)
            return query;

        Type sourceType = typeof(TEntity);
        PropertyInfo[] sourcePropertyInfos = sourceType.GetProperties();

        var idtoSorts = sorts == null || sorts.Sorts == null ? InstanceCreator.CreateGenericType(typeof(List<>), typeof(TSort)) as List<TSort> : sorts.Sorts;

        if (idtoSorts.IsEmpty())
        {
            var attr = sourceType.GetDefaultSortField<TEntity>();
            if (attr != null)
                idtoSorts.Add(new TSort() { FieldName = attr.RealName, Direction = attr.Direction });
        }

        var mi = typeof(EFExtension).GetMethod(nameof(EFExtension.AppendOrderBy));

        bool firstAdd = true;
        foreach (var sortItem in idtoSorts)
        {
            if (sortItem.FieldName.IsEmptyString())
                continue;

            var sortFieldInfo = sourcePropertyInfos.GetSortField<TEntity>(sortItem);
            if (sortFieldInfo == null)
                continue;

            query = mi
                .MakeGenericMethod(new[] { sourceType, sortFieldInfo.Item1.PropertyType })
                .Invoke(null, new object[] { query, sortItem.Direction, firstAdd, sourceType, sortFieldInfo.Item2 }) as IQueryable<TEntity>;

            firstAdd = false;
        }

        return query;
    }


    public virtual string GetNextSequence<TEntity>() where TEntity : class, IDBEntity, IDBFSequence, new()
    {
        var query = GetQueryable<TEntity>();

        var obj = query.OrderByDescending(o => o.CurSequence).FirstOrDefault();
        if (obj == null)
            obj = new TEntity();

        return obj.GetSequence(1);
    }

    public virtual async Task<string> GetNextSequenceAsync<TEntity>() where TEntity : class, IDBEntity, IDBFSequence, new()
    {
        var query = GetQueryable<TEntity>();

        var obj = await query.OrderByDescending(o => o.CurSequence).FirstOrDefaultAsync();
        if (obj == null)
            obj = new TEntity();

        return obj.GetSequence(1);
    }


    public virtual List<IDBEntity> QuerySql(Type dbType, CommandType commandType, string sql, List<SqlParameter> paramList)
    {
        var result = new List<IDBEntity>();

        if (!dbType.IsImplementedOf<IDBEntity>())
            return result;

        var type = dbType;

        using (var connection = new SqlConnection(Database.GetDbConnection().ConnectionString))
        using (var cmd = connection.CreateCommand())
        {
            cmd.CommandType = commandType;
            cmd.CommandText = sql;

            if (paramList.IsNotEmpty())
            {
                paramList.ForEach(o =>
                {
                    if (o.IsNullable && o.Value == null)
                        o.Value = DBNull.Value;
                });

                cmd.Parameters.AddRange(paramList.ToArray());
            }

            connection.Open();

            try
            {
                using (var reader = cmd.ExecuteReader())
                {
                    var columnSchema = reader.GetColumnSchema();

                    while (reader.Read())
                    {
                        var item = InstanceCreator.Create(dbType);

                        foreach (var pi in type.GetProperties())
                        {
                            if (pi.GetCustomAttribute<NotMappedAttribute>() != null)
                                continue;

                            object value = null;

                            var attr = pi.GetCustomAttribute<ColumnAttribute>();
                            var colName = attr != null ? attr.Name : pi.Name;

                            if (reader.IsColumnExist(colName))
                            {
                                object obj_regular = reader[colName];

                                if (obj_regular == DBNull.Value)
                                    value = null;
                                else if (pi.PropertyType.IsExtendOf<Enum>())
                                    value = pi.PropertyType.ToEnum(obj_regular.ToString());
                                else
                                    value = obj_regular.TypeTo(pi.PropertyType);
                            }

                            pi.SetValue(item, value);
                        }

                        result.Add(item as IDBEntity);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        return result;
    }

    public virtual async Task<List<IDBEntity>> QuerySqlAsync(Type dbType, CommandType commandType, string sql, List<SqlParameter> paramList)
    {
        var result = new List<IDBEntity>();

        if (!dbType.IsImplementedOf<IDBEntity>())
            return result;

        var type = dbType;

        using (var connection = new SqlConnection(Database.GetDbConnection().ConnectionString))
        using (var cmd = connection.CreateCommand())
        {
            cmd.CommandType = commandType;
            cmd.CommandText = sql;

            if (paramList.IsNotEmpty())
            {
                paramList.ForEach(o =>
                {
                    if (o.IsNullable && o.Value == null)
                        o.Value = DBNull.Value;
                });

                cmd.Parameters.AddRange(paramList.ToArray());
            }

            connection.Open();

            try
            {
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    var columnSchema = reader.GetColumnSchema();

                    while (reader.Read())
                    {
                        var item = InstanceCreator.Create(dbType);

                        foreach (var pi in type.GetProperties())
                        {
                            if (pi.GetCustomAttribute<NotMappedAttribute>() != null)
                                continue;

                            object value = null;

                            var attr = pi.GetCustomAttribute<ColumnAttribute>();
                            var colName = attr != null ? attr.Name : pi.Name;

                            if (reader.IsColumnExist(colName))
                            {
                                var obj_regular = reader[colName];

                                if (obj_regular == DBNull.Value)
                                    value = null;
                                else if (pi.PropertyType.IsExtendOf<Enum>())
                                    value = pi.PropertyType.ToEnum(obj_regular.ToString());
                                else
                                    value = obj_regular.TypeTo(pi.PropertyType);
                            }

                            pi.SetValue(item, value);
                        }

                        result.Add(item as IDBEntity);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        return result;
    }

    public virtual async Task<List<IDBEntity>> QuerySqlAsync(CancellationToken cancellationToken, Type dbType, CommandType commandType, string sql, List<SqlParameter> paramList)
    {
        var result = new List<IDBEntity>();

        if (!dbType.IsImplementedOf<IDBEntity>())
            return result;

        var type = dbType;

        using (var connection = new SqlConnection(Database.GetDbConnection().ConnectionString))
        using (var cmd = connection.CreateCommand())
        {
            cmd.CommandType = commandType;
            cmd.CommandText = sql;

            if (paramList.IsNotEmpty())
            {
                paramList.ForEach(o =>
                {
                    if (o.IsNullable && o.Value == null)
                        o.Value = DBNull.Value;
                });

                cmd.Parameters.AddRange(paramList.ToArray());
            }

            connection.Open();

            try
            {
                using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
                {
                    var columnSchema = reader.GetColumnSchema();

                    while (reader.Read())
                    {
                        var item = InstanceCreator.Create(dbType);

                        foreach (var pi in type.GetProperties())
                        {
                            if (pi.GetCustomAttribute<NotMappedAttribute>() != null)
                                continue;

                            object value = null;

                            var attr = pi.GetCustomAttribute<ColumnAttribute>();
                            var colName = attr != null ? attr.Name : pi.Name;

                            if (reader.IsColumnExist(colName))
                            {
                                object obj_regular = reader[colName];

                                if (obj_regular == DBNull.Value)
                                    value = null;
                                else if (pi.PropertyType.IsExtendOf<Enum>())
                                    value = pi.PropertyType.ToEnum(obj_regular.ToString());
                                else
                                    value = obj_regular.TypeTo(pi.PropertyType);
                            }

                            pi.SetValue(item, value);
                        }

                        result.Add(item as IDBEntity);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        return result;
    }


    public virtual List<TEntity> Query<TEntity>(CommandType commandType, string sql, List<SqlParameter> paramList) where TEntity : class, IDBEntity, new()
    {
        var dataList = QuerySql(typeof(TEntity), commandType, sql, paramList);

        var result = dataList.Select(o => o as TEntity).ToList();

        return result;
    }

    public virtual async Task<List<TEntity>> QueryAsync<TEntity>(CommandType commandType, string sql, List<SqlParameter> paramList) where TEntity : class, IDBEntity, new()
    {
        var dataList = await QuerySqlAsync(typeof(TEntity), commandType, sql, paramList);

        var result = dataList.Select(o => o as TEntity).ToList();

        return result;
    }

    public virtual async Task<List<TEntity>> QueryAsync<TEntity>(CancellationToken cancellationToken, CommandType commandType, string sql, List<SqlParameter> paramList) where TEntity : class, IDBEntity, new()
    {
        var dataList = await QuerySqlAsync(cancellationToken, typeof(TEntity), commandType, sql, paramList);

        var result = dataList.Select(o => o as TEntity).ToList();

        return result;
    }
}