namespace YogoPop.DB.Define;

public interface IDBTreeNSequence<T> : IDBTree<T>, IDBFSequence
{
    string FullSequence { get; set; }
}

public static class DBTreeNSequenceExntesion
{
    public static void SetFullSequence<TEntity, TKey>(this TEntity obj, string full = default) where TEntity : class, IDBTreeNSequence<TKey>
    {
        if (obj == null)
            return;

        string cur_str = obj.CurSequence.ToString();
        string full_str = full == null ? string.Empty : full.ToString();

        if (full_str.IsEmptyString() && cur_str.IsEmptyString())
        {
            obj.FullSequence = string.Empty;
            return;
        }

        if (full_str.IsEmptyString())
        {
            obj.FullSequence = cur_str;
            return;
        }

        full_str = full_str.StartsWith(",") ? full_str : "," + full_str;
        full_str = full_str.EndsWith(",") ? full_str : full_str + ",";

        obj.FullSequence = full_str + cur_str;
    }

    public static void SetFullSequence<TEntity, TKey>(this TEntity obj, string[] fulls) where TEntity : class, IDBTreeNSequence<TKey>
    {
        if (obj == null)
            return;

        string cur_str = obj.CurSequence;

        if ((fulls == null || fulls.Length == 0) && cur_str.IsEmptyString())
        {
            obj.FullSequence = string.Empty;
            return;
        }

        if (fulls == null || fulls.Length == 0)
        {
            obj.FullSequence = cur_str;
            return;
        }

        string fulls_str = fulls.ToString(',');
        fulls_str = fulls_str.StartsWith(",") ? fulls_str : "," + fulls_str;
        fulls_str = fulls_str.EndsWith(",") ? fulls_str : fulls_str + ",";

        obj.FullSequence = fulls_str + cur_str;
    }

    public static Tuple<string, string> GetFullCode<TEntity>(this TEntity obj, List<TEntity> total) where TEntity : class, IDBTreeNSequence<string>
    {
        var fullCode = string.Empty;
        var fullSequence = string.Empty;

        var parent = total.Where(o => o.CurNode == obj.ParentNode).SingleOrDefault();
        if (parent != null)
        {
            var datas = GetFullCode(parent, total);

            fullCode = datas.Item1;
            if (!fullCode.IsEmptyString())
                fullCode += ",";

            fullSequence = datas.Item2;
            if (!fullSequence.IsEmptyString())
                fullSequence += ",";
        }

        fullCode += obj.CurNode;
        fullSequence += obj.CurSequence.FormatSequence();

        return new Tuple<string, string>(fullCode, fullSequence);
    }
}