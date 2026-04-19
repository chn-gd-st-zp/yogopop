namespace DForge.DSP.GoDaddy;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(IDSHandler<,>), DSPEnum.GoDaddy)]
[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(IDSOptRegistHandler<,>), DSPEnum.GoDaddy)]
public class DSHandler4GoDaddy<TDomain, TDNSRecord> : DSOptRegistHandler<DSSettings4GoDaddy, TDomain, TDNSRecord>
    where TDomain : class, IDomain, new()
    where TDNSRecord : class, IDNSRecord, new()
{
    // 一次最多并发请求个数
    private const int MAXCONCURRENCY = 5;

    // GoDaddy API 最大支持 1000
    private const int BATCHQTY = 1000;

    //public DSHandler4GoDaddy() : base() { }

    public DSHandler4GoDaddy(DSSettings4GoDaddy settings) : base(settings) { }

    public override Tuple<DMainStatusEnum, DSubStatusEnum> StatusExchange(object data)
    {
        //ACTIVE - All is well
        //AWAITING * -System is waiting for the end-user to complete an action
        //CANCELLED * -Domain has been cancelled, and may or may not be reclaimable
        //CONFISCATED - Domain has been confiscated, usually for abuse, chargeback, or fraud
        //DISABLED * -Domain has been disabled
        //EXCLUDED * -Domain has been excluded from Firehose registration
        //EXPIRED * -Domain has expired
        //FAILED * -Domain has failed a required action, and the system is no longer retrying
        //HELD * -Domain has been placed on hold, and likely requires intervention from Support
        //LOCKED*-Domain has been locked, and likely requires intervention from Support
        //PARKED*-Domain has been parked, and likely requires intervention from Support
        //PENDING*-Domain is working its way through an automated workflow
        //RESERVED * -Domain is reserved, and likely requires intervention from Support
        //REVERTED -Domain has been reverted, and likely requires intervention from Support
        //SUSPENDED*-Domain has been suspended, and likely requires intervention from Support
        //TRANSFERRED*-Domain has been transferred out
        //UNKNOWN - Domain is in an unknown state
        //UNLOCKED * -Domain has been unlocked, and likely requires intervention from Support
        //UNPARKED*-Domain has been unparked, and likely requires intervention from Support
        //UPDATED*-Domain ownership has been transferred to another account

        if (data == null) return Tuple.Create(DMainStatusEnum.None, DSubStatusEnum.None);

        var dataStr = data.ToString().ToUpper();
        if (dataStr.IsEmptyString()) return Tuple.Create(DMainStatusEnum.None, DSubStatusEnum.None);

        if (dataStr.In("ACTIVE", "UNLOCKED", "UNPARKED", "UPDATED")) return Tuple.Create(DMainStatusEnum.Normal, DSubStatusEnum.None);
        if (dataStr.In("EXPIRED")) return Tuple.Create(DMainStatusEnum.Expired, DSubStatusEnum.None);
        if (dataStr.In("AWAITING", "HELD", "PENDING")) return Tuple.Create(DMainStatusEnum.Proceccing, DSubStatusEnum.None);

        return Tuple.Create(DMainStatusEnum.Disable, DSubStatusEnum.None);
    }

    public override async Task<IDSResult<IDSDetails<TDomain, TDNSRecord>>> DomainQuery(DSOptEnum dbOpt, string domainKey)
    {
        var result = new DSResult<IDSDetails<TDomain, TDNSRecord>>();
        var resultData = new DSDetails<TDomain, TDNSRecord>();

        var requestUrl = "";
        var requestHeader = new SortedDictionary<string, object>();
        var response = default(IFlurlResponse);
        var responseHttpCode = string.Empty;
        var responseDataString = string.Empty;
        var responseDataResult = default(ResponseResult);

        try
        {
            requestUrl = string.Format($"{Settings.BaseUrl}{Settings.DomainQueryRoute}", domainKey);
            requestHeader.Add("sso-key", $"{Settings.Key}:{Settings.Secret}");

            response = await requestUrl.WithHeaders(requestHeader).GetAsync();
            responseHttpCode = response.StatusCode.ToString();

            if (response.StatusCode.NotIn(HttpStatusCode.OK.ToInt(), HttpStatusCode.NonAuthoritativeInformation.ToInt())) return result.Populate($"HttpStatusCode Error: {responseHttpCode}");

            responseDataString = await response.GetStringAsync();
            if (responseDataString.IsEmptyString()) return result.Populate($"Response Data String Error: Empty");

            responseDataResult = responseDataString.ToObject<ResponseResult>();
            if (responseDataResult == null) return result.Populate($"Response Data String Error: Unexpected");

            var statusArray = StatusExchange(responseDataResult.Status);
            resultData = new DSDetails<TDomain, TDNSRecord>
            {
                Domain = new TDomain
                {
                    Name = responseDataResult.Name,
                    NameServers = responseDataResult.NameServers.ToString(','),
                    CreateTime = responseDataResult.CreateTime,
                    ExpiredTime = responseDataResult.ExpiredTime,
                    MainStatus = statusArray.Item1,
                    SubStatus = statusArray.Item2,

                    RegistSrcID = dbOpt != DSOptEnum.Regist ? string.Empty : responseDataResult.ID,
                    RegistSrcStatus = dbOpt != DSOptEnum.Regist ? string.Empty : responseDataResult.Status,

                    AnalyseSrcID = dbOpt != DSOptEnum.Analyse ? string.Empty : responseDataResult.ID,
                    AnalyseSrcStatus = dbOpt != DSOptEnum.Analyse ? string.Empty : responseDataResult.Status,
                }
            };
        }
        catch (Exception ex)
        {
            return result.Populate($"Exception Error: {ex.Message}", ex);
        }

        result.Data = resultData;
        return result;
    }

    public override async Task<IDSResult<IEnumerable<IDSDetails<TDomain, TDNSRecord>>>> DomainQuery(DSOptEnum dbOpt)
    {
        var result = new DSResult<IEnumerable<IDSDetails<TDomain, TDNSRecord>>>();
        var resultData = new List<DSDetails<TDomain, TDNSRecord>>();

        var requestUrl = "";
        var requestHeader = new SortedDictionary<string, object>();
        var response = default(IFlurlResponse);
        var responseHttpCode = string.Empty;
        var responseDataString = string.Empty;
        var responseDataResult = default(ResponseResults);

        var pageIndex = 1;
        var offset = string.Empty;

        try
        {
            requestUrl = $"{Settings.BaseUrl}{Settings.DomainsQueryRoute}?limit={BATCHQTY}";
            requestHeader.Add("Authorization", $"sso-key {Settings.Key}:{Settings.Secret}");

            while (true)
            {
                var reqUrl = requestUrl;
                if (!string.IsNullOrEmpty(offset)) reqUrl += $"&marker={offset}";

                response = await reqUrl.WithHeaders(requestHeader).GetAsync();
                responseHttpCode = response.StatusCode.ToString();

                if (response.StatusCode != HttpStatusCode.OK.ToInt()) { result.Populate($"HttpStatusCode Error: {responseHttpCode}, pageIndex: {pageIndex}"); break; }

                responseDataString = await response.GetStringAsync();
                if (responseDataString.IsEmptyString()) { result.Populate($"Response Data String Error: Empty, pageIndex: {pageIndex}"); break; }

                responseDataResult = responseDataString.ToObject<ResponseResults>();
                if (responseDataResult == null) { result.Populate($"Response Data String Error: Unexpected, pageIndex: {pageIndex}"); break; }
                if (responseDataResult.IsEmpty()) break;

                var rspDatas = responseDataResult.Select(o =>
                {
                    var statusArray = StatusExchange(o.Status);
                    return new DSDetails<TDomain, TDNSRecord>
                    {
                        Domain = new TDomain
                        {
                            Name = o.Name,
                            NameServers = o.NameServers.ToString(','),
                            CreateTime = o.CreateTime,
                            ExpiredTime = o.ExpiredTime,
                            MainStatus = statusArray.Item1,
                            SubStatus = statusArray.Item2,

                            RegistSrcID = dbOpt != DSOptEnum.Regist ? string.Empty : o.ID,
                            RegistSrcStatus = dbOpt != DSOptEnum.Regist ? string.Empty : o.Status,

                            AnalyseSrcID = dbOpt != DSOptEnum.Analyse ? string.Empty : o.ID,
                            AnalyseSrcStatus = dbOpt != DSOptEnum.Analyse ? string.Empty : o.Status,
                        }
                    };
                }).ToList();

                resultData.AddRange(rspDatas);

                offset = responseDataResult.Last().Name;

                pageIndex++;
            }
        }
        catch (Exception ex)
        {
            return result.Populate($"Exception Error: {ex.Message}, pageIndex: {pageIndex}", ex);
        }

        result.Data = resultData;
        return result;
    }

    public override async Task<IDSResult<TDomain, bool>> NSModify(TDomain domain, params string[] nsArray)
    {
        var result = new DSResult<TDomain, bool>();
        var resultData = false;

        var requestUrl = "";
        var requestHeader = new SortedDictionary<string, object>();
        var response = default(IFlurlResponse);
        var responseHttpCode = string.Empty;
        var responseDataString = string.Empty;

        try
        {
            requestUrl = string.Format($"{Settings.BaseUrl}{Settings.DomainNSModifyRoute}", domain);
            requestHeader.Add("sso-key", $"{Settings.Key}:{Settings.Secret}");

            response = await requestUrl.WithHeaders(requestHeader).PatchJsonAsync(new { nameServers = nsArray });
            responseHttpCode = response.StatusCode.ToString();

            if (response.StatusCode != HttpStatusCode.OK.ToInt()) return result.Populate(domain, $"HttpStatusCode Error: {responseHttpCode}");

            domain.NameServers = nsArray.ToString(',');

            resultData = true;
        }
        catch (Exception ex)
        {
            return result.Populate(domain, $"Exception Error: {ex.Message}", ex);
        }

        result.Data = resultData;
        return result;
    }

    public override async Task<IDSResult<int>> NSModify(IEnumerable<TDomain> domains, params string[] nsArray)
    {
        var result = new DSResult<TDomain, int>();
        var resultData = 0;

        var taskResult = await Concurrency.Run<IDSResult<TDomain, bool>, bool, TDomain, string[]>(MAXCONCURRENCY, TimeSpan.FromSeconds(3), domains, NSModify, nsArray);
        resultData = taskResult.Where(o => o.Data).Count();

        result.Data = resultData;
        return result;
    }
}