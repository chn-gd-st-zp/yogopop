namespace DForge.Infrastructure.DBSupport.Entity.Table
{
    [Description("项目")]
    public partial class TBAppProject : IAccessRecordTrigger, IMultiLanguageObject
    {
        public string GetTriggerObjName() { return Name; }

        public string GroupKey => nameof(IAccessRecordTrigger);

        public string ItemKey => this.GetTBName();
    }
}

namespace DForge.Infrastructure.DBSupport.Repository
{
    public partial interface IAppProjectRepository : IAccessRecordTriggerRepository
    {
        //
    }
}