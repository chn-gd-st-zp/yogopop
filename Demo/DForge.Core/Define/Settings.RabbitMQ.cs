namespace DForge.Core.Define;

public class RenewRabbitMQSettings : RabbitMQSettings
{
    //[Decrypt(EncryptionNDecryptEnum.AES, EnvironmentEnum.DEV, EnvironmentEnum.SIT, EnvironmentEnum.PROD)]
    public override string HostName { get; set; }

    //[Decrypt(EncryptionNDecryptEnum.AES, EnvironmentEnum.DEV, EnvironmentEnum.SIT, EnvironmentEnum.PROD)]
    public override string Port { get; set; }

    [Decrypt(EncryptionNDecryptEnum.AES, EnvironmentEnum.DEV, EnvironmentEnum.SIT, EnvironmentEnum.PROD)]
    public override string UserName { get; set; }

    [Decrypt(EncryptionNDecryptEnum.AES, EnvironmentEnum.DEV, EnvironmentEnum.SIT, EnvironmentEnum.PROD)]
    public override string Password { get; set; }
}