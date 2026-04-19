namespace DForge.DSP;

[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = true)]
public class DSOptAttribute : Attribute
{
    public DSOptEnum DSOpt { get; private set; }

    public DSOptAttribute(DSOptEnum dsOpt) { DSOpt = dsOpt; }
}