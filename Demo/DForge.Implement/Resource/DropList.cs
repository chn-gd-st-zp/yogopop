namespace DForge.Implement.Resource;

[DIModeForService(DIModeEnum.Exclusive, typeof(IDropListService<>))]
public partial class DropListService<TTokenProvider> : ApiResourceService<DropListService<TTokenProvider>, ICache, TTokenProvider>, IDropListService<TTokenProvider> where TTokenProvider : ITokenProvider
{
    public async Task<IServiceResult<List<DTODropItem>>> Role()
    {
        var result = default(List<DTODropItem>);

        using (var repository = InjectionContext.Resolve<IDBRepository>())
        {
            result = (await repository.DBContext.ListAsync<TBSysRole>())
            .Select(o => new DTODropItem
            {
                Name = o.Name,
                Value = o.PrimaryKey
            })
            .ToList();
        }

        return result.Success<List<DTODropItem>, LogicSucceed>();
    }

    public async Task<IServiceResult<List<DTODropItem>>> Project()
    {
        var result = default(List<DTODropItem>);

        using (var repository = InjectionContext.Resolve<IDBRepository>())
        {
            result = (await repository.DBContext.ListAsync<TBAppProject>())
            .Select(o => new DTODropItem
            {
                Name = o.Name,
                Value = o.PrimaryKey
            })
            .ToList();
        }

        return result.Success<List<DTODropItem>, LogicSucceed>();
    }

    public async Task<IServiceResult<DTOPageObj<DTOAppDSPChannelDropListResult>>> Channel(DTOAppDSPChannelDropList input)
    {
        var result = default(DTOPageObj<DTOAppDSPChannelDropListResult>);

        using (var repository = InjectionContext.Resolve<IDBRepository>())
        {
            var query = repository.DBContext.GetQueryObject<TBAppDSPChannel>() as IQueryable<TBAppDSPChannel>;

            query = query.WhereIf(input.Keyword.IsNotEmptyString(), o => o.Alias.Contains(input.Keyword));

            result = (await repository.PageByQueryableAsync<TBAppDSPChannel, DTOSort>(query, input))
                .ToDTOPageObj(input, o => new DTOAppDSPChannelDropListResult
                {
                    Name = o.Alias,
                    Value = o.PrimaryKey,
                    DSP = o.DSP,
                });
        }

        return result.Success<DTOPageObj<DTOAppDSPChannelDropListResult>, LogicSucceed>();
    }
}