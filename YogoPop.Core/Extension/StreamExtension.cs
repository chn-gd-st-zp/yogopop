namespace YogoPop.Core.Extension;

public static class StreamExtension
{
    public static string ToBase64(this Stream stream, bool closeStream = true)
    {
        string result = null;

        if (stream == null)
            return result;

        byte[] byteArray = new byte[stream.Length];
        stream.Read(byteArray, 0, byteArray.Length);

        result = Convert.ToBase64String(byteArray);

        if (closeStream)
            stream.Close();

        return result;
    }

    public static void ToFile(this Stream stream, string fileName)
    {
        byte[] bytes = new byte[stream.Length];

        stream.Read(bytes, 0, bytes.Length);

        stream.Seek(0, SeekOrigin.Begin);

        FileStream fs = new FileStream(fileName, FileMode.Create);

        BinaryWriter bw = new BinaryWriter(fs);

        bw.Write(bytes);

        bw.Close();

        fs.Close();
    }
}