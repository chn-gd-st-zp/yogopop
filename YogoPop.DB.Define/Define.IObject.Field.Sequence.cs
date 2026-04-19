namespace YogoPop.DB.Define;

public interface IDBFSequence : IDBField
{
    public string CurSequence { get; set; }
}

public static class DBFSequenceExtension
{
    public static readonly int MaxLength = 10;
    public static readonly int MaxLength_Full = 255;

    public static void SetSequence(this IDBFSequence obj, string value) => obj.CurSequence = value.FormatSequence();

    public static string DefaultSequence(this string value) => "0".FormatSequence();

    public static string FormatSequence(this string field) => field.PadLeft(MaxLength, '0');

    public static string GetSequence(this IDBFSequence obj, int plusNum = 0)
    {
        int value = 0;
        if (!int.TryParse(obj.CurSequence, out value))
            value = 0;

        value += plusNum;

        return value.ToString().FormatSequence();
    }
}