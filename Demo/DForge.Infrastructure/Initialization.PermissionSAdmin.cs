namespace DForge.Infrastructure;

public class SAdminPermissionInitialization : IPermissionInitialization
{
    public void Operation(ref IEnumerable<IPermission> deleteList, ref IEnumerable<IPermission> createList)
    {
        var permissions_db = deleteList.Select(o => o as TBSysPermission).ToList();
        var permissions_sv = createList.Select(o => o as TBSysPermission).ToList();

        //permissions_db = permissions_db.Where(o => o.FullNode.Contains($",{GlobalPermissionEnum.SAdmin},")).ToList();
        //permissions_sv = permissions_sv.Where(o => o.FullNode.Contains($",{GlobalPermissionEnum.SAdmin},")).ToList();

        foreach (var permission_sv in permissions_sv)
        {
            var datas = permission_sv.GetFullCode(permissions_sv);
            permission_sv.FullNode = datas.Item1;
            permission_sv.FullSequence = datas.Item2;

            var permission_DB = permissions_db.Where(o => o.CurNode == permission_sv.CurNode).SingleOrDefault();
            if (permission_DB != null) permission_sv.AccessLogger = permission_DB.AccessLogger;
        }

        permissions_db = permissions_db.OrderBy(o => o.FullSequence).ToList();
        permissions_sv = permissions_sv.OrderBy(o => o.FullSequence).ToList();

        deleteList = permissions_db.Select(o => o as IPermission).ToList();
        createList = permissions_sv.Select(o => o as IPermission).ToList();
    }
}