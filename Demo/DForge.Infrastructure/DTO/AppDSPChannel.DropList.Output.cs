namespace DForge.Infrastructure.DTO;

public class DTOAppDSPChannelDropListResult : DTODropItem
{
    public DSPEnum DSP { get; set; }
    public DSOptEnum[] DSOpts => DSP.GetAttributes<DSOptAttribute>().Select(o => o.DSOpt).ToArray();
}