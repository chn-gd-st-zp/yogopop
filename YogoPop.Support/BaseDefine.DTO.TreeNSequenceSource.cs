namespace YogoPop.Support;

/// <summary>
/// 树形带排序
/// </summary>
public abstract class DTOTreeNSequenceSource<T> : DTOTreeSource<T>, IDBTreeNSequence<T>
{
    /// <summary>
    /// 排序号
    /// </summary>
    public abstract string CurSequence { get; set; }

    /// <summary>
    /// 完整排序号
    /// </summary>
    public abstract string FullSequence { get; set; }
}