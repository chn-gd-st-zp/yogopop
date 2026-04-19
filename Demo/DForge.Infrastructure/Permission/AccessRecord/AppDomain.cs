namespace DForge.Infrastructure.DBSupport.Entity.Table
{
    [Description("域名")]
    public partial class TBAppDomain : IAccessRecordTrigger, IMultiLanguageObject
    {
        public string GetTriggerObjName() { return $"{ProjectID}_{Name}"; }

        public string GroupKey => nameof(IAccessRecordTrigger);

        public string ItemKey => this.GetTBName();
    }
}

namespace DForge.Infrastructure.DBSupport.Repository
{
    public partial interface IAppDomainRepository : IAccessRecordTriggerRepository
    {
        //
    }
}