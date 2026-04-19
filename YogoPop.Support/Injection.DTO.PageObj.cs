namespace YogoPop.Support;

[DIModeForService(DIModeEnum.Exclusive, typeof(IDTOPageObj<>))]
public class DTOPageObj<TData> : DTOOutput, IDTOPageObj<TData>, ITransient
{
    public int PageSize { get; set; }

    public int PageIndex { get; set; }

    public int TotalPageQty { get; set; }

    public int TotalRowQty { get; set; }

    public List<TData> Data { get; set; }
}

public class DTOPageObj<TData, TSumBar> : DTOPageObj<TData>
{
    public TSumBar SumBar { get; set; }
}

public static class DTOPageObjExtension
{
    public static DTOPageObj<TEntity> ToDTOPageObj<TEntity, TSort>(this IEnumerable<TEntity> dataList, int rowQty, DTOPager<TSort> pageParam) where TSort : IDTOSort, new()
    {
        var result = new DTOPageObj<TEntity>();

        result.PageSize = pageParam.PageSize;
        result.PageIndex = pageParam.PageIndex;
        result.TotalRowQty = rowQty;
        result.Data = dataList.ToList();

        result.TotalPageQty = (result.TotalRowQty / result.PageSize) + (result.TotalRowQty % result.PageSize == 0 ? 0 : 1);

        return result;
    }

    public static DTOPageObj<TEntity> ToDTOPageObj<TEntity, TSort>(this Tuple<List<TEntity>, int> pageData, DTOPager<TSort> pageParam) where TSort : IDTOSort, new()
    {
        var result = pageData.Item1.ToDTOPageObj(pageData.Item2, pageParam);
        return result;
    }

    public static DTOPageObj<TTarget> ToDTOPageObj<TSource, TTarget, TSort>(this Tuple<List<TSource>, int> pageData, DTOPager<TSort> pageParam) where TSort : IDTOSort, new()
    {
        var mapper = InjectionContext.Resolve<IMapper>();
        var dataList = pageData.Item1.Select(o => mapper.Map<TTarget>(o));

        var result = dataList.ToDTOPageObj(pageData.Item2, pageParam);
        return result;
    }

    public static DTOPageObj<TTarget> ToDTOPageObj<TSource, TTarget, TSort>(this Tuple<List<TSource>, int> pageData, DTOPager<TSort> pageParam, Func<TSource, TTarget> selector) where TSort : IDTOSort, new()
    {
        var dataList = pageData.Item1.Select(selector);

        var result = dataList.ToDTOPageObj(pageData.Item2, pageParam);
        return result;
    }

    public static DTOPageObj<TEntity> ToDTOPageObj<TEntity, TSort>(this Tuple<IEnumerable<TEntity>, int> pageData, DTOPager<TSort> pageParam) where TSort : IDTOSort, new()
    {
        var result = pageData.Item1.ToDTOPageObj(pageData.Item2, pageParam);
        return result;
    }

    public static DTOPageObj<TTarget> ToDTOPageObj<TSource, TTarget, TSort>(this Tuple<IEnumerable<TSource>, int> pageData, DTOPager<TSort> pageParam) where TSort : IDTOSort, new()
    {
        var mapper = InjectionContext.Resolve<IMapper>();
        var dataList = pageData.Item1.Select(o => mapper.Map<TTarget>(o));

        var result = dataList.ToDTOPageObj(pageData.Item2, pageParam);
        return result;
    }

    public static DTOPageObj<TTarget> ToDTOPageObj<TSource, TTarget, TSort>(this Tuple<IEnumerable<TSource>, int> pageData, DTOPager<TSort> pageParam, Func<TSource, TTarget> selector) where TSort : IDTOSort, new()
    {
        var dataList = pageData.Item1.Select(selector);

        var result = dataList.ToDTOPageObj(pageData.Item2, pageParam);
        return result;
    }
}