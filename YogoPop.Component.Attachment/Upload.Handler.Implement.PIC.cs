namespace YogoPop.Component.Attachment;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(IHandler), AttachmentHandlerEnum.PIC)]
public class HandlerPIC : HandlerBase
{
    protected override AttachmentHandlerEnum Handler { get { return AttachmentHandlerEnum.PIC; } }

    public override AttachmentResultEnum Do(AttachmentOperationItemSetting operationItemSetting, string path, string fileName, string fileExt)
    {
        var result = AttachmentResultEnum.None;

        #region operation

        var thumbnail = new Thumbnail($"{path}/{fileName}.{fileExt}");
        foreach (var arg in operationItemSetting.ParseArgs<AttachmentPictureOperationArgSetting>())
        {
            var image = default(Image);

            if (!arg.ShrinkTo.IsEmptyString())
                image = thumbnail.Draw(int.Parse(arg.ShrinkTo) * 1.0 / 100);
            else if (!arg.Width.IsEmptyString() && !arg.Width.IsEmptyString())
                image = thumbnail.Draw(int.Parse(arg.Width), int.Parse(arg.Height));

            if (image == null)
                continue;

            image.Save($"{path}/{fileName}_{arg.Suffix}.{fileExt}");
            image.Dispose();
        }

        #endregion

        result = AttachmentResultEnum.Success;

        return result;
    }
}