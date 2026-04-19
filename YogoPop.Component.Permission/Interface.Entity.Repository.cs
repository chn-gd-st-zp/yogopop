namespace YogoPop.Component.Permission;

public interface IPermissionRepository : IDBRepository
{
    public bool Create(IEnumerable<IPermission> permissions);

    public bool Delete(IEnumerable<IPermission> permissions);

    public IPermission Permission(string code);

    public IEnumerable<IPermission> AllPermission();
}