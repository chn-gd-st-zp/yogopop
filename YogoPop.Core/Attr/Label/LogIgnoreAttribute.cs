namespace YogoPop.Core.Attr.Label;

/// <summary>
/// 做了此标记的话，忽略日志记录
/// 1. 在接口请求时
/// 2. 在发生异常时
/// 3. 在权限认证时
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
public class LogIgnoreAttribute : Attribute
{
    //
}