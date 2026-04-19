namespace YogoPop.Component.Permission;

public interface IAccessRecordRepository : IDBRepository
{
    public Task<bool> CreateAsync(AccessRecord accessRecord);
}