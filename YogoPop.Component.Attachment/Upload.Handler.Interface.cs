namespace YogoPop.Component.Attachment;

public interface IHandler : ITransient
{
    public AttachmentResultEnum Do(AttachmentOperationItemSetting operationItemSetting, string path, string fileName, string fileExt);
}