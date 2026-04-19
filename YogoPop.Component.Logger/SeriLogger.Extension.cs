namespace YogoPop.Component.Logger;

public static class SeriLoggerExtension
{
    public static LoggerConfiguration InclusiveLogger<TTrigger>(this LoggerSinkConfiguration configuration, LogEventLevel level)
    {
        var triggerName = typeof(TTrigger).Name;
        return configuration
            .Logger(o =>
                o.Filter
                .ByIncludingOnly(e => e.Level == level)
                .ToFile<TTrigger>(level)
            );
    }

    public static LoggerConfiguration ToFile<TTrigger>(this LoggerConfiguration configuration, LogEventLevel level)
    {
        var settings = InjectionContext.Resolve<SeriLoggerSettings>();
        var triggerName = typeof(TTrigger).Name;

        var factory = InjectionContext.Resolve<IYogoLoggerEnricherFactory>();
        var enrichers = factory == null ? null : factory.Enrichers.Select(o => o as ILogEventEnricher).ToArray();

        var template = settings.Template;
        if (enrichers.IsEmpty())
        {
            if (template.Contains("{cus_ext}", StringComparison.OrdinalIgnoreCase))
                template = template.Replace("{cus_ext}", " ");
        }
        if (enrichers.IsNotEmpty())
        {
            configuration = configuration.Enrich.With(enrichers);

            if (template.Contains("{cus_ext}", StringComparison.OrdinalIgnoreCase))
            {
                var cus_ext = enrichers.Select(o => "{" + ((IYogoLoggerEnricher)o).Key + "}").ToString(" ");
                template = template.Replace("{cus_ext}", $" {cus_ext} ");
            }
        }

        return configuration
            .Enrich.WithProperty("Timestamp", () => DateTimeExtension.NowOffset)
            .WriteTo.File(
                path: $"{AppInitHelper.GeneratePath(settings.PathMode, settings.PathAddr)}/{triggerName}/{level}-.log",
                restrictedToMinimumLevel: level,
                outputTemplate: template,
                rollingInterval: settings.RollingInterval,
                rollOnFileSizeLimit: settings.RollOnFileSizeLimit,
                fileSizeLimitBytes: settings.FileSizeLimitMB * 1024 * 1024,
                //retainedFileCountLimit: 7,
                encoding: System.Text.Encoding.UTF8
            );
    }

    public static void ToEmail(this SeriLogger seriLogger, LogEventLevel level, string message, Exception exception = null) => ToEmail(seriLogger.GetType().Name, level, message, exception);

    public static void ToEmail<TTrigger>(this SeriLogger<TTrigger> seriLogger, LogEventLevel level, string message, Exception exception = null) where TTrigger : class => ToEmail(typeof(TTrigger).Name, level, message, exception);

    private static void ToEmail(string triggerName, LogEventLevel level, string message, Exception exception = null)
    {
        var settings = InjectionContext.Resolve<SeriLoggerSettings>();

        if (!settings.ToEmail.Enable)
            return;

        if (settings.ToEmail.MinimumLevel > level)
            return;

        if (!settings.ToEmail.Triggers.Contains(triggerName))
            return;

        string realMsg = string.Empty;
        string pattern = settings.Template;
        do
        {
            if (pattern.StartsWith(" "))
            {
                realMsg += " ";
                pattern = pattern.Substring(1);
                continue;
            }

            pattern = pattern.Substring(pattern.IndexOf("{") + 1);

            if (pattern.StartsWith("NewLine", StringComparison.OrdinalIgnoreCase))
            {
                realMsg += @"<br>";
                pattern = pattern.Substring(pattern.IndexOf("}") + 1);
                continue;
            }

            if (pattern.StartsWith("TimeStamp:", StringComparison.OrdinalIgnoreCase))
            {
                string format = pattern.Substring(pattern.IndexOf(":") + 1, pattern.IndexOf("}") - 1 - pattern.IndexOf(":"));

                realMsg += DateTimeExtension.Now.ToString(format);
                pattern = pattern.Substring(pattern.IndexOf("}") + 1);
                continue;
            }

            if (pattern.StartsWith("Message", StringComparison.OrdinalIgnoreCase))
            {
                realMsg += message;
                pattern = pattern.Substring(pattern.IndexOf("}") + 1);
                continue;
            }

            if (pattern.StartsWith("Exception", StringComparison.OrdinalIgnoreCase))
            {
                realMsg += exception == null ? string.Empty : exception.ToString();
                pattern = pattern.Substring(pattern.IndexOf("}") + 1);
                continue;
            }

        } while (!pattern.IsEmptyString());

        //send email;
    }
}