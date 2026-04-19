namespace DForge.Infrastructure.DBSupport.Entity.Table
{
    [Description("账号会话")]
    public partial class TBSessionAccount : IAccessRecordTrigger, IMultiLanguageObject
    {
        public string GetTriggerObjName() { return PrimaryKey; }

        public string GroupKey => nameof(IAccessRecordTrigger);

        public string ItemKey => this.GetTBName();
    }
}

namespace DForge.Infrastructure.DBSupport.Repository
{
    public partial interface ISessionRepository : IAccessRecordTriggerRepository
    {
        //
    }
}