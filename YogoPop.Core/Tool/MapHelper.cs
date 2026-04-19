namespace YogoPop.Core.Tool;

public static class MapHelper
{
    public static object TypeTo(this object obj, Type destinationType)
    {
        if (!destinationType.IsGenericType)
        {
            return Convert.ChangeType(obj, destinationType);
        }
        else
        {
            Type genericTypeDefinition = destinationType.GetGenericTypeDefinition();
            if (genericTypeDefinition == typeof(Nullable<>))
                return Convert.ChangeType(obj, Nullable.GetUnderlyingType(destinationType));
        }

        throw new InvalidCastException("Invalid cast from type \"{0}\" to type \"{1}\".".Format(obj.GetType().FullName, destinationType.FullName));
    }

    public static object TypeTo<TDestination>(this object obj) => TypeTo(obj, typeof(TDestination));

    public static TDestination MapTo<TDestination>(this object source, bool byProfile = false)
        where TDestination : class
    {
        if (source == null) return default;

        Type tSource = source.GetType();
        Type tTarget = typeof(TDestination);

        IMapper mapper = byProfile ? InjectionContext.Resolve<IMapper>() : new MapperConfiguration(cfg => cfg.CreateMap(tSource, tTarget)).CreateMapper();

        return mapper.Map<TDestination>(source);
    }

    public static IEnumerable<TDestination> MapTo<TSource, TDestination>(this IEnumerable<TSource> source, bool byProfile = false) where TSource : class where TDestination : class => source.Select(o => o.MapTo<TDestination>(byProfile));

    public static TDestination AdaptTo<TDestination>(this object source) => source == null ? default : source.AdaptTo<TDestination>(TypeAdapterConfig.GlobalSettings);

    public static TDestination AdaptTo<TDestination>(this object source, TypeAdapterConfig config) => source == null ? default : config.GetDynamicMapFunction<TDestination>(source.GetType())(source);

    public static TDestination AdaptTo<TSource, TDestination>(this TSource source) => source == null ? default : TypeAdapter<TSource, TDestination>.Map(source);

    public static TDestination AdaptTo<TSource, TDestination>(this TSource source, TypeAdapterConfig config) => source == null ? default : config.GetMapFunction<TSource, TDestination>()(source);

    public static TDestination AdaptTo<TSource, TDestination>(this TSource source, TDestination destination) => source == null ? destination : source.AdaptTo(destination, TypeAdapterConfig.GlobalSettings);

    public static TDestination AdaptTo<TSource, TDestination>(this TSource source, TDestination destination, TypeAdapterConfig config) => source == null ? destination : config.GetMapToTargetFunction<TSource, TDestination>()(source, destination);
}