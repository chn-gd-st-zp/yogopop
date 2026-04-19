namespace YogoPop.Core.Extension;

public static class DTOExtension
{
    public static string GetPKPropertyName(this IDTOPrimaryKey obj) => "PrimaryKey";

    public static string GetPKPropertyName<T>(this IDTOPrimaryKey<T> obj) => nameof(IDTOPrimaryKey<T>.PrimaryKey);

    public static PropertyInfo GetPKPropertyInfo(this IDTOPrimaryKey obj) => obj.GetType().GetProperty(obj.GetPKPropertyName());

    public static object GetPKValue(this object obj)
    {
        var type = obj.GetType();

        if (!type.IsGenericOf(typeof(IDTOPrimaryKey<>)))
            return null;

        var obj2 = obj as IDTOPrimaryKey;
        if (obj2 == null) return null;

        //var field = type.GetProperty(obj2.GetPKPropertyName(), BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.IgnoreCase);
        var field = type.GetProperty(obj2.GetPKPropertyName());
        if (field == null)
            return null;

        return field.GetValue(obj);
    }

    //public static IDTOPageObj<TDestination> ToDTOPageObj<TDestination, TDTOSort>(this List<TDestination> dataList, int rowQty, IDTOPager<TDTOSort> pager) where TDTOSort : IDTOSort
    //{
    //    var result = InjectionContext.Resolve<IDTOPageObj<TDestination>>();

    //    result.PageSize = pager.PageSize;
    //    result.PageIndex = pager.PageIndex;
    //    result.TotalRowQty = rowQty;
    //    result.Data = dataList;

    //    result.TotalPageQty = (result.TotalRowQty / result.PageSize) + (result.TotalRowQty % result.PageSize == 0 ? 0 : 1);

    //    return result;
    //}

    //public static IDTOPageObj<TDestination> ToDTOPageObj<TDestination, TDTOSort>(this Tuple<int, List<TDestination>> pageData, IDTOPager<TDTOSort> pager) where TDTOSort : IDTOSort
    //{
    //    return pageData.Item2.ToDTOPageObj(pageData.Item1, pager);
    //}

    //public static IDTOPageObj<TDestination> ToDTOPageObj<TSource, TDestination, TDTOSort>(this Tuple<int, List<TSource>> pageData, IDTOPager<TDTOSort> pager, Func<TSource, TDestination> selector) where TDTOSort : IDTOSort
    //{
    //    var dataList = pageData.Item2.Select(selector).ToList();
    //    return dataList.ToDTOPageObj(pageData.Item1, pager);
    //}
}