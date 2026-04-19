namespace YogoPop.Component.Swagger;

public static partial class StartupExtension
{
    // 版本名的格式: v+版本号
    private static string _versionFormat = "V";
    private static ApiVersion _versionDefault = ApiVersion.Default;

    public static IServiceCollection AddSwagger(
        this IServiceCollection services, SwaggerSettings swaggerSettings,
        OpenApiSecurityScheme oass = null, OpenApiSecurityRequirement oasr = null, string docDescription = null
    )
    {
        var useApiVerCtrl = swaggerSettings.VersionKeyInQuery.IsNotEmptyString() || swaggerSettings.VersionKeyInHeader.IsNotEmptyString();

        var apiList = new List<IApiVersionReader>();

        // 默认版本

        services
            .AddApiVersioning(option =>
            {
                // 为true时API返回支持的版本信息
                //option.ReportApiVersions = true;

                // 默认以当前最高版本进行访问
                //option.ApiVersionSelector = new CurrentImplementationApiVersionSelector(option);

                // 是否提供API版本服务
                // 在不提供版本号时，默认为1.0  如果不添加此配置，不提供版本号时会报错"message": "An API version is required, but was not specified."
                option.AssumeDefaultVersionWhenUnspecified = true;

                // 指定默认版本
                option.DefaultApiVersion = _versionDefault;

                if (!useApiVerCtrl)
                    option.ApiVersionReader = new UrlSegmentApiVersionReader();
                else
                {
                    if (swaggerSettings.VersionKeyInQuery.IsNotEmptyString() || swaggerSettings.VersionKeyInHeader.IsNotEmptyString())
                        apiList.Add(new CustomUrlSegmentApiVersionReader());

                    if (swaggerSettings.VersionKeyInQuery.IsNotEmptyString())
                    {
                        apiList.Add(new CustomQueryStringApiVersionReader(swaggerSettings.VersionKeyInQuery));
                        apiList.Add(new CustomQueryStringApiVersionReader(swaggerSettings.VersionKeyInQuery.Replace("-", "_")));
                    }

                    if (swaggerSettings.VersionKeyInHeader.IsNotEmptyString())
                    {
                        apiList.Add(new CustomHeaderApiVersionReader(swaggerSettings.VersionKeyInHeader));
                        apiList.Add(new CustomHeaderApiVersionReader(swaggerSettings.VersionKeyInHeader.Replace("-", "_")));
                    }

                    if (apiList.IsNotEmpty())
                        option.ApiVersionReader = ApiVersionReader.Combine(apiList.ToArray());
                }
            })
            .AddVersionedApiExplorer(option =>
            {
                if (!useApiVerCtrl) return;

                option.GroupNameFormat = _versionFormat;

                // 通知swagger替换控制器路由中的版本并配置api版本
                option.SubstituteApiVersionInUrl = true;
            })
            .AddSwaggerGen(option =>
            {
                option.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                option.DocInclusionPredicate((version, apiDescription) =>
                {
                    return !useApiVerCtrl ? true : apiDescription.GroupName == version;
                });
                option.Config(swaggerSettings, oass, oasr);

                if (!useApiVerCtrl)
                    option.SwaggerDoc($"{_versionDefault}", option.OpenApiInfo(swaggerSettings, docDescription));
            })
            .AddOptions<SwaggerGenOptions>()
            .Configure<IApiVersionDescriptionProvider>((option, provider) =>
            {
                if (!useApiVerCtrl) return;

                foreach (var description in provider.ApiVersionDescriptions)
                    option.SwaggerDoc($"{description.ApiVersion}", option.OpenApiInfo(swaggerSettings, docDescription, description.ApiVersion.ToString(), description.IsDeprecated));
            });

        return services;
    }

