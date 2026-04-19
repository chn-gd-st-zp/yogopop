namespace DForge.Contract.SAdmin;

public interface IAppDNSRecordService<TTokenProvider> : ITransient where TTokenProvider : ITokenProvider
{
    public Task<IServiceResult<List<DTOAppDNSRecordListResult>>> List(DTOAppDNSRecordList input);

    public Task<IServiceResult<bool>> Set(DTOAppDNSRecordSet input);
}