namespace DForge.Infrastructure.DTO;

public class DTOAppProjectUpdate : DTOPrimaryKeyRequired<string>
{
    /// <summary>
    /// 名称
    /// </summary>
    [Description("名称")]
    [Required]
    public string Name { get; set; }
}