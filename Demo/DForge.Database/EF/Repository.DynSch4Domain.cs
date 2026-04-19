namespace DForge.Database.EF;

[DIModeForService(DIModeEnum.Exclusive, typeof(IDSPRepository))]
public class DSPRepository : RenewEFDBRepository, IDSPRepository
{
    public async Task<IEnumerable<IDNSRecord>> DNSRecords(IDomain domain) => await DBContext.ListAsync<TBAppDNSRecord>(o => o.DomainID == ((TBAppDomain)domain).PrimaryKey);

    public async Task SaveAsync<TLoggerTrigger>(IYogoLogger<TLoggerTrigger> logger, DynSchMQMsg msg, DSOptEnum dsOpt, IChannel ichannel, IEnumerable<IDomain> idomains, IEnumerable<IDNSRecord> idnsRecords)
        where TLoggerTrigger : class
    {
        if (logger == null) return;
        if (msg == null) { logger.Error("msg empty"); return; }
        if (msg.Record == null) { logger.Error("msg.record empty"); return; }
        if (msg.Record is not TBAppDynSchRecord) { logger.Error("msg.record wrong type"); return; }
        if (msg.ExtraData == null) { logger.Error("msg.extradata empty"); return; }
        if (msg.ExtraData is not DSPDynSchMQMsgExtraData) { logger.Error("msg.extradata wrong type"); return; }
        if (dsOpt == DSOptEnum.None) { logger.Error("dsOpt wrong type"); return; }
        if (ichannel == null) { logger.Error("ichannel empty"); return; }
        if (ichannel is not TBAppDSPChannel) { logger.Error("ichannel wrong type"); return; }
        if (idomains.IsEmpty() && idnsRecords.IsEmpty()) return;

        var record = msg.Record as TBAppDynSchRecord;
        var channel = ichannel as TBAppDSPChannel;

        var domainList_db = await DBContext.ListAsync<TBAppDomain>(o => idomains.Select(oo => oo.Name).Contains(o.Name));
        var dnsRecordList_db = await DBContext.ListAsync<TBAppDNSRecord>(o => idnsRecords.Select(oo => oo.Source).Contains(o.Source));

        var domain_db_all_list = new List<TBAppDomain>();
        var domain_db_add_list = new List<TBAppDomain>();
        var domain_db_del_list = new List<TBAppDomain>();
        var domain_db_edt_list = new List<TBAppDomain>();
        var dnsRecord_db_add_list = new List<TBAppDNSRecord>();
        var dnsRecord_db_del_list = new List<TBAppDNSRecord>();
        var dnsRecord_db_edt_list = new List<TBAppDNSRecord>();

        foreach (var domain in idomains)
        {
            var obj = domainList_db.SingleOrDefault(o => o.Name.IsEquals(domain.Name));
            if (obj != null) continue;

            obj = domain.MapTo<TBAppDomain>();
            obj.ProjectID = channel.ProjectID;
            obj.Status = StatusEnum.Normal;
            obj.RegistChannelID = dsOpt != DSOptEnum.Regist ? string.Empty : channel.PrimaryKey;
            obj.AnalyseChannelID = dsOpt != DSOptEnum.Analyse ? string.Empty : channel.PrimaryKey;
            domain_db_add_list.Add(obj);
        }

        foreach (var domain in domainList_db)
        {
            var obj = idomains.SingleOrDefault(o => o.Name.IsEquals(domain.Name));
            if (obj != null) continue;

            domain.Status = StatusEnum.Delete;

            domain_db_del_list.Add(domain);
        }

        foreach (var domain in idomains)
        {
            var obj = domainList_db.SingleOrDefault(o => o.Name.IsEquals(domain.Name));
            if (obj == null) continue;

            obj.ProjectID = channel.ProjectID;
            obj.NameServers = dsOpt != DSOptEnum.Regist ? obj.NameServers : (domain.NameServers.IsEmptyString() ? obj.NameServers : domain.NameServers);
            obj.RegistChannelID = dsOpt != DSOptEnum.Regist ? obj.RegistChannelID : channel.PrimaryKey;
            obj.RegistSrcID = dsOpt != DSOptEnum.Regist ? obj.RegistSrcID : domain.RegistSrcID;
            obj.RegistSrcStatus = dsOpt != DSOptEnum.Regist ? obj.RegistSrcStatus : domain.RegistSrcStatus;
            obj.AnalyseChannelID = dsOpt != DSOptEnum.Analyse ? obj.AnalyseChannelID : channel.PrimaryKey;
            obj.AnalyseSrcID = dsOpt != DSOptEnum.Analyse ? obj.AnalyseSrcID : domain.AnalyseSrcID;
            obj.AnalyseSrcStatus = dsOpt != DSOptEnum.Analyse ? obj.AnalyseSrcStatus : domain.AnalyseSrcStatus;
            obj.MainStatus = domain.MainStatus;
            obj.SubStatus = domain.SubStatus;
            obj.Status = obj.MainStatus == DMainStatusEnum.Normal ? StatusEnum.Normal : StatusEnum.Disable;

            if (
                (!obj.CreateTime.HasValue && domain.CreateTime.HasValue)
                ||
                (obj.CreateTime.HasValue && domain.CreateTime.HasValue && obj.CreateTime.Value > domain.CreateTime.Value)
            )
            {
                obj.CreateTime = domain.CreateTime;
            }

            if (
                (!obj.ExpiredTime.HasValue && domain.ExpiredTime.HasValue)
                ||
                (obj.ExpiredTime.HasValue && domain.ExpiredTime.HasValue && obj.ExpiredTime.Value < domain.ExpiredTime.Value)
            )
            {
                obj.ExpiredTime = domain.ExpiredTime;
            }

            domain_db_edt_list.Add(obj);
        }

        domain_db_all_list.AddRange(domain_db_add_list);
        domain_db_all_list.AddRange(domain_db_edt_list);

        foreach (var dNSRecord in idnsRecords)
        {
            var domain = domain_db_all_list.SingleOrDefault(o => o.Name.IsSameRootDomain(dNSRecord.Source));
            if (domain == null) continue;

            var obj = dnsRecordList_db.SingleOrDefault(o => o.Source.IsEquals(dNSRecord.Source));
            if (obj != null) continue;

            obj = dNSRecord.MapTo<TBAppDNSRecord>();
            obj.ProjectID = domain.ProjectID;
            obj.DomainID = domain.PrimaryKey;
            dnsRecord_db_add_list.Add(obj);
        }

        foreach (var dNSRecord in dnsRecordList_db)
        {
            var obj = idnsRecords.SingleOrDefault(o => o.Source.IsEquals(dNSRecord.Source));
            if (obj != null) continue;

            dnsRecord_db_del_list.Add(dNSRecord);
        }

        foreach (var dNSRecord in idnsRecords)
        {
            var domain = domain_db_all_list.SingleOrDefault(o => o.Name.IsSameRootDomain(dNSRecord.Source));
            if (domain == null) continue;

            var obj = dnsRecordList_db.SingleOrDefault(o => o.Source.IsEquals(dNSRecord.Source));
            if (obj == null) continue;

            obj = dNSRecord.AdaptTo<TBAppDNSRecord>();
            obj.ProjectID = channel.ProjectID;
            obj.DomainID = domain.PrimaryKey;

            dnsRecord_db_edt_list.Add(obj);
        }

        if (domain_db_add_list.IsNotEmpty())
        {
            domain_db_add_list.ForEach(o => o.Name = o.Name.ToLower());
            await DBContext.CreateAsync(domain_db_add_list);
        }

        if (domain_db_del_list.IsNotEmpty())
        {
            domain_db_del_list.ForEach(o => o.Name = o.Name.ToLower());
            await DBContext.UpdateAsync(domain_db_del_list);
        }

        if (domain_db_edt_list.IsNotEmpty())
        {
            domain_db_edt_list.ForEach(o => o.Name = o.Name.ToLower());
            await DBContext.UpdateAsync(domain_db_edt_list);
        }

        if (dnsRecord_db_add_list.IsNotEmpty())
        {
            dnsRecord_db_add_list.ForEach(o => o.Source = o.Source.ToLower());
            await DBContext.CreateAsync(dnsRecord_db_add_list);
        }

        if (dnsRecord_db_del_list.IsNotEmpty())
        {
            dnsRecord_db_del_list.ForEach(o => o.Source = o.Source.ToLower());
            await DBContext.DeleteAsync(dnsRecord_db_del_list);
        }

        if (dnsRecord_db_edt_list.IsNotEmpty())
        {
            dnsRecord_db_edt_list.ForEach(o => o.Source = o.Source.ToLower());
            await DBContext.UpdateAsync(dnsRecord_db_edt_list);
        }

        record = await DBContext.SingleAsync<TBAppDynSchRecord>(o => o.PrimaryKey == record.PrimaryKey);
        record.EndAt = DateTime.Now;
        record.IsSuccess = true;
        await DBContext.UpdateAsync(record);
    }

