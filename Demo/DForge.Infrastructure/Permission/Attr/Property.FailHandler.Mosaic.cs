namespace DForge.Infrastructure.Permission.Attr;

public class MosaicHandler
{
    public const string code = "******";

    public static object Parse(object original)
    {
        if (original is null)
            return original;

        var data = original.ToString();
        if (data.IsEmptyString())
            return data;

        if (data.Contains(code))
            return data;

        if (AccountHelper.IsEmail(data))
        {
            return data.Substring(0, 3) + code + data.Substring(data.LastIndexOf("@"));
        }
        else
        {
            return data.Substring(0, 3) + code + data.Substring(data.Length - 4);
        }
    }
}