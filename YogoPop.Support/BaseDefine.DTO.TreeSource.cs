namespace YogoPop.Support;

/// <summary>
/// 树形
/// </summary>
public abstract class DTOTreeSource<T> : IDTO, IDBTree<T>
{
    /// <summary>
    /// 文本
    /// </summary>
    public string Text { get { return Name; } }

    /// <summary>
    /// 值
    /// </summary>
    public string Value { get { return CurNode.ToString(); } }

    /// <summary>
    /// 名称
    /// </summary>
    public abstract string Name { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public abstract T CurNode { get; set; }

    /// <summary>
    /// 父节点编码
    /// </summary>
    public abstract T ParentNode { get; set; }

    /// <summary>
    /// 完整编码
    /// </summary>
    public abstract string FullNode { get; set; }
}