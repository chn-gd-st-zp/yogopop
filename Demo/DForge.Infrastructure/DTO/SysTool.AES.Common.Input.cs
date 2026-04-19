namespace DForge.Infrastructure.DTO;

public abstract class DTOAES : DTOInput
{
    [Required]
    public EnvironmentEnum Env { get; set; } = EnvironmentEnum.None;

    [Required]
    public string Secret { get; set; } = "0123456789abc";

    [Required]
    public string IV { get; set; } = "abcdefghijklmnop";

    [Required]
    public CipherMode CipherMode { get; set; } = CipherMode.ECB;

    [Required]
    public PaddingMode PaddingMode { get; set; } = PaddingMode.PKCS7;

    [Required]
    public string Content { get; set; } = string.Empty;
}