namespace YogoPop.Component.Attachment;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(IHandler), AttachmentHandlerEnum.DOC)]
public class HandlerDOC : HandlerBase
{
    protected override AttachmentHandlerEnum Handler { get { return AttachmentHandlerEnum.DOC; } }

    public override AttachmentResultEnum Do(AttachmentOperationItemSetting operationItemSetting, string path, string fileName, string fileExt)
    {
        var result = AttachmentResultEnum.None;

        #region operation

        //

        #endregion

        result = AttachmentResultEnum.Success;

        return result;
    }
}