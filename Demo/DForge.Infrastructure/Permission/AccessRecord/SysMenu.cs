namespace DForge.Infrastructure.DBSupport.Entity.Table
{
    [Description("菜单")]
    public partial class TBSysMenu : IAccessRecordTrigger, IMultiLanguageObject
    {
        public string GetTriggerObjName() { return Name; }

        public string GroupKey => nameof(IAccessRecordTrigger);

        public string ItemKey => this.GetTBName();
    }
}

namespace DForge.Infrastructure.DBSupport.Repository
{
    public partial interface ISysMenuRepository : IAccessRecordTriggerRepository
    {
        //
    }
}