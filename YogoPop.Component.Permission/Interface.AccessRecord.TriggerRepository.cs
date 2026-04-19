namespace YogoPop.Component.Permission;

public interface IAccessRecordTriggerRepository : IDBRepository
{
    public IAccessRecordTrigger GetTriggerObj(object pk);
}