namespace DForge.Database.EF;

[DIModeForService(DIModeEnum.Exclusive, typeof(ISysRoleRepository))]
public partial class SysRoleRepository : RenewEFDBRepository<TBSysRole, string>, ISysRoleRepository
{
    public async Task<bool> CreateAsync(TBSysRole role, List<TBSysRolePermission> permissions)
    {
        bool result = false;

        using (var tranScope = UnitOfWork.GenerateTransactionScope())
        {
            if (role != null)
            {
                result = DBContext.Create(role, false);
                if (!result)
                    return result;
            }

            if (permissions.IsNotEmpty())
            {
                result = DBContext.Create(permissions, false);
                if (!result)
                    return result;
            }

            if (result)
            {
                DBContext.SaveChanges();
                tranScope.Complete();
            }
        }

        return result;
    }

    public async Task<bool> UpdateAsync(TBSysRole role, List<TBSysRolePermission> permissions_c, List<TBSysRolePermission> permissions_d)
    {
        bool result = false;

        using (var tranScope = UnitOfWork.GenerateTransactionScope())
        {
            if (role != null)
            {
                result = DBContext.Update(role, false);
                if (!result)
                    return result;
            }

            if (permissions_c.IsNotEmpty())
            {
                result = DBContext.Create(permissions_c, false);
                if (!result)
                    return result;
            }

            if (permissions_d.IsNotEmpty())
            {
                result = DBContext.Delete(permissions_d, false);
                if (!result)
                    return result;
            }

            if (result)
            {
                DBContext.SaveChanges();
                tranScope.Complete();
            }
        }
        return result;
    }

    public async Task<TBSysRole> SingleByFirstUserTypeAsync()
    {
        var query = DBContext.GetQueryable<TBSysRole>();

        query = query.Where(o => o.Type == RoleEnum.User && o.GroupID == string.Empty).OrderByDescending(o => o.CreateTime);
        var result = await query.FirstOrDefaultAsync();

        return result;
    }

    public async Task<Tuple<List<TBSysRole>, int>> PageAsync(DTOSysRolePage input, string groupID = "")
    {
        var query = DBContext.GetQueryable<TBSysRole>().Where(o => o.Type != RoleEnum.SuperAdmin);

        if (groupID.IsEmptyString())
            query = query.Where(o => o.GroupID == string.Empty);
        else
            query = query.Where(o => o.GroupID == groupID);

        if (input.Name.IsNotEmptyString())
            query = query.Where(o => o.Name.Contains(input.Name));

        if (input.Type.IsNotNullOrDefault())
            query = query.Where(o => o.Type == input.Type);

        if (input.CreateTimeRange != null)
        {
            if (input.CreateTimeRange.Begin.HasValue && input.CreateTimeRange.End.HasValue)
                query = query.Where(o => input.CreateTimeRange.Begin <= o.CreateTime && o.CreateTime <= input.CreateTimeRange.End);
            else if (input.CreateTimeRange.Begin.HasValue)
                query = query.Where(o => input.CreateTimeRange.Begin <= o.CreateTime);
            else if (input.CreateTimeRange.End.HasValue)
                query = query.Where(o => o.CreateTime <= input.CreateTimeRange.End);
        }

        var result = await base.PageByQueryableAsync<TBSysRole, DTOSort>(query, input);

        return result;
    }

    public async Task<List<TBSysRole>> ListByCodesAsync(string[] roleCodes)
    {
        var query = DBContext.GetQueryable<TBSysRole>();

        query = query.Where(o => roleCodes.Contains(o.CurNode));

        var result = await query.ToListAsync();

        return result;
    }

    public async Task<TBSysRole> GetHighestRoleAsync(string[] roleCodes)
    {
        var query = DBContext.GetQueryable<TBSysRole>();

        query = query.Where(o => roleCodes.Contains(o.CurNode)).OrderBy(o => o.Level);

        var result = await query.FirstOrDefaultAsync();

        return result;
    }
}