    public static void UseSwagger(this IConfigureCollection appConfigures, SwaggerSettings swaggerSettings, Action<SwaggerUIOptions> optionsAction = null)
    {
        var useApiVerCtrl = swaggerSettings.VersionKeyInQuery.IsNotEmptyString() || swaggerSettings.VersionKeyInHeader.IsNotEmptyString();

        appConfigures.App
            .UseSwagger()
            .UseSwaggerUI(options =>
            {
                if (!useApiVerCtrl)
                    options.SwaggerEndpoint($"/{swaggerSettings.RoutePrefix.ToLower()}/{_versionDefault}/swagger.json", $"v{_versionDefault}");
                else
                {
                    foreach (var description in appConfigures.ApiVerDescProvider.ApiVersionDescriptions)
                        options.SwaggerEndpoint($"/{swaggerSettings.RoutePrefix.ToLower()}/{description.ApiVersion}/swagger.json", $"v{description.ApiVersion}");
                }

                if (optionsAction != null)
                    optionsAction(options);
                else
                {
                    options.DocExpansion(DocExpansion.None);
                    options.DisplayOperationId();
                    options.DisplayRequestDuration();
                    options.EnableFilter();
                    options.EnableDeepLinking();
                    options.ShowExtensions();
                    options.DefaultModelExpandDepth(2);
                    options.DefaultModelsExpandDepth(2);
                    options.DefaultModelRendering(ModelRendering.Model);

                    if (AppInitHelper.Environment.In(EnvironmentEnum.UAT, EnvironmentEnum.SANDBOX, EnvironmentEnum.PROD))
                        options.SupportedSubmitMethods(new SubmitMethod[] { });
                }
            });
    }

    private static OpenApiInfo OpenApiInfo(this SwaggerGenOptions option, SwaggerSettings swaggerSettings, string docDescription, string version = default, bool isDeprecated = false)
    {
        return new OpenApiInfo()
        {
            //标题
            Title = swaggerSettings.Title,

            //文档说明
            Description = isDeprecated ? "此版本已放弃兼容" : (docDescription.IsNotEmptyString() ? docDescription : string.Empty),

            //当前版本
            Version = version.IsNotEmptyString() ? version : string.Empty,

            //TermsOfService = new Uri(string.Empty),

            //联系方式
            //Contact = new OpenApiContact() { Name = string.Empty, Email = string.Empty, Url = null },

            //许可证
            //License = new OpenApiLicense() { Name = string.Empty, Url = new Uri(string.Empty) }
        };
    }

    private static void Config(this SwaggerGenOptions option, SwaggerSettings swaggerSettings, OpenApiSecurityScheme oass = null, OpenApiSecurityRequirement oasr = null)
    {
        option.CustomSchemaIds(SwaggerExtension.CustomSchemaIdSelector);
        option.EnableAnnotations();
        option.DocumentFilter<ApiHiddenFilter>();
        option.DocumentFilter<PropertyOperationFilter>();
        option.DocumentFilter<RemoveContentTypeFilter>();
        option.OperationFilter<AccessTokenFilter>();
        option.OperationFilter<CustomizeHeaderFilter>();

        var xmls = AppInitHelper.GetPaths(ExternalFileEnum.XML, swaggerSettings.Patterns, swaggerSettings.Xmls);
        foreach (string xml in xmls)
            option.IncludeXmlComments(xml, true);

        if (!swaggerSettings.AccessTokenKeyInHeader.IsEmptyString() && oass != null && oasr != null)
        {
            option.AddSecurityDefinition(swaggerSettings.AccessTokenKeyInHeader, oass);
            option.AddSecurityRequirement(oasr);
        }
    }

    internal static string SubVersion(this string version)
    {
        if (version.IsEmptyString()) return string.Empty;

        var parts = version.SplitRemoveEmptyEntries('.');
        return parts.Length >= 2 ? $"{parts[0]}.{parts[1]}" : string.Empty;
    }
}


public class CustomUrlSegmentApiVersionReader : UrlSegmentApiVersionReader
{
    public override string? Read(HttpRequest request) => base.Read(request).SubVersion();
}

public class CustomQueryStringApiVersionReader : QueryStringApiVersionReader
{
    public CustomQueryStringApiVersionReader(params string[] parameterNames) : base(parameterNames) { }

    public override string? Read(HttpRequest request) => base.Read(request).SubVersion();
}

public class CustomHeaderApiVersionReader : HeaderApiVersionReader
{
    public CustomHeaderApiVersionReader(params string[] parameterNames) : base(parameterNames) { }

    public override string? Read(HttpRequest request) => base.Read(request).SubVersion();
}