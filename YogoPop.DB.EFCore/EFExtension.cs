namespace YogoPop.DB.EFCore;

public static class EFExtension
{
    public static List<SqlParameter> Parse(this IDBParameter[] paramArray)
    {
        if (paramArray == null)
            return new List<SqlParameter>();

        return paramArray
            .Select(o => new SqlParameter
            {
                ParameterName = o.Name,
                Value = o.Value,
                IsNullable = o.IsNullable,
                Direction = o.Direction,
                DbType = o.DBType,
                Size = o.Size,
            })
            .ToList();
    }

    public static bool IsColumnExist(this DbDataReader dr, string columnName)
    {
        dr.GetSchemaTable().DefaultView.RowFilter = "ColumnName= '" + columnName + "'";
        return (dr.GetSchemaTable().DefaultView.IsNotEmpty());
    }

    public static IQueryable<TEntity> FilteDelete<TEntity>(this IQueryable<TEntity> query)
    {
        var entityType = typeof(TEntity);

        if (!entityType.IsImplementedOf(typeof(IDBEntity)))
            return query;

        if (!entityType.IsImplementedOf(typeof(IDBFStatus<>)))
            return query;

        var dbfStatus = entityType.GetGenericInterface(typeof(IDBFStatus<>));

        foreach (var fProperty in dbfStatus.GetProperties())
        {
            foreach (var eProperty in entityType.GetProperties())
            {
                if (eProperty.Name != fProperty.Name)
                    continue;

                if (eProperty.GetCustomAttributes<NotMappedAttribute>().IsNotEmpty())
                    return query;
            }
        }

        var statusEnum = dbfStatus.GetGenericArguments()[0];

        var appendFilteDelete = typeof(EFExtension).GetMethod(nameof(AppendFilteDelete));
        if (appendFilteDelete == null)
            return query;

        query = (IQueryable<TEntity>)appendFilteDelete
            .MakeGenericMethod(new[] { entityType, statusEnum })
            .Invoke(null, new object[] { query });

        return query;
    }

    public static IQueryable<TEntity> AppendFilteDelete<TEntity, TStatusEnum>(this IQueryable<TEntity> query) where TEntity : class, IDBFStatus<TStatusEnum> where TStatusEnum : Enum
    {
        var attr = typeof(TStatusEnum).GetCustomAttribute<DeleteDeclareAttribute>();
        if (attr == null)
            return query;

        if (typeof(TEntity).GetProperty(nameof(IDBFStatus<TStatusEnum>.Status)).GetCustomAttribute<NotMappedAttribute>() != null)
            return query;

        return query.Where(o => !o.Status.Equals(attr.DeleteTag));
    }

    public static IQueryable<TEntity> AppendOrderBy<TEntity, TFieldName>(this IQueryable<TEntity> query, SortDirectionEnum sortDirection, bool firstAdd, Type type, string fieldName)
    {
        ParameterExpression parameter = Expression.Parameter(type, "p");
        MemberExpression body = Expression.PropertyOrField(parameter, fieldName);
        Expression<Func<TEntity, TFieldName>> expression = Expression.Lambda<Func<TEntity, TFieldName>>(body, parameter);

        if (firstAdd)
        {
            if (sortDirection == SortDirectionEnum.ASC)
            {
                query = query.OrderBy(expression);
            }
            else
            {
                query = query.OrderByDescending(expression);
            }
        }
        else
        {
            if (sortDirection == SortDirectionEnum.ASC)
            {
                query = ((IOrderedQueryable<TEntity>)query).ThenBy(expression);
            }
            else
            {
                query = ((IOrderedQueryable<TEntity>)query).ThenByDescending(expression);
            }
        }

        return query;
    }

    public static PropertyEntry<TEntity, TProperty> GetPropertyEntry<TEntity, TProperty>(this EFDBContext dbContext, TEntity entity, Expression<Func<TEntity, TProperty>> propertyPredicate) where TEntity : class, IDBEntity, new()
    {
        if (dbContext == null || entity == null || propertyPredicate == null)
            return null;

        return dbContext.Entry(entity).Property(propertyPredicate);
    }


    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate) => condition ? query.Where(predicate) : query;

    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, int, bool>> predicate) => condition ? query.Where(predicate) : query;

    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> query, bool condition, Func<T, bool> predicate) => condition ? query.Where(predicate) : query;


    public static Expression<Func<T, bool>> GetTrue<T>() => f => true;

    public static Expression<Func<T, bool>> GetFalse<T>() => f => false;

    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second) => first.AndAlso(second, Expression.AndAlso);

    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second) => first.AndAlso(second, Expression.OrElse);

    private static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2, Func<Expression, Expression, BinaryExpression> func)
    {
        var parameter = Expression.Parameter(typeof(T));
        var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
        var left = leftVisitor.Visit(expr1.Body);
        var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
        var right = rightVisitor.Visit(expr2.Body);
        return Expression.Lambda<Func<T, bool>>(
        func(left, right), parameter);
    }

    private class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;
        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public override Expression Visit(Expression node)
        {
            if (node == _oldValue)
                return _newValue;
            return base.Visit(node);
        }
    }
}