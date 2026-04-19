namespace DForge.DSP;

public abstract class DSPActuator<TActuator> : DynSchActuator<TActuator> where TActuator : class, IDynSchActuator
{
    protected bool CheckHandler<TDomain, TDNSRecord>(IDSHandler<TDomain, TDNSRecord> handler, DSOptEnum dsOpt, IChannel channel, object settings)
        where TDomain : class, IDomain, new()
        where TDNSRecord : class, IDNSRecord, new()
    {
        if (handler == null)
        {
            Logger.Error(new { DSOpt = dsOpt, channel.DSP, Settings = settings, ErrorMsg = "handler empty", }.ToJson());
            return false;
        }

        return true;
    }

    protected bool CheckResult<T1>(IDSResult<T1> dsResult, DSOptEnum dsOpt, IChannel channel, object settings, Func<bool> funcForCheck = null)
    {
        if (dsResult == null)
        {
            Logger.Error(new { DSOpt = dsOpt, channel.DSP, Settings = settings, ErrorMsg = "result empty", }.ToJson());
            return false;
        }
        if (dsResult.ErrorObj != null && dsResult.ErrorMsg.IsEmptyString())
        {
            Logger.Error(new { DSOpt = dsOpt, channel.DSP, Settings = settings, ErrorMsg = "request error" }.ToJson(), dsResult.ErrorObj);
            return false;
        }
        if (dsResult.ErrorMsg.IsNotEmpty())
        {
            Logger.Error(new { DSOpt = dsOpt, channel.DSP, Settings = settings, dsResult.ErrorMsg }.ToJson());
            return false;
        }
        if (funcForCheck != null && funcForCheck())
        {
            Logger.Info(new { DSOpt = dsOpt, channel.DSP, Settings = settings, Msg = "funcForCheck faild" }.ToJson());
            return false;
        }

        return true;
    }
}

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(IDynSchActuator), DynSchEnum.DomainSync)]
public class DSPDomainActuator : DSPActuator<DSPDomainActuator>
{
    protected override string Tag => DynSchEnum.DomainSync.ToString();

    public override async Task<bool> RunAsync(DynSchMQMsg msg)
    {
        var result = false;

        var logTag = $"{Tag}[{Unique.GetGUID()}]:";
        var dsOpt = default(DSOptEnum);
        var extraData = default(DSPDynSchMQMsgExtraData);
        var channel = default(IChannel);

        try
        {
            Logger.Info($"{logTag} 开始");

            #region 校验

            if (msg == null)
            {
                Logger.Error("msg is null");
                return result;
            }

            dsOpt = msg.Record.SubType.ToEnum(DSOptEnum.None);
            if (dsOpt == DSOptEnum.None)
            {
                Logger.Error("dsOpt is null");
                return result;
            }

            if (msg.ExtraData == null)
            {
                Logger.Error("extradata is null");
                return result;
            }

            extraData = msg.ExtraData as DSPDynSchMQMsgExtraData;
            if (extraData == null)
            {
                Logger.Error("extraData wrong type");
                return result;
            }

            channel = extraData.Channel;
            if (channel == null)
            {
                Logger.Error("channel is null");
                return result;
            }

            #endregion;

            var settings = channel.Settings.ToObject(DIScope.ResolveByKeyed<IDSSettings>(channel.DSP).GetType());
            if (settings == null)
            {
                Logger.Error($"{logTag} settings is null");
                return result;
            }

            using (var repository = DIScope.Resolve<IDSPRepository>())
            {
                var handler = DIScope.ResolveByKeyed<IDSHandler<Domain, DNSRecord>>(channel.DSP, settings.ToNamedParameter("settings"));
                if (!CheckHandler(handler, dsOpt, channel, settings)) return result;

                var handleResult = await handler.DomainQuery(dsOpt);
                if (!CheckResult(handleResult, dsOpt, channel, settings, () => handleResult.Data.IsEmpty())) return result;

                var domains = new List<IDomain>();
                var dnsRecords = new List<IDNSRecord>();
                handleResult.Data.ToList().ForEach(item =>
                {
                    if (item.Domain != null) domains.Add(item.Domain);
                    if (item.DNSRecords.IsNotEmpty()) dnsRecords.AddRange(item.DNSRecords);
                });

                Logger.Info($"{logTag} domains[{domains.Count}], dnsRecords[{dnsRecords.Count}]");
                await repository.SaveAsync(Logger, msg, dsOpt, channel, domains, dnsRecords);
            }
        }
        catch (Exception ex)
        {
            Logger.Error($"{logTag} {ex.Message}", ex);
        }
        finally
        {
            Logger.Info($"{logTag} 结束");
        }

        return true;
    }
}

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(IDynSchActuator), DynSchEnum.NameServerSync)]
public class DSPNameServerActuator : DSPActuator<DSPNameServerActuator>
{
    protected override string Tag => DynSchEnum.NameServerSync.ToString();

