namespace DForge.DSP.CloudFlare;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(IDSHandler<,>), DSPEnum.CloudFlare)]
[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(IDSOptAnalyseHandler<,>), DSPEnum.CloudFlare)]
public class DSHandler4CloudFlare<TDomain, TDNSRecord> : DSOptAnalyseHandler<DSSettings4CloudFlare, TDomain, TDNSRecord>
    where TDomain : class, IDomain, new()
    where TDNSRecord : class, IDNSRecord, new()
{
    // 一次最多并发请求个数
    private const int MAXCONCURRENCY = 5;

    // CloudFlare API 最大支持 50
    private const int BATCHQTY = 50;

    //public DSHandler4CloudFlare() : base() { }

    public DSHandler4CloudFlare(DSSettings4CloudFlare settings) : base(settings) { }

    public override Tuple<DMainStatusEnum, DSubStatusEnum> StatusExchange(object data)
    {
        if (data == null) return Tuple.Create(DMainStatusEnum.None, DSubStatusEnum.None);

        var dataObj = data as DomainItem;
        if (dataObj == null) return Tuple.Create(DMainStatusEnum.None, DSubStatusEnum.None);

        if (!dataObj.Paused) return Tuple.Create(DMainStatusEnum.Normal, DSubStatusEnum.None);

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
        var responseDataResult = default(ResponseResults<DomainItem>);

        try
        {
            requestUrl = string.Format($"{Settings.BaseUrl}{Settings.DomainQueryRoute}", domainKey);
            requestHeader.Add("Authorization", $"Bearer {Settings.Secret}");

            response = await requestUrl.WithHeaders(requestHeader).GetAsync();
            responseHttpCode = response.StatusCode.ToString();

            if (response.StatusCode != HttpStatusCode.OK.ToInt()) return result.Populate($"HttpStatusCode Error: {responseHttpCode}");

            responseDataString = await response.GetStringAsync();
            if (responseDataString.IsEmptyString()) return result.Populate($"Response Data String Error: Empty");

            responseDataResult = responseDataString.ToObject<ResponseResults<DomainItem>>();
            if (responseDataResult == null) return result.Populate($"Response Data String Error: Unexpected");

            if (!responseDataResult.Success) return result.Populate($"Response Data Failed: {responseDataResult.Errors.FirstOrDefault()?.Message} | {responseDataResult.Messages.FirstOrDefault()?.Message}");

            if (responseDataResult.Result.IsEmpty()) return result;

            var statusArray = StatusExchange(responseDataResult.Result);
            resultData = new DSDetails<TDomain, TDNSRecord>
            {
                Domain = new TDomain
                {
                    Name = responseDataResult.Result[0].Name,
                    MainStatus = statusArray.Item1,
                    SubStatus = statusArray.Item2,

                    RegistSrcID = dbOpt != DSOptEnum.Regist ? string.Empty : responseDataResult.Result[0].ID,

                    AnalyseSrcID = dbOpt != DSOptEnum.Analyse ? string.Empty : responseDataResult.Result[0].ID,
                    AnalyseSrcTrusteeship = dbOpt != DSOptEnum.Analyse ? string.Empty : responseDataResult.Result[0].Type,
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
        var responseDataResult = default(ResponseResults<DomainItem>);

        var pageIndex = 1;

        try
        {
            requestUrl = $"{Settings.BaseUrl}{Settings.DomainsQueryRoute}?per_page={BATCHQTY}";
            requestHeader.Add("Authorization", $"Bearer {Settings.Secret}");

            while (true)
            {
                var reqUrl = $"{requestUrl}&page={pageIndex}";

                response = await reqUrl.WithHeaders(requestHeader).GetAsync();
                responseHttpCode = response.StatusCode.ToString();

                if (response.StatusCode != HttpStatusCode.OK.ToInt()) { result.Populate($"HttpStatusCode Error: {responseHttpCode}, pageIndex: {pageIndex}"); break; }

                responseDataString = await response.GetStringAsync();
                if (responseDataString.IsEmptyString()) { result.Populate($"Response Data String Error: Empty, pageIndex: {pageIndex}"); break; }

                responseDataResult = responseDataString.ToObject<ResponseResults<DomainItem>>();
                if (responseDataResult == null) { result.Populate($"Response Data String Error: Unexpected, pageIndex: {pageIndex}"); break; }
                if (responseDataResult.Result.IsEmpty()) { break; }

                var rspDatas = responseDataResult.Result.Select(o =>
                {
                    var statusArray = StatusExchange(o);
                    return new DSDetails<TDomain, TDNSRecord>
                    {
                        Domain = new TDomain
                        {
                            Name = o.Name,
                            MainStatus = statusArray.Item1,
                            SubStatus = statusArray.Item2,

                            RegistSrcID = dbOpt != DSOptEnum.Regist ? string.Empty : o.ID,

                            AnalyseSrcID = dbOpt != DSOptEnum.Analyse ? string.Empty : o.ID,
                            AnalyseSrcTrusteeship = dbOpt != DSOptEnum.Analyse ? string.Empty : o.Type,
                        }
                    };
                }).ToList();

                resultData.AddRange(rspDatas);

                if (resultData.Count >= responseDataResult.ResultInfo.TotalCount) break;

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

    public override async Task<IDSResult<string, TDomain>> DomainAdd(string domain, string trusteeship)
    {
        var result = new DSResult<string, TDomain>();
        var resultData = default(TDomain);

        var requestUrl = "";
        var requestHeader = new SortedDictionary<string, object>();
        var response = default(IFlurlResponse);
        var responseHttpCode = string.Empty;
        var responseDataString = string.Empty;
        var responseDataResult = default(ResponseResult<DomainItem>);

        try
        {
            var _ = trusteeship.ToEnum<TrusteeshipEnum>();
        }
        catch
        {
            return result.Populate(domain, $"Trusteeship Unexpected Error: {trusteeship}");
        }

        try
        {
            requestUrl = $"{Settings.BaseUrl}{Settings.DomainAddRoute}";
            requestHeader.Add("Authorization", $"Bearer {Settings.Secret}");

            var requestBody = new
            {
                type = trusteeship.ToLower(),
                name = domain,
            };

            response = await requestUrl.WithHeaders(requestHeader).PostJsonAsync(requestBody);
            responseHttpCode = response.StatusCode.ToString();

            if (response.StatusCode != HttpStatusCode.OK.ToInt()) return result.Populate(domain, $"HttpStatusCode Error: {responseHttpCode}");

            responseDataString = await response.GetStringAsync();
            if (responseDataString.IsEmptyString()) return result.Populate(domain, $"Response Data String Error: Empty");

            responseDataResult = responseDataString.ToObject<ResponseResult<DomainItem>>();
            if (responseDataResult == null || responseDataResult.Result == null) return result.Populate(domain, $"Response Data String Error: Unexpected");

            if (!responseDataResult.Success) return result.Populate(domain, $"Response Data Failed: {responseDataResult.Errors.FirstOrDefault()?.Message} | {responseDataResult.Messages.FirstOrDefault()?.Message}");

            var statusArray = StatusExchange(responseDataResult.Result);
            resultData = new TDomain
            {
                Name = responseDataResult.Result.Name,
                MainStatus = statusArray.Item1,
                SubStatus = statusArray.Item2,
                AnalyseSrcID = responseDataResult.Result.ID,
                AnalyseSrcTrusteeship = trusteeship,
            };
        }
        catch (Exception ex)
        {
            return result.Populate(domain, $"Exception Error: {ex.Message}", ex);
        }

        result.Data = resultData;
        return result;
    }

    public override async Task<IDSResult<IEnumerable<TDomain>>> DomainAdd(IEnumerable<string> domains, string trusteeship)
    {
        var result = new DSResult<IEnumerable<TDomain>>();
        var resultData = new List<TDomain>();

        var taskResults = await Concurrency.Run<IDSResult<string, TDomain>, TDomain, string, string>(MAXCONCURRENCY, TimeSpan.FromSeconds(3), domains, DomainAdd, trusteeship);
        foreach (var taskResult in taskResults)
        {
            if (taskResult.Data != null)
                resultData.Add(taskResult.Data);

            var errorMsg = taskResult.ErrorMsg.IsEmptyString() ? string.Empty : $"{taskResult.Trigger}: {taskResult.ErrorMsg}";
            result.ErrorMsg = result.ErrorMsg.IsEmptyString() ? errorMsg : $"\r\n {errorMsg}";
        }

        result.Data = resultData;
        return result;
    }

    public override async Task<IDSResult<TDomain, bool>> DomainDel(TDomain domain)
    {
        var result = new DSResult<TDomain, bool>();
        var resultData = false;

        var requestUrl = "";
        var requestHeader = new SortedDictionary<string, object>();
        var response = default(IFlurlResponse);
        var responseHttpCode = string.Empty;
        var responseDataString = string.Empty;
        var responseDataResult = default(ResponseResult<DomainItem>);

        try
        {
            requestUrl = string.Format($"{Settings.BaseUrl}{Settings.DomainDelRoute}", domain.AnalyseSrcID);
            requestHeader.Add("Authorization", $"Bearer {Settings.Secret}");

            response = await requestUrl.WithHeaders(requestHeader).DeleteAsync();
            responseHttpCode = response.StatusCode.ToString();

            if (response.StatusCode != HttpStatusCode.OK.ToInt()) return result.Populate(domain, $"HttpStatusCode Error: {responseHttpCode}");

            responseDataString = await response.GetStringAsync();
            if (responseDataString.IsEmptyString()) return result.Populate(domain, $"Response Data String Error: Empty");

            responseDataResult = responseDataString.ToObject<ResponseResult<DomainItem>>();
            if (responseDataResult == null || responseDataResult.Result == null) return result.Populate(domain, $"Response Data String Error: Unexpected");

            if (!responseDataResult.Success) return result.Populate(domain, $"Response Data Failed: {responseDataResult.Errors.FirstOrDefault()?.Message} | {responseDataResult.Messages.FirstOrDefault()?.Message}");

            resultData = true;
        }
        catch (Exception ex)
        {
            return result.Populate(domain, $"Exception Error: {ex.Message}", ex);
        }

        result.Data = resultData;
        return result;
    }

    public override async Task<IDSResult<int>> DomainDel(IEnumerable<TDomain> domains)
    {
        var result = new DSResult<int>();
        var resultData = 0;

        var taskResults = await Concurrency.Run<IDSResult<TDomain, bool>, bool, TDomain>(MAXCONCURRENCY, TimeSpan.FromSeconds(3), domains, DomainDel);
        foreach (var taskResult in taskResults)
        {
            if (taskResult.Data)
                resultData++;

            var errorMsg = taskResult.ErrorMsg.IsEmptyString() ? string.Empty : $"{taskResult.Trigger.Name}: {taskResult.ErrorMsg}";
            result.ErrorMsg = result.ErrorMsg.IsEmptyString() ? errorMsg : $"\r\n {errorMsg}";
        }

        result.Data = resultData;
        return result;
    }

    public override async Task<IDSResult<TDomain, bool>> DomainEdt(TDomain domain)
    {
        var result = new DSResult<TDomain, bool>();
        var resultData = false;

        var requestUrl = "";
        var requestHeader = new SortedDictionary<string, object>();
        var response = default(IFlurlResponse);
        var responseHttpCode = string.Empty;
        var responseDataString = string.Empty;
        var responseDataResult = default(ResponseResult<DomainItem>);

        try
        {
            var _ = domain.AnalyseSrcTrusteeship.ToEnum<TrusteeshipEnum>();
        }
        catch
        {
            return result.Populate(domain, $"Trusteeship Unexpected Error: {domain.AnalyseSrcTrusteeship}");
        }

        try
        {
            requestUrl = string.Format($"{Settings.BaseUrl}{Settings.DomainEdtRoute}", domain.AnalyseSrcID);
            requestHeader.Add("Authorization", $"Bearer {Settings.Secret}");

            var requestBody = new
            {
                vanity_name_servers = domain.NameServers.SplitRemoveEmptyEntries(','),
            };

            response = await requestUrl.WithHeaders(requestHeader).PatchJsonAsync(requestBody);
            responseHttpCode = response.StatusCode.ToString();

            if (response.StatusCode != HttpStatusCode.OK.ToInt()) return result.Populate(domain, $"HttpStatusCode Error: {responseHttpCode}");

            responseDataString = await response.GetStringAsync();
            if (responseDataString.IsEmptyString()) return result.Populate(domain, $"Response Data String Error: Empty");

            responseDataResult = responseDataString.ToObject<ResponseResult<DomainItem>>();
            if (responseDataResult == null || responseDataResult.Result == null) return result.Populate(domain, $"Response Data String Error: Unexpected");

            if (!responseDataResult.Success) return result.Populate(domain, $"Response Data Failed: {responseDataResult.Errors.FirstOrDefault()?.Message} | {responseDataResult.Messages.FirstOrDefault()?.Message}");

            resultData = true;
        }
        catch (Exception ex)
        {
            return result.Populate(domain, $"Exception Error: {ex.Message}", ex);
        }

        result.Data = resultData;
        return result;
    }

    public override async Task<IDSResult<int>> DomainEdt(IEnumerable<TDomain> domains)
    {
        var result = new DSResult<int>();
        var resultData = 0;

        var taskResults = await Concurrency.Run<IDSResult<TDomain, bool>, bool, TDomain>(MAXCONCURRENCY, TimeSpan.FromSeconds(3), domains, DomainEdt);
        foreach (var taskResult in taskResults)
        {
            if (taskResult.Data)
                resultData++;

            var errorMsg = taskResult.ErrorMsg.IsEmptyString() ? string.Empty : $"{taskResult.Trigger.Name}: {taskResult.ErrorMsg}";
            result.ErrorMsg = result.ErrorMsg.IsEmptyString() ? errorMsg : $"\r\n {errorMsg}";
        }

        result.Data = resultData;
        return result;
    }

    public override async Task<IDSResult<TDNSRecord>> DNSRecordQuery(TDomain domain, string dnsKey)
    {
        var result = new DSResult<TDNSRecord>();
        var resultData = default(TDNSRecord);

        var requestUrl = "";
        var requestHeader = new SortedDictionary<string, object>();
        var response = default(IFlurlResponse);
        var responseHttpCode = string.Empty;
        var responseDataString = string.Empty;
        var responseDataResult = default(ResponseResult<DNSRecordItem>);

        try
        {
            requestUrl = string.Format($"{Settings.BaseUrl}{Settings.DNSRecordQueryRoute}", domain.AnalyseSrcID, dnsKey);
            requestHeader.Add("Authorization", $"Bearer {Settings.Secret}");

            response = await requestUrl.WithHeaders(requestHeader).GetAsync();
            responseHttpCode = response.StatusCode.ToString();

            if (response.StatusCode != HttpStatusCode.OK.ToInt()) return result.Populate($"HttpStatusCode Error: {responseHttpCode}");

            responseDataString = await response.GetStringAsync();
            if (responseDataString.IsEmptyString()) return result.Populate($"Response Data String Error: Empty");

            responseDataResult = responseDataString.ToObject<ResponseResult<DNSRecordItem>>();
            if (responseDataResult == null) return result.Populate($"Response Data String Error: Unexpected");

            if (!responseDataResult.Success) return result.Populate($"Response Data Failed: {responseDataResult.Errors.FirstOrDefault()?.Message} | {responseDataResult.Messages.FirstOrDefault()?.Message}");

            if (responseDataResult.Result == null) return result;

            var statusArray = StatusExchange(responseDataResult.Result);
            resultData = new TDNSRecord
            {
                Type = responseDataResult.Result.Type.ToEnum<DNSRecordEnum>(),
                Source = responseDataResult.Result.Name,
                Target = responseDataResult.Result.Content,
                TTL = responseDataResult.Result.TTL.ToString(),
                Proxied = responseDataResult.Result.Proxied,
                IPv4Only = responseDataResult.Result.Settings == null ? false : responseDataResult.Result.Settings.Ipv4Only,
                IPv6Only = responseDataResult.Result.Settings == null ? false : responseDataResult.Result.Settings.Ipv6Only,
                Remark = responseDataResult.Result.Comment,
                Tags = responseDataResult.Result.Tags.ToString(','),
                SrcID = responseDataResult.Result.ID,
            };
        }
        catch (Exception ex)
        {
            return result.Populate($"Exception Error: {ex.Message}", ex);
        }

        result.Data = resultData;
        return result;
    }

    public override async Task<IDSResult<IEnumerable<TDNSRecord>>> DNSRecordQuery(TDomain domain)
    {
        var result = new DSResult<IEnumerable<TDNSRecord>>();
        var resultData = new List<TDNSRecord>();

        var requestUrl = "";
        var requestHeader = new SortedDictionary<string, object>();
        var response = default(IFlurlResponse);
        var responseHttpCode = string.Empty;
        var responseDataString = string.Empty;
        var responseDataResult = default(ResponseResults<DNSRecordItem>);

        var pageIndex = 1;

        try
        {
            requestUrl = string.Format($"{Settings.BaseUrl}{Settings.DNSRecordsQueryRoute}?per_page={BATCHQTY}", domain.AnalyseSrcID);
            requestHeader.Add("Authorization", $"Bearer {Settings.Secret}");

            while (true)
            {
                var reqUrl = $"{requestUrl}&page={pageIndex}";

                response = await reqUrl.WithHeaders(requestHeader).GetAsync();
                responseHttpCode = response.StatusCode.ToString();

                if (response.StatusCode != HttpStatusCode.OK.ToInt()) { result.Populate($"HttpStatusCode Error: {responseHttpCode}, pageIndex: {pageIndex}"); break; }

                responseDataString = await response.GetStringAsync();
                if (responseDataString.IsEmptyString()) { result.Populate($"Response Data String Error: Empty, pageIndex: {pageIndex}"); break; }

                responseDataResult = responseDataString.ToObject<ResponseResults<DNSRecordItem>>();
                if (responseDataResult == null) { result.Populate($"Response Data String Error: Unexpected, pageIndex: {pageIndex}"); break; }
                if (responseDataResult.Result.IsEmpty()) { break; }

                var rspDatas = responseDataResult.Result.Select(o => new TDNSRecord
                {
                    Type = o.Type.ToEnum<DNSRecordEnum>(),
                    Source = o.Name,
                    Target = o.Content,
                    TTL = o.TTL.ToString(),
                    Proxied = o.Proxied,
                    IPv4Only = o.Settings == null ? false : o.Settings.Ipv4Only,
                    IPv6Only = o.Settings == null ? false : o.Settings.Ipv6Only,
                    Remark = o.Comment,
                    Tags = o.Tags.ToString(','),
                    SrcID = o.ID,
                }).ToList();

                resultData.AddRange(rspDatas);

                if (resultData.Count >= responseDataResult.ResultInfo.TotalCount) break;

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

    public override async Task<IDSResult<int>> DNSRecordAdd(TDomain domain, params TDNSRecord[] dnsRecordArray)
    {
        Func<TDNSRecord, TDomain, Task<IDSResult<TDNSRecord, bool>>> func = async (dnsRecord, doa) =>
        {
            var result = new DSResult<TDNSRecord, bool>();
            var resultData = false;

            var requestUrl = "";
            var requestHeader = new SortedDictionary<string, object>();
            var response = default(IFlurlResponse);
            var responseHttpCode = string.Empty;
            var responseDataString = string.Empty;
            var responseDataResult = default(ResponseResult<DNSRecordItem>);

            try
            {
                requestUrl = string.Format($"{Settings.BaseUrl}{Settings.DNSRecordAddRoute}", doa.AnalyseSrcID);
                requestHeader.Add("Authorization", $"Bearer {Settings.Secret}");

                var requestBody = new
                {
                    type = dnsRecord.Type.ToString(),
                    name = dnsRecord.Source,
                    content = dnsRecord.Target,
                    ttl = int.Parse(dnsRecord.TTL),
                    comment = dnsRecord.Remark,
                    //tags = dnsRecord.Tags.SplitRemoveEmptyEntries(','),
                    proxied = dnsRecord.Proxied,
                    settings = new
                    {
                        ipv4_only = dnsRecord.IPv4Only,
                        ipv6_only = dnsRecord.IPv6Only,
                    },
                };

                response = await requestUrl.WithHeaders(requestHeader).PostJsonAsync(requestBody);
                responseHttpCode = response.StatusCode.ToString();

                if (response.StatusCode != HttpStatusCode.OK.ToInt()) return result.Populate(dnsRecord, $"HttpStatusCode Error: {responseHttpCode}");

                responseDataString = await response.GetStringAsync();
                if (responseDataString.IsEmptyString()) return result.Populate(dnsRecord, $"Response Data String Error: Empty");

                responseDataResult = responseDataString.ToObject<ResponseResult<DNSRecordItem>>();
                if (responseDataResult == null || responseDataResult.Result == null) return result.Populate(dnsRecord, $"Response Data String Error: Unexpected");

                if (!responseDataResult.Success) return result.Populate(dnsRecord, $"Response Data Failed: {responseDataResult.Errors.FirstOrDefault()?.Message} | {responseDataResult.Messages.FirstOrDefault()?.Message}");

                dnsRecord.SrcID = responseDataResult.Result.ID;

                resultData = true;
            }
            catch (Exception ex)
            {
                return result.Populate(dnsRecord, $"Exception Error: {ex.Message}", ex);
            }

            result.Data = resultData;
            return result;
        };

        var result = new DSResult<TDomain, int>();
        var resultData = 0;

        var taskResults = await Concurrency.Run<IDSResult<TDNSRecord, bool>, bool, TDNSRecord, TDomain>(MAXCONCURRENCY, TimeSpan.FromSeconds(3), dnsRecordArray, func, domain);
        foreach (var taskResult in taskResults)
        {
            if (taskResult.Data)
                resultData++;

            var errorMsg = taskResult.ErrorMsg.IsEmptyString() ? string.Empty : $"{taskResult.Trigger.Type} - {taskResult.Trigger.Source} > {taskResult.Trigger.Target}: {taskResult.ErrorMsg}";
            result.ErrorMsg = result.ErrorMsg.IsEmptyString() ? errorMsg : $"\r\n {errorMsg}";
        }

        result.Data = resultData;
        return result;
    }

    public override async Task<IDSResult<int>> DNSRecordDel(TDomain domain, params TDNSRecord[] dnsRecordArray)
    {
        Func<TDNSRecord, TDomain, Task<IDSResult<TDNSRecord, bool>>> func = async (dnsRecord, doa) =>
        {
            var result = new DSResult<TDNSRecord, bool>();
            var resultData = false;

            var requestUrl = "";
            var requestHeader = new SortedDictionary<string, object>();
            var response = default(IFlurlResponse);
            var responseHttpCode = string.Empty;

            try
            {
                requestUrl = string.Format($"{Settings.BaseUrl}{Settings.DNSRecordDelRoute}", doa.AnalyseSrcID, dnsRecord.SrcID);
                requestHeader.Add("Authorization", $"Bearer {Settings.Secret}");

                response = await requestUrl.WithHeaders(requestHeader).DeleteAsync();
                responseHttpCode = response.StatusCode.ToString();

                if (response.StatusCode != HttpStatusCode.OK.ToInt()) return result.Populate(dnsRecord, $"HttpStatusCode Error: {responseHttpCode}");

                resultData = true;
            }
            catch (Exception ex)
            {
                return result.Populate(dnsRecord, $"Exception Error: {ex.Message}", ex);
            }

            result.Data = resultData;
            return result;
        };

        var result = new DSResult<TDomain, int>();
        var resultData = 0;

        var taskResults = await Concurrency.Run<IDSResult<TDNSRecord, bool>, bool, TDNSRecord, TDomain>(MAXCONCURRENCY, TimeSpan.FromSeconds(3), dnsRecordArray, func, domain);
        foreach (var taskResult in taskResults)
        {
            if (taskResult.Data)
                resultData++;

            var errorMsg = taskResult.ErrorMsg.IsEmptyString() ? string.Empty : $"{taskResult.Trigger.Type} - {taskResult.Trigger.Source} > {taskResult.Trigger.Target}: {taskResult.ErrorMsg}";
            result.ErrorMsg = result.ErrorMsg.IsEmptyString() ? errorMsg : $"\r\n {errorMsg}";
        }

        result.Data = resultData;
        return result;
    }

    public override async Task<IDSResult<int>> DNSRecordEdt(TDomain domain, params TDNSRecord[] dnsRecordArray)
    {
        Func<TDNSRecord, TDomain, Task<IDSResult<TDNSRecord, bool>>> func = async (dnsRecord, doa) =>
        {
            var result = new DSResult<TDNSRecord, bool>();
            var resultData = false;

            var requestUrl = "";
            var requestHeader = new SortedDictionary<string, object>();
            var response = default(IFlurlResponse);
            var responseHttpCode = string.Empty;
            var responseDataString = string.Empty;
            var responseDataResult = default(ResponseResult<DNSRecordItem>);

            try
            {
                requestUrl = string.Format($"{Settings.BaseUrl}{Settings.DNSRecordEdtRoute}", doa.AnalyseSrcID, dnsRecord.SrcID);
                requestHeader.Add("Authorization", $"Bearer {Settings.Secret}");

                var requestBody = new
                {
                    type = dnsRecord.Type.ToString(),
                    name = dnsRecord.Source,
                    content = dnsRecord.Target,
                    ttl = int.Parse(dnsRecord.TTL),
                    comment = dnsRecord.Remark,
                    //tags = dnsRecord.Tags.SplitRemoveEmptyEntries(','),
                    proxied = dnsRecord.Proxied,
                    settings = new
                    {
                        ipv4_only = dnsRecord.IPv4Only,
                        ipv6_only = dnsRecord.IPv6Only,
                    },
                };

                response = await requestUrl.WithHeaders(requestHeader).PatchJsonAsync(requestBody);
                responseHttpCode = response.StatusCode.ToString();

                if (response.StatusCode != HttpStatusCode.OK.ToInt()) return result.Populate(dnsRecord, $"HttpStatusCode Error: {responseHttpCode}");

                responseDataString = await response.GetStringAsync();
                if (responseDataString.IsEmptyString()) return result.Populate(dnsRecord, $"Response Data String Error: Empty");

                responseDataResult = responseDataString.ToObject<ResponseResult<DNSRecordItem>>();
                if (responseDataResult == null || responseDataResult.Result == null) return result.Populate(dnsRecord, $"Response Data String Error: Unexpected");

                if (!responseDataResult.Success) return result.Populate(dnsRecord, $"Response Data Failed: {responseDataResult.Errors.FirstOrDefault()?.Message} | {responseDataResult.Messages.FirstOrDefault()?.Message}");

                resultData = true;
            }
            catch (Exception ex)
            {
                return result.Populate(dnsRecord, $"Exception Error: {ex.Message}", ex);
            }

            result.Data = resultData;
            return result;
        };

        var result = new DSResult<TDomain, int>();
        var resultData = 0;

        var taskResults = await Concurrency.Run<IDSResult<TDNSRecord, bool>, bool, TDNSRecord, TDomain>(MAXCONCURRENCY, TimeSpan.FromSeconds(3), dnsRecordArray, func, domain);
        foreach (var taskResult in taskResults)
        {
            if (taskResult.Data)
                resultData++;

            var errorMsg = taskResult.ErrorMsg.IsEmptyString() ? string.Empty : $"{taskResult.Trigger.Type} - {taskResult.Trigger.Source} > {taskResult.Trigger.Target}: {taskResult.ErrorMsg}";
            result.ErrorMsg = result.ErrorMsg.IsEmptyString() ? errorMsg : $"\r\n {errorMsg}";
        }

        result.Data = resultData;
        return result;
    }
}