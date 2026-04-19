namespace DForge.Infrastructure.DBSupport.Entity.Table
{
    [Description("权限")]
    public partial class TBSysPermission : IAccessRecordTrigger, IPermission, IMultiLanguageObject
    {
        public string GetTriggerObjName() { return Name; }

        public string GroupKey => nameof(IAccessRecordTrigger);

        public string ItemKey => this.GetTBName();
    }
}

namespace DForge.Infrastructure.DBSupport.Repository
{
    public partial interface ISysPermissionRepository : IAccessRecordTriggerRepository, IPermissionRepository
    {
        //
    }
}