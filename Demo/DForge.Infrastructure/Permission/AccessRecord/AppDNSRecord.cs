namespace DForge.Infrastructure.DBSupport.Entity.Table
{
    [Description("DNS解析记录")]
    public partial class TBAppDNSRecord : IAccessRecordTrigger, IMultiLanguageObject
    {
        public string GetTriggerObjName() { return $"{Type}: {Source} -> {Target}"; }

        public string GroupKey => nameof(IAccessRecordTrigger);

        public string ItemKey => this.GetTBName();
    }
}

namespace DForge.Infrastructure.DBSupport.Repository
{
    public partial interface IAppDNSRecordRepository : IAccessRecordTriggerRepository
    {
        //
    }
}