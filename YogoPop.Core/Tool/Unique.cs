namespace YogoPop.Core.Tool;

public static class Unique
{
    /// <summary>
    /// 获取ID
    /// </summary>
    /// <returns></returns>
    public static string GetID()
    {
        string result = string.Empty;

        var idGenerator = InjectionContext.Resolve<IIdentityGenerator>();
        if (idGenerator != null)
            return idGenerator.Get();

        return GetGUID();
    }

    /// <summary>
    /// 获取GUID
    /// </summary>
    /// <param name="replaceSplitCode">是否移除中间的'-'分隔符</param>
    /// <returns></returns>
    public static string GetGUID(bool replaceSplitCode = true)
    {
        string result = string.Empty;

        result = Guid.NewGuid().ToString();

        if (replaceSplitCode)
            result = result.Replace("-", string.Empty);

        return result;
    }

    /// <summary>
    /// 获取随机数生成器
    /// </summary>
    /// <returns></returns>
    public static Random GetRandom()
    {
        //return new Random(Guid.NewGuid().GetHashCode());

        //return new Random(DateTimeExtension.Now.Millisecond * new Random().Next(1000));

        long tick = DateTimeExtension.Now.Ticks;
        return new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
    }


    /// <summary>
    /// 生成随机码(纯数字)
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static string GetRandomCode1(int min, int max) => GetRandom().GetRandomCode1(min, max);

    /// <summary>
    /// 生成随机码(纯数字)
    /// </summary>
    /// <param name="random"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static string GetRandomCode1(this Random random, int min, int max) => random.Next(min, max + 1).ToString();

    /// <summary>
    /// 生成随机码(纯数字)
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string GetRandomCode1(int length) => GetRandom().GetRandomCode1(length);

    /// <summary>
    /// 生成随机码(纯数字)
    /// </summary>
    /// <param name="random"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string GetRandomCode1(this Random random, int length) => random.Next((int)Math.Pow(10, length - 1), (int)Math.Pow(10, length) - 1).ToString();


    /// <summary>
    /// 生成随机码(纯字母)
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string GetRandomCode2(int length) => GetRandom().GetRandomCode2(length);

    /// <summary>
    /// 生成随机码(纯字母)
    /// </summary>
    /// <param name="random"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string GetRandomCode2(this Random random, int length)
    {
        var dictionary = new char[]
        {
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
        };

        var result = new StringBuilder(length);

        for (int i = 0; i < length; i++)
            result.Append(dictionary[random.Next(dictionary.Length)]);

        return result.ToString();
    }


    /// <summary>
    /// 生成随机码(纯字母)，但不包含: O I L o i l 这些容易混淆的字符
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string GetRandomCode3(int length) => GetRandom().GetRandomCode3(length);

    /// <summary>
    /// 生成随机码(纯字母)，但不包含: O I L o i l 这些容易混淆的字符
    /// </summary>
    /// <param name="random"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string GetRandomCode3(this Random random, int length)
    {
        var dictionary = new char[]
        {
            'a','b','c','d','e','f','g','h','j','k','m','n','p','q','r','s','t','u','v','w','x','y','z',
            'A','B','C','D','E','F','G','H','J','K','M','N','P','Q','R','S','T','U','V','W','X','Y','Z'
        };

        var result = new StringBuilder(length);

        for (int i = 0; i < length; i++)
            result.Append(dictionary[GetRandom().Next(dictionary.Length)]);

        return result.ToString();
    }


    /// <summary>
    /// 生成随机码，0-9 | a-z | A-Z
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string GetRandomCode4(int length) => GetRandom().GetRandomCode4(length);

    /// <summary>
    /// 生成随机码，0-9 | a-z | A-Z
    /// </summary>
    /// <param name="random"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string GetRandomCode4(this Random random, int length)
    {
        var dictionary = new char[]
        {
            '0','1','2','3','4','5','6','7','8','9',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
        };

        var result = new StringBuilder(length);

        for (int i = 0; i < length; i++)
            result.Append(dictionary[random.Next(dictionary.Length)]);

        return result.ToString();
    }


    /// <summary>
    /// 生成随机码，0-9 | a-z | A-Z，但不包含: 0 O I L o i l 这些容易混淆的字符
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string GetRandomCode5(int length) => GetRandom().GetRandomCode5(length);

    /// <summary>
    /// 生成随机码，0-9 | a-z | A-Z，但不包含: 0 O I L o i l 这些容易混淆的字符
    /// </summary>
    /// <param name="random"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string GetRandomCode5(this Random random, int length)
    {
        var dictionary = new char[]
        {
            '1','2','3','4','5','6','7','8','9',
            'a','b','c','d','e','f','g','h','j','k','m','n','p','q','r','s','t','u','v','w','x','y','z',
            'A','B','C','D','E','F','G','H','J','K','M','N','P','Q','R','S','T','U','V','W','X','Y','Z'
        };

        var result = new StringBuilder(length);

        for (int i = 0; i < length; i++)
            result.Append(dictionary[random.Next(dictionary.Length)]);

        return result.ToString();
    }
}