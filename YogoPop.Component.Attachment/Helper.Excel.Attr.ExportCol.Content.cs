namespace YogoPop.Component.Attachment;

public class ExportContentAttribute : ExportColAttribute
{
    public ExportContentAttribute(int index, string title, Type dataType)
        : base(index, title, dataType)
    {
        //
    }

    public ExportContentAttribute(int index, string title, Type dataType, string dataFormat = default, string prefix = default, string suffix = default, int enlargement = 1)
        : base(index, title, dataType, dataFormat, prefix, suffix, enlargement)
    {
        //
    }
}