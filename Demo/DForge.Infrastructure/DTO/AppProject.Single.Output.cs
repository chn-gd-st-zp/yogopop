namespace DForge.Infrastructure.DTO;

public class DTOAppProjectSingleResult : DTOPrimaryKey<string>
{
    /// <summary>
    /// 名称
    /// </summary>
    [Description("名称")]
    public string Name { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [Description("状态")]
    public StatusEnum Status { get; set; }
}