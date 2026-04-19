namespace YogoPop.Core.Extension;

public static class DataTableExtension
{
    public static T ToEntity<T>(this DataTable dt)
    {
        T s = InstanceCreator.Create<T>();
        if (dt == null || dt.Rows.IsEmpty())
        {
            return default(T);
        }
        var plist = new List<PropertyInfo>(typeof(T).GetProperties());
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            PropertyInfo info = plist.Find(p => p.Name == dt.Columns[i].ColumnName);
            if (info != null)
            {
                try
                {
                    if (!Convert.IsDBNull(dt.Rows[0][i]))
                    {
                        object v = null;
                        if (info.PropertyType.ToString().Contains("System.Nullable"))
                        {
                            v = Convert.ChangeType(dt.Rows[0][i], Nullable.GetUnderlyingType(info.PropertyType));
                        }
                        else
                        {
                            v = Convert.ChangeType(dt.Rows[0][i], info.PropertyType);
                        }
                        info.SetValue(s, v, null);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("字段[" + info.Name + "]转换出错," + ex.Message);
                }
            }
        }
        return s;
    }

    public static List<T> ToDataList<T>(this DataTable dt)
    {
        var list = new List<T>();
        var plist = new List<PropertyInfo>(typeof(T).GetProperties());

        if (dt == null || dt.Rows.IsEmpty())
        {
            return null;
        }

        foreach (DataRow item in dt.Rows)
        {
            T s = InstanceCreator.Create<T>();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                PropertyInfo info = plist.Find(p => p.Name == dt.Columns[i].ColumnName);
                if (info != null)
                {
                    try
                    {
                        if (!Convert.IsDBNull(item[i]))
                        {
                            object v = null;
                            if (info.PropertyType.ToString().Contains("System.Nullable"))
                            {
                                v = Convert.ChangeType(item[i], Nullable.GetUnderlyingType(info.PropertyType));
                            }
                            else
                            {
                                v = Convert.ChangeType(item[i], info.PropertyType);
                            }
                            info.SetValue(s, v, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("字段[" + info.Name + "]转换出错," + ex.Message);
                    }
                }
            }
            list.Add(s);
        }

        return list;
    }

    public static DataTable ToDataTable<T>(this IEnumerable<T> collection)
    {
        var dt = new DataTable();

        var props = typeof(T).GetProperties();
        props.ToList().ForEach(o =>
        {
            DataColumn column = null;

            if (o.PropertyType.IsGenericType && o.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                column = new DataColumn(o.Name, o.PropertyType.GetGenericArguments()[0]);
            else
                column = new DataColumn(o.Name, o.PropertyType);

            dt.Columns.Add(column);
        });

        if (collection.IsNotEmpty())
        {
            for (int i = 0; i < collection.Count(); i++)
            {
                ArrayList tempList = new ArrayList();
                foreach (PropertyInfo pi in props)
                {
                    object obj = pi.GetValue(collection.ElementAt(i), null);
                    obj = obj == null ? DBNull.Value : obj;
                    tempList.Add(obj);
                }
                object[] array = tempList.ToArray();
                dt.LoadDataRow(array, true);
            }
        }

        return dt;
    }
}