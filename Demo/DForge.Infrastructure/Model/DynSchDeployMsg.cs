namespace DForge.Infrastructure.Model;

[DIModeForService(DIModeEnum.Exclusive, typeof(IDynSchMQMsgConvertor))]
public class DynSchMQMsgConvertor : IDynSchMQMsgConvertor
{
    public DynSchMQMsg Restore(string msg)
    {
        var settings = new JsonSerializerSettings();
        settings.Converters.Add(new InterfaceToConcreteConverter(typeof(IDynSchRecordEntity), typeof(TBAppDynSchRecord)));
        settings.Converters.Add(new InterfaceToConcreteConverter(typeof(IDynSchMQMsgExtraData), typeof(DSPDynSchMQMsgExtraData)));
        settings.Converters.Add(new InterfaceToConcreteConverter(typeof(IChannel), typeof(TBAppDSPChannel)));
        settings.Converters.Add(new InterfaceToConcreteConverter(typeof(IDomain), typeof(TBAppDomain)));
        settings.Converters.Add(new InterfaceToConcreteConverter(typeof(IDNSRecord), typeof(TBAppDNSRecord)));

        return JsonConvert.DeserializeObject<DynSchMQMsg>(msg, settings);
    }
}

public sealed class InterfaceToConcreteConverter : JsonConverter
{
    private readonly Type _interfaceType;
    private readonly Type _concreteType;

    public InterfaceToConcreteConverter(Type interfaceType, Type concreteType)
    {
        if (interfaceType is null || concreteType is null)
            throw new ArgumentNullException();

        if (!interfaceType.IsAssignableFrom(concreteType))
            throw new ArgumentException($"{concreteType.FullName} does not implement {interfaceType.FullName}");

        _interfaceType = interfaceType;
        _concreteType = concreteType;
    }

    public override bool CanConvert(Type objectType) => objectType == _interfaceType;

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null) return null;

        // 读成 JToken 再按具体类型反序列化，避免 reader 游标问题
        var token = JToken.ReadFrom(reader);
        return token.ToObject(_concreteType, serializer);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        // 直接按实际运行时类型写回（不写 $type）
        serializer.Serialize(writer, value);
    }
}

public static class DynSchDeployMsgExtension
{
    public static List<DynSchMQMsg> GenerateMsg(this IEnumerable<TBAppDSPChannel> channels, DSOptEnum[] dsOpts, DateCycleEnum frequency, bool isManual = false)
    {
        var result = new List<DynSchMQMsg>();

        if (channels.IsEmpty()) return result;
        if (dsOpts.IsEmpty()) return result;
        if (frequency == DateCycleEnum.None) return result;

        var mainType = DynSchEnum.DomainSync;
        var subType = "";

        if (!mainType.GetAttributes<DynSchPeriodAttribute>().Any(o => o.DateCycle == frequency)) return result;

        var now = DateTimeExtension.Now;
        var date = now;
        var date_begin = date.ToBeginTime();

        foreach (var channel in channels)
        {
            var attrs = channel.DSP.GetAttributes<DSOptAttribute>().Where(o => o.DSOpt.In(dsOpts)).ToList();
            if (attrs.IsEmpty()) continue;

            foreach (var attr in attrs)
            {
                subType = attr.DSOpt.ToString();

                result.Add(new DynSchMQMsg
                {
                    Record = new TBAppDynSchRecord
                    {
                        TriggerID = channel.PrimaryKey,
                        IsManual = isManual,
                        MainType = mainType,
                        SubType = attr.DSOpt.ToString(),
                        Frequency = frequency,
                        CreateTime = DateTimeExtension.Now,
                        DataDate = date_begin,
                        DataYear = date_begin.Year,
                        DataMonth = date_begin.Month,
                        DataDay = date_begin.Day,
                        DataHour = date_begin.Hour,
                        DataMinute = date_begin.Minute,
                        DataSecond = date_begin.Second,
                        StartAt = now,
                        EndAt = now,
                        IsSuccess = false,
                    },
                    ExtraData = new DSPDynSchMQMsgExtraData
                    {
                        Channel = channel
                    },
                });
            }
        }

        return result;
    }

    public static List<DynSchMQMsg> GenerateMsg(this TBAppDSPChannel channel)
    {
        var result = new List<DynSchMQMsg>();

        if (channel == null) return result;

        var mainType = DynSchEnum.DomainSync;
        var subType = DSOptEnum.Regist.ToString();

        var isManual = true;
        var now = DateTimeExtension.Now;
        var date = now;
        var date_begin = date.ToBeginTime();

        result.Add(new DynSchMQMsg
        {
            Record = new TBAppDynSchRecord
            {
                TriggerID = channel.PrimaryKey,
                IsManual = isManual,
                MainType = mainType,
                SubType = subType,
                Frequency = DateCycleEnum.None,
                CreateTime = DateTimeExtension.Now,
                DataDate = date_begin,
                DataYear = date_begin.Year,
                DataMonth = date_begin.Month,
                DataDay = date_begin.Day,
                DataHour = date_begin.Hour,
                DataMinute = date_begin.Minute,
                DataSecond = date_begin.Second,
                StartAt = now,
                EndAt = now,
                IsSuccess = false,
            },
            ExtraData = new DSPDynSchMQMsgExtraData
            {
                Channel = channel
            },
        });

        return result;
    }

