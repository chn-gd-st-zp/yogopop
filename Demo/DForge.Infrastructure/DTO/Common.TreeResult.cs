namespace DForge.Infrastructure.DTO;

/// <summary>
/// 树形
/// </summary>
public class DTOTreeResult : DTOTreeSource<string>
{
    /// <summary>
    /// 名称
    /// </summary>
    [Description("名称")]
    [JsonProperty("Name"), PropertyRename("Name")]
    public override string Name { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [Description("编码")]
    [JsonProperty("Code"), PropertyRename("Code")]
    public override string CurNode { get; set; }

    /// <summary>
    /// 父节点编码
    /// </summary>
    [Description("父节点编码")]
    [JsonProperty("ParentCode"), PropertyRename("ParentCode")]
    public override string ParentNode { get; set; }

    /// <summary>
    /// 完整编码
    /// </summary>
    [Description("完整编码")]
    [JsonProperty("FullCode"), PropertyRename("FullCode")]
    public override string FullNode { get; set; }
}