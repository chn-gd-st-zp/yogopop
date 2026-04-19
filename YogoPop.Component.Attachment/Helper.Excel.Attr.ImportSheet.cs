namespace YogoPop.Component.Attachment;

[AttributeUsage(AttributeTargets.Class)]
public class ImportSheetAttribute : Attribute
{
    public ImportSheetAttribute(string sheetName)
    {
        SheetName = sheetName;
    }

    public string SheetName { get; set; }
}