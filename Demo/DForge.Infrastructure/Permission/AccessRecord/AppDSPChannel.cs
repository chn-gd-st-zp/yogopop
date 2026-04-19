namespace DForge.Infrastructure.DBSupport.Entity.Table
{
    [Description("DSP通道")]
    public partial class TBAppDSPChannel : IAccessRecordTrigger, IMultiLanguageObject
    {
        public string GetTriggerObjName() { return $"{ProjectID}_{Alias}"; }

        public string GroupKey => nameof(IAccessRecordTrigger);

        public string ItemKey => this.GetTBName();
    }
}

namespace DForge.Infrastructure.DBSupport.Repository
{
    public partial interface IAppDSPChannelRepository : IAccessRecordTriggerRepository
    {
        //
    }
}