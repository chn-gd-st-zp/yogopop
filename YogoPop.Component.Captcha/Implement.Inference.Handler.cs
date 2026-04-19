namespace YogoPop.Component.Captcha;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(ICaptchaHandler), CaptchaEnum.Inference)]
public class InferenceCaptcha : ICaptchaHandler
{
    private readonly InferenceCaptchaSettings _settings;

    public InferenceCaptcha(InferenceCaptchaSettings settings) { _settings = settings; }

    public async Task<object> Generate()
    {
        var result = new List<InferenceCaptchaResult>();

        try
        {
            var sourceBM = default(Image<Rgba32>);

            #region 加载图片

            var sourceImg = default(Image);

            if (_settings.SourceAddress.ToLower().Contains("http:") || _settings.SourceAddress.ToLower().Contains("https:"))
            {
                var stream = await _settings.SourceAddress
                    .WithHeaders(_settings.Headers)
                    .GetStreamAsync();
                if (stream == null)
                    return result;

                sourceImg = await Image.LoadAsync<Rgba32>(stream);
            }
            else
            {
                if (!Directory.Exists(_settings.SourceAddress))
                    return result;

                var imgPaths = Directory.GetFiles(_settings.SourceAddress);
                if (imgPaths.Length == 0)
                    return result;

                var randomIndex = int.Parse(Unique.GetRandomCode1(0, imgPaths.Length - 1));
                sourceImg = await Image.LoadAsync<Rgba32>(imgPaths[randomIndex]);
            }

            if (sourceImg == null)
                return result;

            sourceBM = new Image<Rgba32>(sourceImg.Width, sourceImg.Height);
            sourceBM.Mutate(x => x.DrawImage(sourceImg, new Point(0, 0), 1f));

            #endregion;

            #region 裁除余量

            var startX = 0;
            var startY = 0;
            var marginW = sourceBM.Width % _settings.Columns;
            var marginH = sourceBM.Height % _settings.Rows;

            if (marginW != 0) startX = marginW / 2;

            if (marginH != 0) startY = marginH / 2;

            var cropWidth = sourceBM.Width - marginW;
            var cropHeight = sourceBM.Height - marginH;

            sourceBM.Mutate(x => x.Crop(new Rectangle(startX, startY, cropWidth, cropHeight)));

            #endregion

            #region 裁剪切片

            var piecesWidth = sourceBM.Width / _settings.Columns;
            var piecesHeight = sourceBM.Height / _settings.Rows;

            var index = 0;
            for (var row = 0; row < _settings.Rows; row++)
            {
                for (var col = 0; col < _settings.Columns; col++)
                {
                    var item = new InferenceCaptchaResult
                    {
                        Index = index++,
                        RowIndex = row,
                        ColIndex = col,
                    };

                    var x = col * piecesWidth;
                    var y = row * piecesHeight;

                    var pieceRect = new Rectangle(x, y, piecesWidth, piecesHeight);

                    using var piece = sourceBM.Clone(ctx => ctx.Crop(pieceRect));
                    using var stream = new MemoryStream();
                    await piece.SaveAsJpegAsync(stream, new JpegEncoder { Quality = 90 });
                    item.Image = stream.ToArray();

                    result.Add(item);
                }
            }

            #endregion
        }
        catch (Exception)
        {
            throw;
        }

        return result;
    }
}