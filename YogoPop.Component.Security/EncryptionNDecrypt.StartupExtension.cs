namespace YogoPop.Component.Security;

public static partial class EncryptionNDecryptExtension
{
    public static ContainerBuilder RegisAES(this ContainerBuilder containerBuilder, string secretPrefix)
    {
        containerBuilder.RegisterType<AESHandle>().Keyed<IEncryptionNDecrypt>(EncryptionNDecryptEnum.AES).WithParameter("secretPrefix", secretPrefix).InstancePerDependency();

        return containerBuilder;
    }

    public static ContainerBuilder RegisDES(this ContainerBuilder containerBuilder, string secretPrefix)
    {
        containerBuilder.RegisterType<DESHandle>().Keyed<IEncryptionNDecrypt>(EncryptionNDecryptEnum.DES).WithParameter("secretPrefix", secretPrefix).InstancePerDependency();

        return containerBuilder;
    }
}