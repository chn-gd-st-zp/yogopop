namespace DForge.DynScheduling;

[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = true)]
public class DynSchPeriodAttribute : Attribute
{
    public DateCycleEnum DateCycle { get; private set; }

    public string Cron { get; private set; }

    /// <summary>
    /// 要减多少天
    /// </summary>
    public int Diff { get; private set; }

    public DynSchPeriodAttribute(DateCycleEnum dateCycle, string corn)
    {
        var diff = 0;

        switch (dateCycle)
        {
            case DateCycleEnum.Day:
                diff = 1;
                break;
            case DateCycleEnum.Week:
                diff = 7;
                break;
            case DateCycleEnum.Month:
                var month = DateTimeExtension.Now.FirstDayOfMonth().AddDays(-1).ToEndTime() - DateTimeExtension.Now.FirstDayOfMonth().AddMonths(-1).ToBeginTime();
                diff = Convert.ToInt32(month.TotalDays);
                break;
            case DateCycleEnum.Season:
                var season = DateTimeExtension.Now.FirstDayOfSeason().AddDays(-1).ToEndTime() - DateTimeExtension.Now.FirstDayOfSeason().AddMonths(-3).ToBeginTime();
                diff = Convert.ToInt32(season.TotalDays);
                break;
            case DateCycleEnum.Year:
                var year = DateTimeExtension.Now.FirstDayOfYear().AddDays(-1).ToEndTime() - DateTimeExtension.Now.FirstDayOfYear().AddYears(-1).ToBeginTime();
                diff = Convert.ToInt32(year.TotalDays);
                break;
            default:
                diff = 0;
                break;
        }

        DateCycle = dateCycle;
        Cron = corn;
        Diff = -diff;
    }
}