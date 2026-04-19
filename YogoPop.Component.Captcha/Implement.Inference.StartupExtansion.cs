namespace YogoPop.Component.Captcha;

public static partial class StartupExtansion
{
    public static IServiceCollection RegisInferenceCaptcha(this IServiceCollection services, InferenceCaptchaSettings settings)
    {
        return services;
    }
}