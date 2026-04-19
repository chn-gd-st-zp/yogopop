namespace YogoPop.Core.DTO;

public class DTOTree<T> : List<DTOTreeNode<T>>, IDTO
    where T : class, IDTOTreeItem
{
    //
}

public class DTOTreeNode<T> : IDTO
    where T : class, IDTOTreeItem
{
    public string Text { get { return Info.Text; } }

    public string Value { get { return Info.Value; } }

    public T Info { get; set; }

    public List<DTOTreeNode<T>> Childs { get; set; }

    public DTOTreeNode()
    {
        Info = default;
        Childs = new DTOTree<T>();
    }
}