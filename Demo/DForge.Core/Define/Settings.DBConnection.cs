namespace DForge.Core.Define;

[DIModeForSettings("DBConnectionSettings", typeof(DBConnectionSettings))]
public class DBConnectionSettings : ISettings
{
    [Decrypt(EncryptionNDecryptEnum.AES, EnvironmentEnum.DEV, EnvironmentEnum.SIT, EnvironmentEnum.PROD)]
    public string Main { get; set; }
}