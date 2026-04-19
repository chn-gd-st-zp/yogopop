namespace YogoPop.Core;

public class VEnumFactory
{
    internal List<IVEnumItem> Members = new List<IVEnumItem>();
    internal Dictionary<string, IVEnumItem> Keys = new Dictionary<string, IVEnumItem>();
    internal Dictionary<int, IVEnumItem> Values = new Dictionary<int, IVEnumItem>();
    internal int Counter = -1;
}