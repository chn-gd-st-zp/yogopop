namespace DForge.Contract.SAdmin;

public interface ISysAccessRecordService<TTokenProvider> : ITransient where TTokenProvider : ITokenProvider
{
    public Task<IServiceResult<DTOSysAccessRecordSingleResult>> Single(DTOSysAccessRecordSingle input);

    public Task<IServiceResult<DTOPageObj<DTOSysAccessRecordPageResult>>> Page(DTOSysAccessRecordPage input);
}