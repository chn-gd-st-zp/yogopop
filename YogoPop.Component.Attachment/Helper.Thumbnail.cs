namespace YogoPop.Component.Attachment;

/// <summary>
/// 图片处理类
/// 1、生成缩略图片或按照比例改变图片的大小和画质
/// 2、将生成的缩略图放到指定的目录下
/// </summary>
public class Thumbnail
{
    private string _filePath;

    private int image_Width;
    private int image_Height;

    public Thumbnail(string filePath) => _filePath = filePath;

    /// <summary>
    /// 绘制缩略图
    /// </summary>
    /// <param name="width">缩略图的宽度</param>
    /// <param name="height">缩略图的高度</param>
    /// <returns>缩略图的Image对象</returns>
    public Image Draw(int width, int height)
    {
        var result = Image.Load(_filePath);
        result.Mutate(x => x.Resize(width, height));
        return result;
    }

    /// <summary>
    /// 绘制缩略图
    /// </summary>
    /// <param name="percent">缩略图的宽度百分比如: 需要百分之80，就填0.8</param>  
    /// <returns>缩略图的Image对象</returns>
    public Image Draw(double percent)
    {
        var result = Image.Load(_filePath);
        result.Mutate(x => x.Resize(Convert.ToInt32(result.Width * percent), (Convert.ToInt32(result.Height * percent))));
        return result;
    }
}