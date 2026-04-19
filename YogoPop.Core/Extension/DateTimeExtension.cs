namespace YogoPop.Core.Extension;

public static class DateTimeExtension
{
    public static DateTime BaseTime => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToUtc();

    public static DateTimeOffset NowOffset => DateTimeOffset.UtcNow.AddHours(AppInitHelper.TimeZone);

    public static DateTime Now => DateTime.UtcNow.AddHours(AppInitHelper.TimeZone);

    private static int ShortTimeStampLength => Math.Round((Now - BaseTime).TotalSeconds).ToString().Length;

    private static int LongTimeStampLength => Math.Round((Now - BaseTime).TotalMilliseconds).ToString().Length;

    private static DateTime ToUtc(this DateTime dateTime) => TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Utc);

    public static long ToTimeStamp(this DateTime dateTime, int digit = 10)
    {
        var result = 0d;

        var dt = dateTime.ToUtc().AddHours(AppInitHelper.TimeZone) - BaseTime;

        if (digit == ShortTimeStampLength)
            result = dt.TotalSeconds;

        if (digit == LongTimeStampLength)
            result = dt.TotalMilliseconds;

        return (long)result;
    }

    public static DateTime ToDateTimeFromTimeStamp(this long timeStamp)
    {
        var result = BaseTime.ToUtc().AddHours(AppInitHelper.TimeZone);

        if (timeStamp.ToString().Length == ShortTimeStampLength)
            result = result.AddSeconds(timeStamp);

        if (timeStamp.ToString().Length == LongTimeStampLength)
            result = result.AddMilliseconds(timeStamp);

        return result;
    }

    public static DateTime Monday => Now.FirstDayOfWeek();

    public static DateTime Tuesday => Monday.AddDays(1);

    public static DateTime Wednesday => Monday.AddDays(2);

    public static DateTime Thursday => Monday.AddDays(3);

    public static DateTime Friday => Monday.AddDays(4);

    public static DateTime Saturday => Monday.AddDays(5);

    public static DateTime Sunday => Monday.AddDays(6);

    public static bool IsPast(this string date) => date.ToDateTime().IsPast();

    public static bool IsPast(this DateTime dateTime) => Now > dateTime;

    public static DateTime ToDateTime(this string dateTime) => DateTime.Parse(dateTime);

    public static string ToDateString(this DateTime dateTime, string format = "yyyy-MM-dd") => dateTime.ToString(format);

    public static string ToDateTimeString(this DateTime dateTime, string format = "yyyy-MM-dd HH:mm:ss.fff") => dateTime.ToString(format);

    public static DateTime ToBeginTime(this string date) => DateTime.Parse(date + " 00:00:00.000");

    public static DateTime ToBeginTime(this DateTime dateTime) => dateTime.ToDateString().ToBeginTime();

    public static DateTime ToEndTime(this string date) => DateTime.Parse(date + " 23:59:59.999");

    public static DateTime ToEndTime(this DateTime dateTime) => dateTime.ToDateString().ToEndTime();

    public static DateTime FirstDayOfWeek(this DateTime dateTime)
    {
        var i = dateTime.DayOfWeek - DayOfWeek.Monday == -1 ? 6 : -1;
        var ts = new TimeSpan(i, 0, 0, 0);

        return NowOffset.Subtract(ts).Date;
    }

    public static DateTime FirstDayOfMonth(this DateTime dateTime) => DateTime.Parse(dateTime.ToString("yyyy-MM") + "-01 00:00:00");

    public static DateTime LastDayOfMonth(this DateTime dateTime) => DateTime.Parse(DateTime.Parse(dateTime.AddMonths(1).ToString("yyyy-MM") + "-01 00:00:00").AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59");

    public static DateTime FirstDayOfSeason(this DateTime dateTime)
    {
        int quarter = (dateTime.Month - 1) / 3 + 1;
        int startMonth = (quarter - 1) * 3 + 1;

        return DateTime.Parse(Now.ToString("yyyy-") + startMonth.ToString().PadLeft(2, '0') + Now.ToString("-01 00:00:00")).FirstDayOfMonth();
    }

    public static DateTime LastDayOfSeason(this DateTime dateTime) => dateTime.AddMonths(2).LastDayOfMonth();

    public static DateTime FirstDayOfYear(this DateTime dateTime) => DateTime.Parse(Now.ToString("yyyy-") + Now.ToString("-01-01 00:00:00")).FirstDayOfMonth();

    public static DateTime LastDayOfYear(this DateTime dateTime) => dateTime.FirstDayOfYear().AddYears(1).AddDays(-1).ToEndTime();

    public static DateTime FirstDayOfMonth(this string month) => DateTime.Parse(Now.ToString("yyyy-") + month.PadLeft(2, '0') + Now.ToString("-01 00:00:00")).FirstDayOfMonth();

    public static DateTime LastDayOfMonth(this string month) => DateTime.Parse(Now.ToString("yyyy-") + month.PadLeft(2, '0') + Now.ToString("-01 00:00:00")).LastDayOfMonth();

    //public static DateTime FirstDayOfNextMonth(this DateTime dateTime) => DateTime.Parse(dateTime.AddMonths(1).ToString("yyyy-MM") + "-01 00:00:00");
}