    public override async Task<bool> RunAsync(DynSchMQMsg msg)
    {
        var result = false;

        var logTag = $"{Tag}[{Unique.GetGUID()}]: ";
        var dsOpt = default(DSOptEnum);
        var extraData = default(DSPDynSchMQMsgExtraData);
        var channel = default(IChannel);
        var domains = default(IEnumerable<IDomain>);
        var nameServers = default(IEnumerable<string>);
        var domainList = default(List<Domain>);

        try
        {
            Logger.Info($"{logTag} 开始");

            #region 校验

            if (msg == null)
            {
                Logger.Error("msg is null");
                return result;
            }

            dsOpt = msg.Record.SubType.ToEnum(DSOptEnum.None);
            if (dsOpt != DSOptEnum.Regist)
            {
                Logger.Error("dsOpt wrong type");
                return result;
            }

            if (msg.ExtraData == null)
            {
                Logger.Error("extradata is null");
                return result;
            }

            extraData = msg.ExtraData as DSPDynSchMQMsgExtraData;
            if (extraData == null)
            {
                Logger.Error("extraData wrong type");
                return result;
            }

            channel = extraData.Channel;
            if (channel == null)
            {
                Logger.Error("channel is null");
                return result;
            }

            domains = extraData.Domains;
            if (domains.IsEmpty())
            {
                Logger.Error("domainArray is null");
                return result;
            }

            nameServers = extraData.NameServers;
            if (nameServers.IsEmpty())
            {
                Logger.Error("nsArray is null");
                return result;
            }

            domainList = domains.Select(o => o.MapTo<Domain>()).ToList();
            if (domainList.IsEmpty())
            {
                Logger.Error("domainList is null");
                return result;
            }

            #endregion

            var settings = channel.Settings.ToObject(DIScope.ResolveByKeyed<IDSSettings>(channel.DSP).GetType());
            if (settings == null)
            {
                Logger.Error($"{logTag} settings is null");
                return result;
            }

            using (var repository = DIScope.Resolve<IDSPRepository>())
            {
                var handler = DIScope.ResolveByKeyed<IDSOptRegistHandler<Domain, DNSRecord>>(channel.DSP, settings.ToNamedParameter("settings"));
                if (!CheckHandler(handler, dsOpt, channel, settings)) return result;

                var handleResult = await handler.NSModify(domainList, nameServers.ToArray());
                if (!CheckResult(handleResult, dsOpt, channel, settings, () => handleResult.Data == 0)) return result;

                Logger.Info($"{logTag} domains[{domainList.Count}], NameServers[{nameServers.ToString(',')}]");
                await repository.SaveAsync(Logger, msg, dsOpt, channel, domainList);
            }
        }
        catch (Exception ex)
        {
            Logger.Error($"{logTag} {ex.Message}", ex);
        }
        finally
        {
            Logger.Info($"{logTag} 结束");
        }

        return true;
    }
}

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(IDynSchActuator), DynSchEnum.DNSSync)]
public class DSPDNSActuator : DSPActuator<DSPDNSActuator>
{
    protected override string Tag => DynSchEnum.DNSSync.ToString();

