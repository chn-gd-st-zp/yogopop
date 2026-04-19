namespace DForge.Infrastructure.DBSupport.Entity.Table
{
    [Description("管理员")]
    public partial class TBAccountAdmin : IAccessRecordTrigger, IMultiLanguageObject
    {
        public string GetTriggerObjName() { return UserName; }

        public string GroupKey => nameof(IAccessRecordTrigger);

        public string ItemKey => this.GetTBName();
    }
}

namespace DForge.Infrastructure.DBSupport.Repository
{
    public partial interface IAdminRepository : IAccessRecordTriggerRepository
    {
        //
    }
}