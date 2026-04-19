namespace YogoPop.Core.Extension;

public static class ByteExtension
{
    public static string ToHexStr(this byte[] dataArray)
    {
        string returnStr = string.Empty;
        if (dataArray != null)
        {
            for (int i = 0; i < dataArray.Length; i++)
            {
                returnStr += dataArray[i].ToString("X2");
            }
        }
        return returnStr;
    }

    public static string ToBase64(this byte[] dataArray)
    {
        var result = string.Empty;

        using (var stream = new MemoryStream(dataArray))
        {
            result = stream.ToBase64(false);
        }

        return result;
    }
}