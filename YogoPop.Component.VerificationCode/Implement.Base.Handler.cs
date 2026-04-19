namespace YogoPop.Component.VerificationCode;

public abstract class VCHandler<TSettings> : IHandler
    where TSettings : class, IVCSettings
{
    public abstract string Provider { get; }

    public TSettings Settings { get; private set; }

    public void LoadSettings(string json) { Settings = json.ToObject<TSettings>(); Init(); }

    public virtual void Init() { }

    public virtual async Task<bool> CreateAsync(string eventKey, RemoteChannelEnum remoteChannel, string prefix, string num, string message = default)
    {
        var result = true;

        prefix = prefix.IsNotEmptyString() ? prefix : string.Empty;
        num = num.IsNotEmptyString() ? num : string.Empty;
        message = message.IsNotEmptyString() ? message : string.Empty;

        using (var scope = InjectionContext.Root.CreateScope())
        using (var repository = scope.Resolve<IVCRepository>())
        {
            var entity = await repository.CreateRecordAsync(eventKey, remoteChannel, Provider, prefix, num);
            if (entity == null)
                return false;

            var sr = SendMessage(entity, message);

            result = sr.IsSuccess;
            if (!result)
                return result;

            entity.VerifyKey = sr.Data;
            entity.ExpiredTime = entity.CreateTime.AddSeconds(Settings.DurationSecond);
            entity.IsEnable = true;

            result = repository.UpdateRecord(entity);
            if (!result)
                return result;
        }

        return result;
    }

    public virtual async Task<bool> VerifyAsync(string eventKey, RemoteChannelEnum remoteChannel, string prefix, string num, string code)
    {
        var result = true;

        prefix = prefix.IsNotEmptyString() ? prefix : string.Empty;
        num = num.IsNotEmptyString() ? num : string.Empty;
        code = code.IsNotEmptyString() ? code : string.Empty;

        using (var scope = InjectionContext.Root.CreateScope())
        using (var repository = scope.Resolve<IVCRepository>())
        {
            var entity = await repository.QueryRecordAsync(eventKey, remoteChannel, Provider, prefix, num);
            if (entity == null)
                return false;

            using (var tranScope = UnitOfWork.GenerateTransactionScope())
            {
                entity.IsEnable = false;

                result = repository.UpdateRecord(entity, false);
                if (!result)
                    return result;

                result = VerifyByRemote(entity, code);
                if (!result)
                    return result;

                repository.DBContext.SaveChanges();
                tranScope.Complete();
            }
        }

        return result;
    }

    public abstract IServiceResult<string> SendMessage(IVCEntity entity, string message = default);

    public virtual bool VerifyByRemote(IVCEntity entity, string verifyCode) { return true; }
}