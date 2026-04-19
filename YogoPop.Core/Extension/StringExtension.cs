namespace YogoPop.Core.Extension;

public static class StringExtension
{
    public static bool IsNull([AllowNull] this string text) => text == null;

    public static bool IsNull(this StringValues text) => text.ToString().IsNull();

    public static bool IsNotNull([AllowNull] this string text) => !text.IsNull();

    public static bool IsNotNull(this StringValues text) => !text.IsNull();

    public static bool IsEmptyString([AllowNull] this string text) => text == null || string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);

    public static bool IsEmptyString(this StringValues text) => text.ToString().IsEmptyString();

    public static bool IsNotEmptyString([AllowNull] this string text) => !text.IsEmptyString();

    public static bool IsNotEmptyString(this StringValues text) => !text.IsEmptyString();

    public static bool IsEquals(this string value1, string value2, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase) => value1.Equals(value2, stringComparison);

    public static Guid ToGuid(this string str)
    {
        var result = Guid.Empty;

        if (str.IsEmptyString())
            return result;

        if (!Guid.TryParse(str, out result))
            return result;

        return result;
    }

    public static string ToPath(this string str) => str.IsEmptyString() ? str : str.Replace("\\", "/").Replace(@"\", "/");

    public static string CombinePath(this string root, string path, params string[] paths)
    {
        if (root.IsEmptyString())
            return string.Empty;

        root = root.ToPath();
        root += !root.EndsWith("/") ? "/" : string.Empty;

        if (path.IsNotEmptyString())
        {
            path = path.ToPath();
            path += !path.EndsWith("/") ? "/" : string.Empty;
        }

        var combinedPaths = root + path;
        foreach (string ePath in paths)
            combinedPaths += ePath;

        return combinedPaths;
    }

    public static string CombineUrl(this string root, string path, params string[] paths)
    {
        if (root.IsEmptyString())
            return string.Empty;

        root = root.ToPath();
        root += !root.EndsWith("/") ? "/" : string.Empty;

        if (path.IsNotEmptyString())
        {
            path = path.ToPath();
            path += !path.EndsWith("/") ? "/" : string.Empty;
        }

        var combinedPaths = new Uri(new Uri(root), path);
        foreach (string ePath in paths)
            combinedPaths = new Uri(combinedPaths, ePath);

        return combinedPaths.AbsoluteUri;
    }

    public static string Format(this string str, params object[] args) => str.IsEmptyString() ? string.Empty : string.Format(str, args);

    public static string FirstChatToLower(this string str) => str.IsEmptyString() ? str : str.First().ToString().ToLower() + str.Substring(1);

    public static string FirstChatToUpper(this string str) => str.IsEmptyString() ? str : str.First().ToString().ToUpper() + str.Substring(1);

    public static string[] SplitRemoveEmptyEntries(this string str, char code) => str.Split(new char[] { code }, StringSplitOptions.RemoveEmptyEntries);

    public static string[] SplitRemoveEmptyEntries(this string str, string code) => str.Split(code, StringSplitOptions.RemoveEmptyEntries);

    public static string ToBase64(this string str) => str.ToBase64();

    public static string ToBase64(this string str, Encoding encoding) => str.IsEmptyString() ? str : Convert.ToBase64String(encoding.GetBytes(str));

    public static Stream ToStreamByBase64(this string base64Str) => new MemoryStream(Convert.FromBase64String(base64Str));

    public static byte[] ToBytesByBase64(this string base64Str) => Convert.FromBase64String(base64Str);

    public static byte[] ToBytesByHex(this string hexStr)
    {
        hexStr = hexStr.Replace(" ", string.Empty);
        if ((hexStr.Length % 2) != 0)
            hexStr += " ";

        byte[] returnBytes = new byte[hexStr.Length / 2];
        for (int i = 0; i < returnBytes.Length; i++)
            returnBytes[i] = Convert.ToByte(hexStr.Substring(i * 2, 2).Trim(), 16);

        return returnBytes;
    }

    public static string ToPinYin(this string str)
    {
        string result = string.Empty;
        foreach (char item in str)
        {
            try
            {
                ChineseChar cc = new ChineseChar(item);
                if (cc.Pinyins.IsNotEmpty() && cc.Pinyins[0].IsNotEmpty())
                {
                    string temp = cc.Pinyins[0].ToString();
                    result += temp.Substring(0, temp.Length - 1);
                }
            }
            catch (Exception)
            {
                result += item.ToString();
            }
        }
        return result;
    }

    public static char GetFirstChar(this string str)
    {
        var result = str.First();
        if (('a' <= result && result <= 'z') || ('A' <= result && result <= 'Z'))
        {
            return result;
        }
        else
        {
            try
            {
                ChineseChar cc = new ChineseChar(result);
                if (cc.Pinyins.IsNotEmpty() && cc.Pinyins[0].IsNotEmpty())
                {
                    return cc.Pinyins[0][0];
                }
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }
    }

    public static bool CheckLinks(this string str, string[] allowedDomains)
    {
        foreach (var allowedDomain in allowedDomains)
        {
            // Regular expression pattern to match HTTP links
            string pattern = @"https?://(www\.)?" + Regex.Escape(allowedDomain) + @"\b";

            // Create a regular expression object
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            // Find all matches
            MatchCollection matches = regex.Matches(str);

            // Validate the domain of each match using Uri class
            foreach (Match match in matches)
            {
                Uri uri = new Uri(match.Value);
                if (uri.Host != allowedDomain && !uri.Host.EndsWith("." + allowedDomain))
                    return false;
            }
        }

        return true;
    }
}