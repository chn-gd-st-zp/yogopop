namespace YogoPop.Component.Attachment;

public abstract class HandlerBase : IHandler
{
    protected abstract AttachmentHandlerEnum Handler { get; }

    public abstract AttachmentResultEnum Do(AttachmentOperationItemSetting operationItemSetting, string path, string fileName, string fileExt);
}