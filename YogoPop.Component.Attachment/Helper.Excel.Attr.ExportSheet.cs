namespace YogoPop.Component.Attachment;

[AttributeUsage(AttributeTargets.Class)]
public class ExportSheetAttribute : Attribute
{
    public ExportSheetAttribute(string sheetName)
    {
        SheetName = sheetName;
    }

    public string SheetName { get; set; }
}