    public async Task SaveAsync<TLoggerTrigger>(IYogoLogger<TLoggerTrigger> logger, DynSchMQMsg msg, DSOptEnum dsOpt, IChannel ichannel, IEnumerable<IDomain> idomains)
        where TLoggerTrigger : class
    {
        if (logger == null) return;
        if (msg == null) { logger.Error("msg empty"); return; }
        if (msg.Record == null) { logger.Error("msg.record empty"); return; }
        if (msg.Record is not TBAppDynSchRecord) { logger.Error("msg.record wrong type"); return; }
        if (msg.ExtraData == null) { logger.Error("msg.extradata empty"); return; }
        if (msg.ExtraData is not DSPDynSchMQMsgExtraData) { logger.Error("msg.extradata wrong type"); return; }
        if (dsOpt != DSOptEnum.Regist) { logger.Error("dsOpt wrong type"); return; }
        if (ichannel == null) { logger.Error("ichannel empty"); return; }
        if (ichannel is not TBAppDSPChannel) { logger.Error("ichannel wrong type"); return; }
        if (idomains.IsEmpty()) return;

        var record = msg.Record as TBAppDynSchRecord;

        var domain_db_list = await DBContext.ListAsync<TBAppDomain>(o => idomains.Select(oo => oo.Name).Contains(o.Name));

        var domain_db_edt_List = new List<TBAppDomain>();

        foreach (var domain in domain_db_list)
        {
            var obj = idomains.SingleOrDefault(o => o.Name.IsEquals(domain.Name));
            if (obj == null) continue;

            domain.NameServers = obj.NameServers.ToLower();

            domain_db_edt_List.Add(domain);
        }

        if (domain_db_edt_List.IsNotEmpty())
            await DBContext.UpdateAsync(domain_db_edt_List);

        record = await DBContext.SingleAsync<TBAppDynSchRecord>(o => o.PrimaryKey == record.PrimaryKey);
        record.EndAt = DateTime.Now;
        record.IsSuccess = true;
        await DBContext.UpdateAsync(record);
    }

