namespace YogoPop.Core.Tool;

public static class RegexHelper
{
    public static bool IsCN(this string str) => Regex.IsMatch(str, @"^[\u4e00-\u9fa5]+$");

    public static bool IsEN(this string str) => Regex.IsMatch(str, @"^[a-zA-Z]+$");
}