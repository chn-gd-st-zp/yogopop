namespace YogoPop.Component.Permission;

public interface IAccessRecordTrigger : IDBEntity
{
    public abstract string GetTriggerObjName();
}