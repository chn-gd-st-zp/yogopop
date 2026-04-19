namespace YogoPop.Component.VerificationCode;

public static class VCGlobalSettings
{
    public static bool IsTestMode
    {
        get
        {
            var value = Environment.GetEnvironmentVariable("ASPNETCORE_VCSEND");
            if (value.IsEmptyString())
                return true;

            try
            {
                return !bool.Parse(value);
            }
            catch (Exception)
            {
                throw new Exception("无效的环境变量[VCSend]");
            }
        }
    }

    public readonly static string CodeTag = "|code|";

    public readonly static string DurationTag = "|duration|";
}