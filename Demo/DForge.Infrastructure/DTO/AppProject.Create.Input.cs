namespace DForge.Infrastructure.DTO;

public class DTOAppProjectCreate : DTOInput
{
    /// <summary>
    /// 名称
    /// </summary>
    [Description("名称")]
    [Required]
    public string Name { get; set; }
}