    public async Task SaveAsync<TLoggerTrigger>(IYogoLogger<TLoggerTrigger> logger, DynSchMQMsg msg, DSOptEnum dsOpt, IChannel ichannel, IDomain idomain,
        IEnumerable<IDNSRecord> dnsRecords_A, IEnumerable<IDNSRecord> dnsRecords_D, IEnumerable<IDNSRecord> dnsRecords_E)
        where TLoggerTrigger : class
    {
        if (logger == null) return;
        if (msg == null) { logger.Error("msg empty"); return; }
        if (msg.Record == null) { logger.Error("msg.record empty"); return; }
        if (msg.Record is not TBAppDynSchRecord) { logger.Error("msg.record wrong type"); return; }
        if (msg.ExtraData == null) { logger.Error("msg.extradata empty"); return; }
        if (msg.ExtraData is not DSPDynSchMQMsgExtraData) { logger.Error("msg.extradata wrong type"); return; }
        if (dsOpt != DSOptEnum.Analyse) { logger.Error("dsOpt wrong type"); return; }
        if (ichannel == null) { logger.Error("ichannel empty"); return; }
        if (ichannel is not TBAppDSPChannel) { logger.Error("ichannel wrong type"); return; }
        if (idomain == null) { logger.Error("ichannel empty"); return; }

        var record = msg.Record as TBAppDynSchRecord;
        var channel = ichannel as TBAppDSPChannel;
        var domain = idomain.MapTo<TBAppDomain>();
        var dnsRecords_Add = dnsRecords_A.Select(o => o.MapTo<TBAppDNSRecord>()).ToList();
        var dnsRecords_Del = dnsRecords_D.Select(o => o as TBAppDNSRecord).ToList();
        var dnsRecords_Edt = dnsRecords_E.Select(o => o as TBAppDNSRecord).ToList();

        domain.AnalyseChannelID = channel.PrimaryKey;
        dnsRecords_Add.ForEach(o => { o.ProjectID = domain.ProjectID; o.DomainID = domain.PrimaryKey; });

        using (var tranScope = UnitOfWork.GenerateTransactionScope())
        {
            if (!DBContext.Update(domain, false)) return;
            if (dnsRecords_Add.IsNotEmpty() && !DBContext.Create(dnsRecords_Add, false)) return;
            if (dnsRecords_Del.IsNotEmpty() && !DBContext.Delete(dnsRecords_Del, false)) return;
            if (dnsRecords_Edt.IsNotEmpty() && !DBContext.Update(dnsRecords_Edt, false)) return;

            DBContext.SaveChanges();
            tranScope.Complete();
        }

        record = await DBContext.SingleAsync<TBAppDynSchRecord>(o => o.PrimaryKey == record.PrimaryKey);
        record.EndAt = DateTime.Now;
        record.IsSuccess = true;
        await DBContext.UpdateAsync(record);
    }
}