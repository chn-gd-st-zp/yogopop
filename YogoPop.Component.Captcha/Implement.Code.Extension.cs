namespace YogoPop.Component.Captcha;

public class CodeCaptchaGenerator : ICaptchaCodeGenerator
{
    public string Generate(int length) { return Unique.GetRandomCode5(length); }
}

public class CodeCaptchaGraphics : ICaptchaImageGenerator
{
    public byte[] Generate(int width, int height, string captchaCode)
    {
        var settings = InjectionContext.Resolve<CodeCaptchaSettings>();

        var codeW = settings.ImageWidth;
        var codeH = settings.ImageHeight;
        var fontSize = settings.FontSize;
        var fonts = settings.Fonts;
        var colors = settings.Colors.Select(c => Color.Parse(c)).ToArray();

        var fontSpace = (codeW - fontSize * captchaCode.Length) / (captchaCode.Length + 1);

        var random = Unique.GetRandom();
        var result = default(byte[]);

        try
        {
            // 颜色转换
            var colorList = colors.ToArray();

            // 创建画布（白底）
            using (var image = new Image<Rgba32>(codeW, codeH, Color.White))
            {
                // 画噪线
                for (int i = 0; i < settings.LineQty; i++)
                {
                    var color = colorList[random.Next(colorList.Length)];
                    var p1 = new PointF(random.Next(codeW), random.Next(codeH));
                    var p2 = new PointF(random.Next(codeW), random.Next(codeH));

                    image.Mutate(ctx => ctx.DrawLine(color, 1, p1, p2));
                }

                // 画噪点
                for (int i = 0; i < settings.PointQty; i++)
                {
                    var color = colorList[random.Next(colorList.Length)];
                    var x = random.Next(codeW);
                    var y = random.Next(codeH);

                    if (x >= 0 && x < codeW && y >= 0 && y < codeH)
                    {
                        image[x, y] = color;
                    }
                }

                // 画验证码文字
                for (int i = 0; i < captchaCode.Length; i++)
                {
                    var fontName = fonts[random.Next(fonts.Length)];
                    var font = default(Font);

                    try
                    {
                        font = SystemFonts.CreateFont(fontName, fontSize);
                    }
                    catch
                    {
                        var fontCollection = new FontCollection();
                        var family = fontCollection.Add(AppInitHelper.RootPath.CombinePath("fonts", "notosans-regular.ttf"));
                        font = family.CreateFont(fontSize, FontStyle.Regular);
                    }

                    var textColor = colorList[random.Next(colorList.Length)];
                    var position = new PointF(i * fontSize + i * fontSpace, (codeH - fontSize) / 2f);

                    image.Mutate(ctx => ctx.DrawText(captchaCode[i].ToString(), font, textColor, position));
                }

                // 输出 JPEG
                using (var ms = new MemoryStream())
                {
                    image.Save(ms, new JpegEncoder());
                    result = ms.ToArray();
                }
            }
        }
        catch
        {
            throw;
        }

        return result;
    }
}