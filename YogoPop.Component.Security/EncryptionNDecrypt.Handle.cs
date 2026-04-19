namespace YogoPop.Component.Security;

public abstract class BaseHandle<TSettings> : IEncryptionNDecryptHandle<TSettings> where TSettings : class, IEncryptionNDecryptSettings, new()
{
    protected string SecretPrefix { get; private set; }

    public TSettings Settings { get; set; }

    public BaseHandle(string secretPrefix) : base()
    {
        SecretPrefix = secretPrefix.IsEmptyString() ? string.Empty : secretPrefix;
        Settings = InjectionContext.Resolve<TSettings>();
    }

    public abstract string Decrypt(string text);

    public abstract string Encrypt(string text);
}