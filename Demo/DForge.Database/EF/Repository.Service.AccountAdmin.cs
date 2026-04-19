namespace DForge.Database.EF;

[DIModeForService(DIModeEnum.Exclusive, typeof(IAdminRepository))]
partial class AdminRepository : RenewEFDBRepository<TBAccountAdmin, string>, IAdminRepository
{
    public async Task<bool> CreateAsync(TBAccountAdmin accountAdmin, TBAccountInfo accountInfo)
    {
        var result = false;

        using (var tranScope = UnitOfWork.GenerateTransactionScope())
        {
            result = DBContext.Create(accountAdmin, false);
            if (!result)
                return result;

            result = DBContext.Create(accountInfo, false);
            if (!result)
                return result;

            if (result)
            {
                DBContext.SaveChanges();
                tranScope.Complete();
            }
        }

        return result;
    }

    public async Task<bool> UpdateAsync(TBAccountAdmin accountAdmin, TBAccountInfo accountInfo)
    {
        var result = false;

        using (var tranScope = UnitOfWork.GenerateTransactionScope())
        {
            result = DBContext.Update(accountAdmin, false);
            if (!result)
                return result;

            result = DBContext.Update(accountInfo, false);
            if (!result)
                return result;

            if (result)
            {
                DBContext.SaveChanges();
                tranScope.Complete();
            }
        }

        return result;
    }

    public async Task<Tuple<List<VTVAccountAdmin>, int>> PageAsync(DTOSysAdminPage input)
    {
        var query_acctAdmin = DBContext.GetQueryable<TBAccountAdmin>();
        var query_acctInfo = DBContext.GetQueryable<TBAccountInfo>();
        var query_role = DBContext.GetQueryable<TBSysRole>(false).Where(o => o.Type != RoleEnum.SuperAdmin);

        if (input.UserName.IsNotEmptyString())
            query_acctAdmin = query_acctAdmin.Where(r => r.UserName == input.UserName);

        var query = query_acctAdmin
            .GroupJoin(query_acctInfo, l => l.PrimaryKey, r => r.PrimaryKey, (l, r) => new { acctAdmin = l, r })
            .SelectMany(o => o.r.DefaultIfEmpty(), (l, r) => new { l.acctAdmin, acctInfo = r })
            .Where(o => o.acctInfo != null)
            .GroupJoin(query_role, l => l.acctAdmin.RoleID, r => r.PrimaryKey, (l, r) => new { l.acctAdmin, l.acctInfo, r })
            .SelectMany(o => o.r.DefaultIfEmpty(), (l, r) => new { l.acctAdmin, l.acctInfo, role = r })
            .Where(o => o.role != null)
            .Select(o => new VTVAccountAdmin
            {
                PrimaryKey = o.acctAdmin.PrimaryKey,
                RoleType = o.role == null ? RoleEnum.None : o.role.Type,
                RoleID = o.role.PrimaryKey,
                RoleName = o.role.Name,
                UserName = o.acctAdmin.UserName,
                MFASecret = o.acctInfo.MFASecret,
                CreateTime = o.acctAdmin.CreateTime,
                Status = o.acctAdmin.Status
            });

        var result = await base.PageByQueryableAsync<VTVAccountAdmin, DTOSort>(query, input);

        return result;
    }
}