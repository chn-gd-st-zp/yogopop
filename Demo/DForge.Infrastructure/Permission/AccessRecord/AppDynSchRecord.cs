namespace DForge.Infrastructure.DBSupport.Entity.Table
{
    [Description("动态调度记录")]
    public partial class TBAppDynSchRecord : IAccessRecordTrigger, IMultiLanguageObject
    {
        public string GetTriggerObjName() { return $"{MainType}_{SubType}_{Frequency}_{DataDate}_{TriggerID}"; }

        public string GroupKey => nameof(IAccessRecordTrigger);

        public string ItemKey => this.GetTBName();
    }
}

namespace DForge.Infrastructure.DBSupport.Repository
{
    public partial interface IAppDynSchRecordRepository : IAccessRecordTriggerRepository
    {
        //
    }
}