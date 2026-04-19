namespace DForge.DynScheduling;

public static class DynSchExtension
{
    public static DTORange<DateTime> GetQueryRange(this IDynSchRecordEntity entity)
    {
        var result = new DTORange<DateTime>();

        switch (entity.Frequency)
        {
            case DateCycleEnum.Day:
                result.Begin = entity.DataDate.ToBeginTime();
                result.End = entity.DataDate.ToEndTime();
                break;
            case DateCycleEnum.Week:
                result.Begin = entity.DataDate.FirstDayOfWeek();
                result.End = entity.DataDate.FirstDayOfWeek().AddDays(7).ToEndTime();
                break;
            case DateCycleEnum.Month:
                result.Begin = entity.DataDate.FirstDayOfMonth().ToBeginTime();
                result.End = entity.DataDate.LastDayOfMonth().ToEndTime();
                break;
            case DateCycleEnum.Season:
                result.Begin = entity.DataDate.FirstDayOfSeason().ToBeginTime();
                result.End = entity.DataDate.LastDayOfSeason().ToEndTime();
                break;
            case DateCycleEnum.Year:
                result.Begin = entity.DataDate.FirstDayOfYear().ToBeginTime();
                result.End = entity.DataDate.LastDayOfYear().ToEndTime();
                break;
            case DateCycleEnum.Hour:
                result.Begin = DateTime.Parse(entity.DataDate.ToString("yyyy-MM-dd HH:00:00"));
                result.End = DateTime.Parse(entity.DataDate.ToString("yyyy-MM-dd HH:59:59"));
                break;
            case DateCycleEnum.Minute:
                result.Begin = DateTime.Parse(entity.DataDate.ToString("yyyy-MM-dd HH:mm:00"));
                result.End = DateTime.Parse(entity.DataDate.ToString("yyyy-MM-dd HH:mm:59"));
                break;
            case DateCycleEnum.Second:
                result.Begin = entity.DataDate;
                result.End = entity.DataDate;
                break;
            default:
                return null;
        }

        return result;
    }
}