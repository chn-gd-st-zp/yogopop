namespace DForge.Infrastructure.Helper;

public class AccountHelper
{
    public static bool IsValid(string data, bool allowEmpty = false)
    {
        if (!allowEmpty && data.IsEmptyString())
            return false;

        if (IsUserName(data, allowEmpty))
            return true;

        if (IsEmail(data, allowEmpty))
            return true;

        if (IsMobile(data, allowEmpty))
            return true;

        return false;
    }

    public static bool IsUserName(string data, bool allowEmpty = false)
    {
        if (!allowEmpty && data.IsEmptyString())
            return false;

        var accountRegex = InjectionContext.Resolve<IAccountRegex>();

        if (Regex.IsMatch(data, accountRegex.UserNameRegex))
            return true;

        return false;
    }

    public static bool IsEmail(string data, bool allowEmpty = false)
    {
        if (!allowEmpty && data.IsEmptyString())
            return false;

        return false;
    }

    public static bool IsMobile(string data, bool allowEmpty = false)
    {
        if (!allowEmpty && data.IsEmptyString())
            return false;

        return false;
    }

    public static string EncryptPassword(string password, string secret, bool encryptOnce = false)
    {
        password = encryptOnce ? password : YogoPopCoreSecurity.MD5.Encrypt(password);
        password = password.ToLower();
        password = YogoPopCoreSecurity.MD5.Encrypt(secret + password);
        password = password.ToLower();

        return password;
    }

    public static async Task<IServiceResult<bool>> Offline(string accessToken, Action<string> removeAccountSession, bool kick = true)
    {
        var result = false;

        var now = DateTimeExtension.Now;

        using (var scope = InjectionContext.Root.CreateScope())
        using (var repository = scope.Resolve<IDBRepository>())
        {
            try
            {
                var accountSession = await repository.DBContext.SingleAsync<TBSessionAccount>(o => o.PrimaryKey == accessToken);
                if (accountSession == null)
                    return result.Fail<bool, LogicFailed>();

                var deviceSession = await repository.DBContext.SingleAsync<TBSessionDevice>(o => o.PrimaryKey == accessToken);

                deviceSession.ExpiredTime = now;
                accountSession.ExpiredTime = now;

                result = true;
                using (var tranScope = UnitOfWork.GenerateTransactionScope())
                {
                    if (result)
                        result = repository.DBContext.Update(accountSession, false);

                    if (result && deviceSession != null)
                        result = repository.DBContext.Update(deviceSession, false);

                    if (result)
                        removeAccountSession(accessToken);

                    if (result)
                    {
                        repository.DBContext.SaveChanges();
                        tranScope.Complete();
                    }

                    return result ? result.Success<bool, LogicSucceed>() : result.Fail<bool, LogicFailed>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}