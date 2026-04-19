namespace YogoPop.Core.Extension;

public static class CollectionsExtension
{
    public static bool IsNull([AllowNull] this IEnumerable datas) => datas == null;

    public static bool IsNotNull([AllowNull] this IEnumerable datas) => !datas.IsNull();

    public static bool IsEmpty([AllowNull] this IEnumerable datas)
    {
        if (datas == null)
            return true;

        var list = datas.GetEnumerator();

        return list.MoveNext() ? false : true;
    }

    public static bool IsNotEmpty([AllowNull] this IEnumerable datas) => !datas.IsEmpty();

    public static bool IsEmpty<T>([AllowNull] this IEnumerable<T> datas)
    {
        if (datas == null)
            return true;

        var list = datas.ToList();

        if (list == null)
            return true;

        return list.Any() ? false : true;
    }

    public static bool IsNotEmpty<T>([AllowNull] this IEnumerable<T> datas) => !datas.IsEmpty();

    public static string ToString([AllowNull] this IEnumerable datas, char code) => datas.IsEmpty() ? string.Empty : string.Join(code, datas);

    public static string ToString<T>([AllowNull] this IEnumerable<T> datas, char code) => datas.IsEmpty() ? string.Empty : string.Join(code, datas);

    public static string ToString([AllowNull] this IEnumerable datas, string code) => datas.IsEmpty() ? string.Empty : string.Join(code, datas);

    public static string ToString<T>([AllowNull] this IEnumerable<T> datas, string code) => datas.IsEmpty() ? string.Empty : string.Join(code, datas);

    /// <summary>
    /// 排序
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="comparison"></param>
    /// <returns></returns>
    public static T[] Sort<T>(this T[] array, Comparison<T> comparison = default)
    {
        if (comparison == default)
            Array.Sort(array);
        else
            Array.Sort(array, comparison);

        return array;
    }

    /// <summary>
    /// 根据日转换
    /// </summary>
    /// <param name="timeRange">长度为2，格式为yyyy-MM-dd</param>
    /// <returns></returns>
    public static DTORange<DateTime?> ParseByDate(this List<string>? timeRange)
    {
        var result = new DTORange<DateTime?>();

        if (timeRange.IsEmpty())
            return result;

        if (timeRange.Count >= 1 && !timeRange[0].IsEmptyString())
            result.Begin = timeRange[0].ToBeginTime();

        if (timeRange.Count >= 2 && !timeRange[1].IsEmptyString())
            result.End = timeRange[1].ToEndTime();

        return result;
    }

    /// <summary>
    /// 根据月转换
    /// </summary>
    /// <param name="timeRange">长度为2，格式为yyyy-MM</param>
    /// <returns></returns>
    public static DTORange<DateTime?> ParseByMonth(this List<string>? timeRange)
    {
        var result = new DTORange<DateTime?>();

        if (timeRange.IsEmpty())
            return result;

        if (timeRange.Count >= 1 && timeRange[0].IsNotEmptyString())
            result.Begin = (timeRange[0] + "-01").ToBeginTime();

        if (timeRange.Count >= 2 && timeRange[1].IsNotEmptyString())
            result.End = (timeRange[1] + "-01").ToEndTime().LastDayOfMonth();

        return result;
    }

    /// <summary>
    /// 根据月转换
    /// </summary>
    /// <param name="timeRange">长度为2，格式为yyyy-MM</param>
    /// <returns></returns>
    public static DTORange<DateTime?> ParseByMonth(this DTORange<string>? timeRange)
    {
        var result = new DTORange<DateTime?>();

        if (timeRange == null)
            return result;

        if (timeRange.Begin.IsNotEmpty())
            result.Begin = (timeRange.Begin + "-01").ToBeginTime();

        if (timeRange.End.IsNotEmpty())
            result.End = (timeRange.End + "-01").ToEndTime().LastDayOfMonth();

        return result;
    }

    /// <summary>
    /// 根据日转换
    /// </summary>
    /// <param name="timeRange">长度为2，格式为yyyy-MM-dd</param>
    /// <returns></returns>
    public static DTORange<DateTime?> ParseByDate(this DTORange<string>? timeRange)
    {
        var result = new DTORange<DateTime?>();

        if (timeRange == null)
            return result;

        if (timeRange.Begin.IsNotEmpty())
            result.Begin = timeRange.Begin.ToBeginTime();

        if (timeRange.End.IsNotEmpty())
            result.End = timeRange.End.ToEndTime();

        return result;
    }
}