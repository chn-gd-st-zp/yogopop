namespace YogoPop.Component.Captcha;

public static partial class StartupExtansion
{
    public static IServiceCollection RegisCodeCaptcha(this IServiceCollection services, CodeCaptchaSettings settings)
    {
        services.AddMemoryCache();

        services.AddSimpleCaptcha(builder =>
        {
            builder.UseMemoryStore();
            builder.AddConfiguration(options =>
            {
                options.CodeLength = settings.CodeLength;
                options.ImageWidth = settings.ImageWidth;
                options.ImageHeight = settings.ImageHeight;
                options.IgnoreCase = false;
                options.ExpiryTime = TimeSpan.FromSeconds(1);

                options.CodeGenerator = new CodeCaptchaGenerator();
                options.ImageGenerator = new CodeCaptchaGraphics();
            });
        });

        return services;
    }
}