namespace YogoPop.Core.Extension;

public static class VersionExtension
{
    public static bool IsOlderThan(this Version ver1, Version ver2)
    {
        //返回负值: 前一个版本 小于 后一个版本；
        //返回零值: 前一个版本 等于 后一个版本；
        //返回正值: 前一个版本 大于 后一个版本。

        return ver1.CompareTo(ver2) < 0;
    }

    public static bool IsSame(this Version ver1, Version ver2)
    {
        //返回负值: 前一个版本 小于 后一个版本；
        //返回零值: 前一个版本 等于 后一个版本；
        //返回正值: 前一个版本 大于 后一个版本。

        return ver1.CompareTo(ver2) == 0;
    }

    public static bool IsNewerThan(this Version ver1, Version ver2)
    {
        //返回负值: 前一个版本 小于 后一个版本；
        //返回零值: 前一个版本 等于 后一个版本；
        //返回正值: 前一个版本 大于 后一个版本。

        return ver1.CompareTo(ver2) > 0;
    }
}