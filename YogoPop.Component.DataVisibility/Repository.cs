namespace YogoPop.Component.DataVisibility;

public interface IDataVisiAssignRepository<TDataVisiAssign> : ITransient, IDBRepository
    where TDataVisiAssign : class, IDataVisiAssign
{
    public Task<bool> SaveAsync(TDataVisiAssign assign);

    public Task<IEnumerable<TDataVisiAssign>> LoadAsync(DataVisionEnum dataVision = DataVisionEnum.None, string sourceKey = default, params string[] identityKeys);
}

public interface IDataVisiElementSourceRepository : ITransient
{
    public Task<Tuple<IEnumerable<IDataVisiElementSource>, int>> ElementSourcePageAsync(DTOElementSourcePage input);
}