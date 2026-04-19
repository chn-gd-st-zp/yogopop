namespace DForge.DSP.CloudFlare;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(IDSSettings), DSPEnum.CloudFlare)]
public class DSSettings4CloudFlare : IDSOptAnalyseSetting
{
    public string Key { get; set; }

    public string Secret { get; set; }

    public string BaseUrl => "https://api.cloudflare.com/client/v4";

    /// <summary>
    /// Method: GET <br/>
    /// Route: /zones?name={0} <br/>
    /// Params-0: domain <br/>
    /// </summary>
    public string DomainQueryRoute => "/zones?name={0}";

    /// <summary>
    /// Method: GET <br/>
    /// Route: /zones <br/>
    /// </summary>
    public string DomainsQueryRoute => "/zones";

    /// <summary>
    /// Method: POST <br/>
    /// Route: /zones <br/>
    /// Params-0: domain_id <br/>
    /// </summary>
    public string DomainAddRoute => "/zones";
    public string DomainsAddRoute => "";

    /// <summary>
    /// Method: DELETE <br/>
    /// Route: /zones/{0} <br/>
    /// Params-0: domain_id <br/>
    /// </summary>
    public string DomainDelRoute => "/zones/{0}";
    public string DomainsDelRoute => "";

    /// <summary>
    /// Method: PATCH <br/>
    /// Route: /zones/{0} <br/>
    /// Params-0: domain_id <br/>
    /// </summary>
    public string DomainEdtRoute => "/zones/{0}";
    public string DomainsEdtRoute => "";

    /// <summary>
    /// Method: GET <br/>
    /// Route: /zones/{0}/dns_records/{1} <br/>
    /// Params-0: domain_id <br/>
    /// Params-1: dns_id <br/>
    /// </summary>
    public string DNSRecordQueryRoute => "/zones/{0}/dns_records/{1}";

    /// <summary>
    /// Method: GET <br/>
    /// Route: /zones/{0}/dns_records <br/>
    /// Params-0: domain_id <br/>
    /// </summary>
    public string DNSRecordsQueryRoute => "/zones/{0}/dns_records";

    /// <summary>
    /// Method: POST <br/>
    /// Route: /zones/{0}/dns_records <br/>
    /// Params-0: domain_id <br/>
    /// </summary>
    public string DNSRecordAddRoute => "/zones/{0}/dns_records";

    /// <summary>
    /// Method: DELETE <br/>
    /// Route: /zones/{0}/dns_records/{1} <br/>
    /// Params-0: domain_id <br/>
    /// Params-1: dns_id <br/>
    /// </summary>
    public string DNSRecordDelRoute => "/zones/{0}/dns_records/{1}";

    /// <summary>
    /// Method: PATCH <br/>
    /// Route: /zones/{0}/dns_records/{1} <br/>
    /// Params-0: domain_id <br/>
    /// Params-1: dns_id <br/>
    /// </summary>
    public string DNSRecordEdtRoute => "/zones/{0}/dns_records/{1}";
}