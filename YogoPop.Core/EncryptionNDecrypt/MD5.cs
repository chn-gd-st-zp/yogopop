namespace YogoPop.Core.EncryptionNDecrypt;

public class MD5
{
    public static string Encrypt(string text, int digit = 32)
    {
        string md5code = string.Empty;
        using (var md5 = System.Security.Cryptography.MD5.Create())
        {
            var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
            var sb = new StringBuilder();
            foreach (var b in hashBytes)
                sb.Append(b.ToString("x2"));
            md5code = sb.ToString();
        }

        if (digit == 16) //16位MD5加密（取32位加密的9~25字符）
        {
            return md5code.Substring(8, 16);
        }

        if (digit == 32) //32位加密
        {
            return md5code;
        }

        return md5code;
    }
}