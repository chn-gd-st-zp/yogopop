namespace YogoPop.Component.Attachment;

internal class ExportColumn
{
    public PropertyInfo PropertyInfo { get; set; }
    public ExportColAttribute ExportColAttribute { get; set; }
    public ExportAggregateAttribute ExportAggregateAttribute { get; set; }
}

public static class ExportExcelExtension
{
    public static DataTable ToDataTable<T>(this List<T> objList) where T : new()
    {
        objList = objList == null ? new List<T>() : objList;

        DataTable result = null;

        var sheetAttr = default(ExportSheetAttribute);
        var cols_export = new List<ExportColumn>();
        var data_aggregate = new T();

        #region 初始化

        var type = typeof(T);

        sheetAttr = type.GetCustomAttribute<ExportSheetAttribute>();
        if (sheetAttr == null)
            sheetAttr = new ExportSheetAttribute(string.Empty);

        var piArray = type.GetProperties();
        if (piArray == null)
            return result;

        foreach (var pi in piArray)
        {
            cols_export.Add(new ExportColumn
            {
                PropertyInfo = pi,
                ExportColAttribute = pi.GetCustomAttribute<ExportContentAttribute>(false),
                ExportAggregateAttribute = pi.GetCustomAttribute<ExportAggregateAttribute>(false),
            });
        }
        cols_export = cols_export.Where(o => o.ExportColAttribute != null).OrderBy(o => o.ExportColAttribute.Index).ToList();
        if (cols_export.IsEmpty()) return null;

        foreach (var col_export in cols_export)
        {
            if (col_export.ExportAggregateAttribute == null) continue;

            foreach (var obj in objList)
            {
                foreach (var pi2 in piArray)
                {
                    if (col_export.PropertyInfo.Name != pi2.Name)
                        continue;

                    var obj1 = col_export.PropertyInfo.GetValue(data_aggregate, null);
                    var obj2 = pi2.GetValue(obj, null);
                    var dataValue = col_export.ExportAggregateAttribute.Calculate(obj1, obj2);
                    col_export.PropertyInfo.SetValue(data_aggregate, dataValue);
                }
            }
        }

        #endregion

        #region 数据填充

        result = new DataTable(sheetAttr.SheetName);

        foreach (var col_export in cols_export)
            result.Columns.Add(col_export.ExportColAttribute.Title, typeof(object));

        foreach (var obj in objList)
        {
            var row_content = result.NewRow();

            foreach (var col_export in cols_export)
            {
                if (col_export.ExportColAttribute == null) continue;
                var colData = col_export.PropertyInfo.GetValue(obj, null);
                row_content[col_export.ExportColAttribute.Title] = col_export.ExportColAttribute.GetDataValue(colData);
            }

            result.Rows.Add(row_content);
        }

        var row_aggregate = result.NewRow();
        foreach (var col_export in cols_export)
        {
            if (col_export.ExportAggregateAttribute == null) continue;
            var colData = col_export.PropertyInfo.GetValue(data_aggregate, null);
            row_aggregate[col_export.ExportColAttribute.Title] = col_export.ExportAggregateAttribute.GetDataValue(colData);
        }
        result.Rows.Add(row_aggregate);

        #endregion

        return result;
    }

    public static List<T> ToObject<T>(this Stream stream) where T : class, new()
    {
        var result = new List<T>();
        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
        var sheetAttr = default(ImportSheetAttribute);
        var cols = new Dictionary<ImportColAttribute, PropertyInfo>();
        var datas = new List<Dictionary<ImportColAttribute, object>>();

        #region 初始化 列标识 从泛型对象载入

        Type type = typeof(T);

        sheetAttr = type.GetCustomAttribute<ImportSheetAttribute>();
        if (sheetAttr == null)
            return result;

        PropertyInfo[] piArray = type.GetProperties();
        if (piArray == null)
            return result;

        foreach (var pi in piArray)
        {
            var attr = pi.GetCustomAttribute<ImportColAttribute>(false);
            if (attr == null)
                continue;

            cols.Add(attr, pi);
        }

        #endregion

        #region 初始化 数据内容 从文件载入

        using (var package = new ExcelPackage(stream))
        {
            if (package == null) return result;
            if (package.Workbook == null) return result;
            if (package.Workbook.Worksheets == null) return result;

            var sheet = package.Workbook.Worksheets[sheetAttr.SheetName];
            if (sheet == null) return result;

            var dimension = sheet.Dimension;
            if (dimension == null) return result;

            int rowQty = dimension.Rows;
            int colQty = dimension.Columns;

            if (colQty != cols.Count()) return result;

            try
            {
                //sheet.Cells的 [行] 和 [列] 的索引都是从1开始的

                //跳过第一行的列名
                for (int curRowIndex = 2; curRowIndex <= rowQty; curRowIndex++)
                {
                    var data = new Dictionary<ImportColAttribute, object>();

                    for (int curColIndex = 1; curColIndex <= colQty; curColIndex++)
                    {
                        var colIndex = 0;
                        var colAttr = cols.Keys.ToList()[curColIndex - 1];
                        var colTitle = !colAttr.ColAlias.IsEmptyString() ? colAttr.ColAlias : cols[colAttr].Name;

                        #region 每一列的去查，获取当前列所对应的属性的确切下标

                        for (int index = 1; index <= colQty; index++)
                        {
                            var title = sheet.Cells[1, index].Value;
                            if (colTitle.IsEquals(title == null ? string.Empty : title.ToString()))
                            {
                                colIndex = index;
                                break;
                            }
                        }

                        #endregion

                        var colValue = sheet.Cells[curRowIndex, colIndex].Value;

                        data.Add(colAttr, colValue);
                    }

                    datas.Add(data);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("初始化数据内容发生错误", ex);
            }
        }

        #endregion

        foreach (var data in datas)
        {
            var resultItem = new T();

            foreach (var col in cols)
            {
                object value = Convert.ChangeType(data[col.Key], col.Key.ColType);
                col.Value.SetValue(resultItem, value);
            }

            result.Add(resultItem);
        }

        return result;
    }
}