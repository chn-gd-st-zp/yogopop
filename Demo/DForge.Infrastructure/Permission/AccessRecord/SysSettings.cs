namespace DForge.Infrastructure.DBSupport.Entity.Table
{
    [Description("系统设置")]
    public partial class TBSysSettings : IAccessRecordTrigger, IMultiLanguageObject
    {
        public string GetTriggerObjName() { return $"{Type.GetDesc()} - {Title}"; }

        public string GroupKey => nameof(IAccessRecordTrigger);

        public string ItemKey => this.GetTBName();
    }
}

namespace DForge.Infrastructure.DBSupport.Repository
{
    public partial interface ISysSettingsRepository : IAccessRecordTriggerRepository
    {
        //
    }
}