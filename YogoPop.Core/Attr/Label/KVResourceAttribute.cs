namespace YogoPop.Core.Attr.Label;

[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false)]
public class KVResourceForEnumAttribute : Attribute, IKVResourceForEnum
{
    //
}

/// <summary>
/// 公开的枚举
/// </summary>
public class PublicEnumAttribute : KVResourceForEnumAttribute
{
    public bool CheckItem { get; private set; }

    public PublicEnumAttribute(bool checkItem = false)
    {
        CheckItem = checkItem;
    }
}

/// <summary>
/// 内部的枚举
/// </summary>
public class InternalEnumAttribute : KVResourceForEnumAttribute
{
    //
}