    public static List<DynSchMQMsg> GenerateMsg(this TBAppDSPChannel channel, IEnumerable<TBAppDomain> domains, string[] nameServers)
    {
        var result = new List<DynSchMQMsg>();

        if (channel == null) return result;
        if (domains.IsEmpty()) return result;
        if (nameServers.IsEmpty()) return result;

        domains = domains.Where(o => o.RegistChannelID == channel.PrimaryKey);
        if (domains.IsEmpty()) return result;

        foreach (var item in domains) item.Name = item.Name.ToLower();
        nameServers = nameServers.Select(o => o.ToLower()).ToArray();

        var mainType = DynSchEnum.NameServerSync;
        var subType = DSOptEnum.Regist.ToString();

        var isManual = true;
        var now = DateTimeExtension.Now;
        var date = now;
        var date_begin = date.ToBeginTime();

        result.Add(new DynSchMQMsg
        {
            Record = new TBAppDynSchRecord
            {
                TriggerID = channel.PrimaryKey,
                IsManual = isManual,
                MainType = mainType,
                SubType = subType,
                Frequency = DateCycleEnum.None,
                CreateTime = DateTimeExtension.Now,
                DataDate = date_begin,
                DataYear = date_begin.Year,
                DataMonth = date_begin.Month,
                DataDay = date_begin.Day,
                DataHour = date_begin.Hour,
                DataMinute = date_begin.Minute,
                DataSecond = date_begin.Second,
                StartAt = now,
                EndAt = now,
                IsSuccess = false,
            },
            ExtraData = new DSPDynSchMQMsgExtraData
            {
                Channel = channel,
                Domains = domains,
                NameServers = nameServers
            },
        });

        return result;
    }

    public static List<DynSchMQMsg> GenerateMsg(this TBAppDSPChannel channel, TBAppDomain domain)
    {
        var result = new List<DynSchMQMsg>();

        if (channel == null) return result;
        if (domain == null) return result;

        var mainType = DynSchEnum.DNSSync;
        var subType = DSOptEnum.Analyse.ToString();

        var isManual = true;
        var now = DateTimeExtension.Now;
        var date = now;
        var date_begin = date.ToBeginTime();

        result.Add(new DynSchMQMsg
        {
            Record = new TBAppDynSchRecord
            {
                TriggerID = domain.PrimaryKey,
                IsManual = isManual,
                MainType = mainType,
                SubType = subType,
                Frequency = DateCycleEnum.None,
                CreateTime = DateTimeExtension.Now,
                DataDate = date_begin,
                DataYear = date_begin.Year,
                DataMonth = date_begin.Month,
                DataDay = date_begin.Day,
                DataHour = date_begin.Hour,
                DataMinute = date_begin.Minute,
                DataSecond = date_begin.Second,
                StartAt = now,
                EndAt = now,
                IsSuccess = false,
            },
            ExtraData = new DSPDynSchMQMsgExtraData
            {
                Channel = channel,
                Domains = new IDomain[] { domain },
            },
        });

        return result;
    }

    public static List<DynSchMQMsg> GenerateMsg(this TBAppDSPChannel channel, TBAppDomain domain, IEnumerable<IDNSRecord> dnsRecords)
    {
        var result = new List<DynSchMQMsg>();

        if (channel == null) return result;
        if (domain == null) return result;

        domain.Name = domain.Name.ToLower();
        foreach (var item in dnsRecords) item.Source = item.Source.ToLower();

        var mainType = DynSchEnum.AnalyseSync;
        var subType = DSOptEnum.Analyse.ToString();

        var isManual = true;
        var now = DateTimeExtension.Now;
        var date = now;
        var date_begin = date.ToBeginTime();

        result.Add(new DynSchMQMsg
        {
            Record = new TBAppDynSchRecord
            {
                TriggerID = domain.PrimaryKey,
                IsManual = isManual,
                MainType = mainType,
                SubType = subType,
                Frequency = DateCycleEnum.None,
                CreateTime = DateTimeExtension.Now,
                DataDate = date_begin,
                DataYear = date_begin.Year,
                DataMonth = date_begin.Month,
                DataDay = date_begin.Day,
                DataHour = date_begin.Hour,
                DataMinute = date_begin.Minute,
                DataSecond = date_begin.Second,
                StartAt = now,
                EndAt = now,
                IsSuccess = false,
            },
            ExtraData = new DSPDynSchMQMsgExtraData
            {
                Channel = channel,
                Domains = new IDomain[] { domain },
                DNSRecords = dnsRecords,
            },
        });

        return result;
    }
}