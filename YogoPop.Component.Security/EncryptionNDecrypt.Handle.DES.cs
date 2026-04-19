namespace YogoPop.Component.Security;

[DIModeForSettings("DESSettings", typeof(DESSettings))]
public class DESSettings : IEncryptionNDecryptSettings
{
    public DESSettings() { }

    /// <summary>
    /// 密钥
    /// </summary>
    public string Secret { get; set; } = "1234567890123456";

    /// <summary>
    /// 向量/偏移量
    /// </summary>
    public string IV { get; set; } = "1234567890123456";
}

public class DESHandle : BaseHandle<DESSettings>
{
    public DESHandle(string secretPrefix) : base(secretPrefix) { }

    public override string Encrypt(string text) => Core.EncryptionNDecrypt.DES.Encrypt(SecretPrefix + Settings.Secret, Settings.IV, text);

    public override string Decrypt(string text) => Core.EncryptionNDecrypt.DES.Decrypt(SecretPrefix + Settings.Secret, Settings.IV, text);
}