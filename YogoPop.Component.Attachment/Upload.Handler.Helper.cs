namespace YogoPop.Component.Attachment;

public static class AttachmentHandlerHelper
{
    public static AttachmentResult Operation(string key, string[] base64DataArray, string prefix = default)
    {
        var result = new AttachmentResult { State = AttachmentResultEnum.None, Items = new List<AttachmentItemResult>() };

        if (key.IsEmptyString())
        {
            result.State = AttachmentResultEnum.EmptyKey;
            return result;
        }

        if (base64DataArray == null || base64DataArray.Length == 0)
        {
            result.State = AttachmentResultEnum.EmptyData;
            return result;
        }

        var setting = InjectionContext.Resolve<AttachmentSettings>();

        var operation = setting.Operations.Where(o => o.Key.IsEquals(key)).SingleOrDefault();
        if (operation == null)
        {
            result.State = AttachmentResultEnum.OperationNotFound;
            return result;
        }

        foreach (var base64Data in base64DataArray)
        {
            var resultItem = new AttachmentItemResult();
            result.Items.Add(resultItem);

            var fileExt = base64Data.AnalyzeFileExt(setting).ToLower();
            var datas = base64Data.TirmFileDefine();

            var eHandler = setting.Basic.Handlers
                .Where(o => o.Exts.Select(oo => oo.ToLower()).Contains(fileExt))
                .Select(o => o.Handler)
                .FirstOrDefault();

            if (eHandler == AttachmentHandlerEnum.None)
            {
                resultItem.State = AttachmentResultEnum.ExtNotSupport;
                continue;
            }

            var handler = InjectionContext.ResolveByKeyed<IHandler>(eHandler);
            var handlerSetting = operation.Handlers.Where(o => o.Handler == eHandler).FirstOrDefault();
            if (handler == null || handlerSetting == null)
            {
                resultItem.State = AttachmentResultEnum.HandlerNotFound;
                continue;
            }

            if (handlerSetting.MaxKB > 0 && datas.IsOverSize(handlerSetting.MaxKB))
            {
                resultItem.State = AttachmentResultEnum.OverSize;
                continue;
            }

            key = key.ToLower();

            var basePath = AppInitHelper.GeneratePath(setting.Basic.PathMode, setting.Basic.PathAddr);
            if (prefix.IsNotEmptyString()) basePath += $"/{prefix}";
            basePath += $"/{key}";
            var fileName = Unique.GetGUID().ToLower();

            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            using (var stream = datas.ToStreamByBase64())
            using (var fileStream = File.Create($"{basePath}/{fileName}.{fileExt}"))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
            }

            resultItem.State = handler.Do(handlerSetting, basePath, fileName, fileExt);
            if (resultItem.State == AttachmentResultEnum.Success)
            {
                resultItem.FilePath = $"/{key}/{fileName}.{fileExt}";

                if (prefix.IsNotEmptyString()) resultItem.FilePath = $"/{prefix}{resultItem.FilePath}";
            }
        }

        result.State = result.Items.Count(o => o.State == AttachmentResultEnum.Success) == result.Items.Count() ? AttachmentResultEnum.Success : AttachmentResultEnum.Error;

        return result;
    }

    private static string AnalyzeFileExt(this string base64Data, AttachmentSettings settings)
    {
        var desc = base64Data.Substring(0, base64Data.IndexOf(","));
        desc = desc.Replace("data:", string.Empty);
        desc = desc.Replace(";base64", string.Empty);
        var ext = desc.IndexOf("/") != -1 ? desc.Substring(desc.IndexOf("/") + 1) : desc;
        ext = ext.Replace(".", string.Empty);

        if (settings.Basic.ExtMapping.IsNotEmpty() && settings.Basic.ExtMapping.ContainsKey(ext, true))
            ext = settings.Basic.ExtMapping.GetValue(ext, true);

        ext = ext.ToLower();

        return ext;
    }

    private static string TirmFileDefine(this string base64Data)
    {
        return base64Data.Substring(base64Data.IndexOf(",") + 1);
    }

    private static bool IsOverSize(this string base64DataWithNoFileDefine, int maxKB)
    {
        if (base64DataWithNoFileDefine.ToBytesByBase64().Length > maxKB * 1024)
            return true;

        return false;
    }
}