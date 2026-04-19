namespace YogoPop.Component.Permission;

public interface IPermissionInitialization : IYogoPopInitialization
{
    public void Operation(ref IEnumerable<IPermission> deleteList, ref IEnumerable<IPermission> createList);
}