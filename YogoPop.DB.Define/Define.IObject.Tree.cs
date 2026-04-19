namespace YogoPop.DB.Define;

public interface IDBTree<T> : IDTOTreeItem
{
    public string Name { get; set; }

    public T CurNode { get; set; }

    public T ParentNode { get; set; }

    public string FullNode { get; set; }
}

public static class DBFTreeExtension
{
    public static void SetFullNode<TEntity, TKey>(this TEntity obj, string full = default) where TEntity : class, IDBTree<TKey>
    {
        if (obj == null)
            return;

        string cur_str = obj.CurNode.ToString();
        string full_str = full == default ? string.Empty : full.ToString();

        if (full_str.IsEmptyString() && cur_str.IsEmptyString())
        {
            obj.FullNode = string.Empty;
            return;
        }

        if (full_str.IsEmptyString())
        {
            obj.FullNode = cur_str;
            return;
        }

        full_str = full_str.StartsWith(",") ? full_str : "," + full_str;
        full_str = full_str.EndsWith(",") ? full_str : full_str + ",";

        obj.FullNode = full_str + cur_str;
    }

    public static void SetFullNode<TEntity, TKey>(this TEntity obj, string[] fulls) where TEntity : class, IDBTree<TKey>
    {
        if (obj == null)
            return;

        string cur_str = obj.CurNode.ToString();

        if ((fulls == null || fulls.Length == 0) && cur_str.IsEmptyString())
        {
            obj.FullNode = string.Empty;
            return;
        }

        if (fulls == null || fulls.Length == 0)
        {
            obj.FullNode = cur_str;
            return;
        }

        string fulls_str = fulls.ToString(',');
        fulls_str = fulls_str.StartsWith(",") ? fulls_str : "," + fulls_str;
        fulls_str = fulls_str.EndsWith(",") ? fulls_str : fulls_str + ",";

        obj.FullNode = fulls_str + cur_str;
    }

    public static DTOTree<TEntity> ToTree<TEntity, TKey>(this List<TEntity> sourceDataList, TKey pathTag = default) where TEntity : class, IDBTree<TKey>
    {
        DTOTree<TEntity> tree = new DTOTree<TEntity>();

        var query = sourceDataList.AsQueryable();

        if (pathTag == null)
            query = query.Where(o => o.ParentNode == null);
        else
            query = query.Where(o => o.ParentNode != null && o.ParentNode.ToString() == pathTag.ToString());

        List<TEntity> dataList = query.ToList();
        foreach (TEntity data in dataList)
        {
            DTOTreeNode<TEntity> treeNode = new DTOTreeNode<TEntity>();
            treeNode.Info = data;
            treeNode.Childs = sourceDataList.ToTree(data.CurNode);

            tree.Add(treeNode);
        }

        return tree;
    }
}