    public override async Task<bool> RunAsync(DynSchMQMsg msg)
    {
        var result = false;

        var logTag = $"{Tag}[{Unique.GetGUID()}]: ";
        var dsOpt = default(DSOptEnum);
        var extraData = default(DSPDynSchMQMsgExtraData);
        var channel = default(IChannel);
        var domains = default(IEnumerable<IDomain>);
        var domain = default(IDomain);

        try
        {
            Logger.Info($"{logTag} 开始");

            #region 校验

            if (msg == null)
            {
                Logger.Error("msg is null");
                return result;
            }

            dsOpt = msg.Record.SubType.ToEnum(DSOptEnum.None);
            if (dsOpt != DSOptEnum.Analyse)
            {
                Logger.Error("dsOpt wrong type");
                return result;
            }

            if (msg.ExtraData == null)
            {
                Logger.Error("extradata is null");
                return result;
            }

            extraData = msg.ExtraData as DSPDynSchMQMsgExtraData;
            if (extraData == null)
            {
                Logger.Error("extraData wrong type");
                return result;
            }

            channel = extraData.Channel;
            if (channel == null)
            {
                Logger.Error("channel is null");
                return result;
            }

            domains = extraData.Domains;
            if (domains.IsEmpty())
            {
                Logger.Error("domainArray is null");
                return result;
            }

            domain = domains.FirstOrDefault();
            if (domain == null)
            {
                Logger.Error("domain is null");
                return result;
            }

            #endregion

            var settings = channel.Settings.ToObject(DIScope.ResolveByKeyed<IDSSettings>(channel.DSP).GetType());
            if (settings == null)
            {
                Logger.Error($"{logTag} settings is null");
                return result;
            }

            using (var repository = DIScope.Resolve<IDSPRepository>())
            {
                var handler = DIScope.ResolveByKeyed<IDSOptAnalyseHandler<Domain, DNSRecord>>(channel.DSP, settings.ToNamedParameter("settings"));
                if (!CheckHandler(handler, dsOpt, channel, settings)) return result;

                var dnsRecords_Remote = default(IEnumerable<DNSRecord>);
                var dnsRecords_Local = await repository.DNSRecords(domain);
                var dnsRecords_Add = new List<IDNSRecord>();
                var dnsRecords_Del = new List<IDNSRecord>();
                var dnsRecords_Edt = new List<IDNSRecord>();

                #region 域名维护

                var handleResult_DomainQuery = await handler.DomainQuery(dsOpt, domain.Name);
                if (!CheckResult(handleResult_DomainQuery, dsOpt, channel, settings)) return result;

                if (handleResult_DomainQuery.Data == null || handleResult_DomainQuery.Data.Domain == null)
                {
                    var handleResult_DomainAdd = await handler.DomainAdd(domain.Name, domain.AnalyseSrcTrusteeship);
                    if (!CheckResult(handleResult_DomainAdd, dsOpt, channel, settings, () => handleResult_DomainAdd.Data == null)) return result;
                    domain.AnalyseSrcID = handleResult_DomainAdd.Data.AnalyseSrcID;
                }
                else
                {
                    domain.AnalyseSrcID = handleResult_DomainQuery.Data.Domain.AnalyseSrcID;
                    //var handleResult_DomainEdt = await handler.DomainEdt(domain.MapTo<Domain>());
                    //if (!CheckResult(handleResult_DomainEdt, dsOpt, channel, settings, () => !handleResult_DomainEdt.Data)) return result;
                }

                #endregion

                #region 拉取DNS记录

                var handleResult_All = await handler.DNSRecordQuery(domain.MapTo<Domain>());
                if (!CheckResult(handleResult_All, dsOpt, channel, settings)) return result;

                dnsRecords_Remote = handleResult_All.Data;
                dnsRecords_Remote = dnsRecords_Remote.IsNotEmpty() ? dnsRecords_Remote : new List<DNSRecord>();

                #endregion

                #region 计算待处理数组

                // 新增 - 远端有，本地无
                foreach (var dnsRecord_Remote in dnsRecords_Remote)
                {
                    var dnsRecord_Local = dnsRecords_Local.FirstOrDefault(o => domain.Name.IsSameDNS(o, dnsRecord_Remote));
                    if (dnsRecord_Local != null) continue;

                    dnsRecords_Add.Add(dnsRecord_Remote.MapTo<DNSRecord>());
                }

                // 删除 - 本地有，远端无
                foreach (var dnsRecord_Local in dnsRecords_Local)
                {
                    var dnsRecord_Remote = dnsRecords_Remote.SingleOrDefault(o => domain.Name.IsSameDNS(o, dnsRecord_Local));
                    if (dnsRecord_Remote != null) continue;

                    dnsRecords_Del.Add(dnsRecord_Local);
                }

                //修改 - 本地有，远端有
                foreach (var dnsRecord_Local in dnsRecords_Local)
                {
                    var dnsRecord_Remote = dnsRecords_Remote.SingleOrDefault(o => domain.Name.IsSameDNS(o, dnsRecord_Local));
                    if (dnsRecord_Remote == null) continue;

                    dnsRecord_Local.Type = dnsRecord_Remote.Type;
                    dnsRecord_Local.Source = dnsRecord_Remote.Source;
                    dnsRecord_Local.Target = dnsRecord_Remote.Target;
                    dnsRecord_Local.TTL = dnsRecord_Remote.TTL;
                    dnsRecord_Local.Priority = dnsRecord_Remote.Priority;
                    dnsRecord_Local.Proxied = dnsRecord_Remote.Proxied;
                    dnsRecord_Local.IPv4Only = dnsRecord_Remote.IPv4Only;
                    dnsRecord_Local.IPv6Only = dnsRecord_Remote.IPv6Only;
                    dnsRecord_Local.Remark = dnsRecord_Remote.Remark;
                    dnsRecord_Local.Tags = dnsRecord_Remote.Tags;
                    dnsRecord_Local.SrcID = dnsRecord_Remote.SrcID;

                    dnsRecords_Edt.Add(dnsRecord_Local);
                }

                #endregion

                Logger.Info($"{logTag} domain[{domain.Name}], add:[{dnsRecords_Add.Count}], del:[{dnsRecords_Del.Count}], edt:[{dnsRecords_Edt.Count}]");
                await repository.SaveAsync(Logger, msg, dsOpt, channel, domain, dnsRecords_Add, dnsRecords_Del, dnsRecords_Edt);
            }
        }
        catch (Exception ex)
        {
            Logger.Error($"{logTag} {ex.Message}", ex);
        }
        finally
        {
            Logger.Info($"{logTag} 结束");
        }

        return true;
    }
}

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(IDynSchActuator), DynSchEnum.AnalyseSync)]
public class DSPAnalyseActuator : DSPActuator<DSPAnalyseActuator>
{
    protected override string Tag => DynSchEnum.AnalyseSync.ToString();

