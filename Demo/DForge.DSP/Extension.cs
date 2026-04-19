namespace DForge.DSP;

public static class DSPExtension
{
    public static bool IsSameDNS(this string domain, IDNSRecord record1, IDNSRecord record2)
    {
        if (record1.SrcID.IsNotEmpty() && record2.SrcID.IsNotEmpty() && record1.SrcID == record2.SrcID) return true;

        if (record1.Type != record2.Type) return false;
        if (record1.Target != record2.Target) return false;

        var source1 = record1.Source.ToLower().Replace($".{domain}", "");
        var source2 = record2.Source.ToLower().Replace($".{domain}", "");
        if (source1 != source2) return false;

        return true;
    }
}
