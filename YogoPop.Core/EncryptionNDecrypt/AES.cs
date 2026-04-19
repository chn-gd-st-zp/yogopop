namespace YogoPop.Core.EncryptionNDecrypt;

public class AES
{
    /// <summary>
    /// AES 加密
    /// </summary>
    /// <param name="secret">密钥[32位，如: 12345678901234561234567890123456]</param>
    /// <param name="iv">向量/偏移量[16位，如: abcdefghijklmnop]</param>  
    /// <param name="cipherMode">加密模式</param>  
    /// <param name="paddingMode">填充模式</param>  
    /// <param name="text">需要加密的字符串</param>
    /// <returns>加密后的字符串</returns>
    public static string Encrypt(string secret, string iv, CipherMode cipherMode, PaddingMode paddingMode, string text)
    {
        secret = secret.Length < 32 ? secret.PadRight(32, ' ') : secret.Substring(0, 32);
        iv = iv.Length < 16 ? iv.PadRight(16, ' ') : iv.Substring(0, 16);

        var rijndael = new RijndaelManaged();
        rijndael.Key = Encoding.UTF8.GetBytes(secret);
        rijndael.IV = Encoding.UTF8.GetBytes(iv);
        rijndael.Mode = cipherMode;
        rijndael.Padding = paddingMode;

        ICryptoTransform cTransform = rijndael.CreateEncryptor();
        byte[] toEncryptArray = Encoding.UTF8.GetBytes(text);
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    /// <summary>
    /// AES 解密
    /// </summary>
    /// <param name="secret">密钥[32位，如: 12345678901234561234567890123456]</param>
    /// <param name="iv">向量/偏移量[16位，如: abcdefghijklmnop]</param>  
    /// <param name="cipherMode">加密模式</param>  
    /// <param name="paddingMode">填充模式</param>  
    /// <param name="text">需要解密的字符串</param>
    /// <returns>解密后的字符串</returns>
    public static string Decrypt(string secret, string iv, CipherMode cipherMode, PaddingMode paddingMode, string text)
    {
        secret = secret.Length < 32 ? secret.PadRight(32, ' ') : secret.Substring(0, 32);
        iv = iv.Length < 16 ? iv.PadRight(16, ' ') : iv.Substring(0, 16);

        var rijndael = new RijndaelManaged();
        rijndael.Key = Encoding.UTF8.GetBytes(secret);
        rijndael.IV = Encoding.UTF8.GetBytes(iv);
        rijndael.Mode = cipherMode;
        rijndael.Padding = paddingMode;

        ICryptoTransform cTransform = rijndael.CreateDecryptor();
        byte[] toEncryptArray = Convert.FromBase64String(text);
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        return Encoding.UTF8.GetString(resultArray);
    }
}