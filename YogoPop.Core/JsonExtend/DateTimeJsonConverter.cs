namespace YogoPop.Core.JsonExtend;

public class DateTimeJsonConverter : IsoDateTimeConverter
{
    public DateTimeJsonConverter(string format) { DateTimeFormat = format; }
}