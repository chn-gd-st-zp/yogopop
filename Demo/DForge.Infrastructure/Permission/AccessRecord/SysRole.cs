namespace DForge.Infrastructure.DBSupport.Entity.Table
{
    [Description("角色")]
    public partial class TBSysRole : IAccessRecordTrigger, IMultiLanguageObject
    {
        public string GetTriggerObjName() { return Name; }

        public string GroupKey => nameof(IAccessRecordTrigger);

        public string ItemKey => this.GetTBName();
    }
}

namespace DForge.Infrastructure.DBSupport.Repository
{
    public partial interface ISysRoleRepository : IAccessRecordTriggerRepository
    {
        //
    }
}