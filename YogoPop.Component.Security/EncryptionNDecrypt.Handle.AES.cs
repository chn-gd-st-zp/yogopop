namespace YogoPop.Component.Security;

[DIModeForSettings("AESSettings", typeof(AESSettings))]
public class AESSettings : IEncryptionNDecryptSettings
{
    public AESSettings() { }

    /// <summary>
    /// 32位密钥
    /// </summary>
    public string Secret { get; set; } = "1234567890123456";

    /// <summary>
    /// 32位向量/偏移量
    /// </summary>
    public string IV { get; set; } = "abcdefghijklmnop";

    /// <summary>
    /// 加密模式
    /// </summary>
    public CipherMode CipherMode { get; set; } = CipherMode.ECB;

    /// <summary>
    /// 填充模式
    /// </summary>
    public PaddingMode PaddingMode { get; set; } = PaddingMode.PKCS7;
}

public class AESHandle : BaseHandle<AESSettings>
{
    public AESHandle(string secretPrefix) : base(secretPrefix) { }

    public override string Encrypt(string text) => AES.Encrypt(SecretPrefix + Settings.Secret, Settings.IV, Settings.CipherMode, Settings.PaddingMode, text);

    public override string Decrypt(string text) => AES.Decrypt(SecretPrefix + Settings.Secret, Settings.IV, Settings.CipherMode, Settings.PaddingMode, text);
}