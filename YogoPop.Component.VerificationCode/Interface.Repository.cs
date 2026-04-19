namespace YogoPop.Component.VerificationCode;

public interface IVCRepository : IDBRepository, ITransient
{
    public Task<IVCEntity> CreateRecordAsync(string eventKey, RemoteChannelEnum remoteChannel, string provider, string prefix, string num);

    public bool UpdateRecord(IVCEntity entity, bool save = true);

    public Task<IVCEntity> QueryRecordAsync(string eventKey, RemoteChannelEnum remoteChannel, string provider, string prefix, string num);
}