    public override async Task<bool> RunAsync(DynSchMQMsg msg)
    {
        var result = false;

        var logTag = $"{Tag}[{Unique.GetGUID()}]: ";
        var dsOpt = default(DSOptEnum);
        var extraData = default(DSPDynSchMQMsgExtraData);
        var channel = default(IChannel);
        var domains = default(IEnumerable<IDomain>);
        var domain = default(IDomain);
        var dnsRecords_input = default(IEnumerable<IDNSRecord>);

        try
        {
            Logger.Info($"{logTag} 开始");

            #region 校验

            if (msg == null)
            {
                Logger.Error("msg is null");
                return result;
            }

            dsOpt = msg.Record.SubType.ToEnum(DSOptEnum.None);
            if (dsOpt != DSOptEnum.Analyse)
            {
                Logger.Error("dsOpt wrong type");
                return result;
            }

            if (msg.ExtraData == null)
            {
                Logger.Error("extradata is null");
                return result;
            }

            extraData = msg.ExtraData as DSPDynSchMQMsgExtraData;
            if (extraData == null)
            {
                Logger.Error("extraData wrong type");
                return result;
            }

            channel = extraData.Channel;
            if (channel == null)
            {
                Logger.Error("channel is null");
                return result;
            }

            domains = extraData.Domains;
            if (domains.IsEmpty())
            {
                Logger.Error("domainArray is null");
                return result;
            }

            domain = domains.FirstOrDefault();
            if (domain == null)
            {
                Logger.Error("domain is null");
                return result;
            }

            dnsRecords_input = extraData.DNSRecords;

            #endregion

            var settings = channel.Settings.ToObject(DIScope.ResolveByKeyed<IDSSettings>(channel.DSP).GetType());
            if (settings == null)
            {
                Logger.Error($"{logTag} settings is null");
                return result;
            }

            using (var repository = DIScope.Resolve<IDSPRepository>())
            {
                var handler = DIScope.ResolveByKeyed<IDSOptAnalyseHandler<Domain, DNSRecord>>(channel.DSP, settings.ToNamedParameter("settings"));
                if (!CheckHandler(handler, dsOpt, channel, settings)) return result;

                var dnsRecords_Local = await repository.DNSRecords(domain);
                var dnsRecords_Add = new List<IDNSRecord>();
                var dnsRecords_Del = new List<IDNSRecord>();
                var dnsRecords_Edt = new List<IDNSRecord>();

                #region 计算待处理数组

                // 新增 - 提交有, 本地无
                foreach (var dnsRecord_input in dnsRecords_input)
                {
                    var dnsRecord_Local = dnsRecords_Local.FirstOrDefault(o => domain.Name.IsSameDNS(o, dnsRecord_input));
                    if (dnsRecord_Local != null) continue;

                    dnsRecords_Add.Add(dnsRecord_input.MapTo<DNSRecord>());
                }

                // 删除 - 本地有, 提交无
                foreach (var dnsRecord_Local in dnsRecords_Local)
                {
                    var dnsRecord_input = dnsRecords_input.SingleOrDefault(o => domain.Name.IsSameDNS(o, dnsRecord_Local));
                    if (dnsRecord_input != null) continue;

                    dnsRecords_Del.Add(dnsRecord_Local);
                }

                //修改 - 本地有, 提交有
                foreach (var dnsRecord_Local in dnsRecords_Local)
                {
                    var dnsRecord_Input = dnsRecords_input.SingleOrDefault(o => domain.Name.IsSameDNS(o, dnsRecord_Local));
                    if (dnsRecord_Input == null) continue;

                    dnsRecord_Local.Type = dnsRecord_Input.Type;
                    dnsRecord_Local.Source = dnsRecord_Input.Source;
                    dnsRecord_Local.Target = dnsRecord_Input.Target;
                    dnsRecord_Local.TTL = dnsRecord_Input.TTL;
                    dnsRecord_Local.Priority = dnsRecord_Input.Priority;
                    dnsRecord_Local.Proxied = dnsRecord_Input.Proxied;
                    dnsRecord_Local.IPv4Only = dnsRecord_Input.IPv4Only;
                    dnsRecord_Local.IPv6Only = dnsRecord_Input.IPv6Only;
                    dnsRecord_Local.Remark = dnsRecord_Input.Remark;
                    dnsRecord_Local.Tags = dnsRecord_Input.Tags;

                    dnsRecords_Edt.Add(dnsRecord_Local);
                }

                #endregion

                if (dnsRecords_Add.IsNotEmpty())
                {
                    var handleResult_Add = await handler.DNSRecordAdd(domain.MapTo<Domain>(), dnsRecords_Add.Select(o => o as DNSRecord).ToArray());
                    if (!CheckResult(handleResult_Add, dsOpt, channel, settings, () => handleResult_Add.Data == 0)) return result;
                }

                if (dnsRecords_Del.IsNotEmpty())
                {
                    var handleResult_Del = await handler.DNSRecordDel(domain.MapTo<Domain>(), dnsRecords_Del.Select(o => o.MapTo<DNSRecord>()).ToArray());
                    if (!CheckResult(handleResult_Del, dsOpt, channel, settings, () => handleResult_Del.Data == 0)) return result;
                }

                if (dnsRecords_Edt.IsNotEmpty())
                {
                    var handleResult_Edt = await handler.DNSRecordEdt(domain.MapTo<Domain>(), dnsRecords_Edt.Select(o => o.MapTo<DNSRecord>()).ToArray());
                    if (!CheckResult(handleResult_Edt, dsOpt, channel, settings, () => handleResult_Edt.Data == 0)) return result;
                }

                Logger.Info($"{logTag} domain[{domain.Name}], add:[{dnsRecords_Add.Count}], del:[{dnsRecords_Del.Count}], edt:[{dnsRecords_Edt.Count}]");
                await repository.SaveAsync(Logger, msg, dsOpt, channel, domain, dnsRecords_Add, dnsRecords_Del, dnsRecords_Edt);
            }
        }
        catch (Exception ex)
        {
            Logger.Error($"{logTag} {ex.Message}", ex);
        }
        finally
        {
            Logger.Info($"{logTag} 结束");
        }

        return true;
    }
}