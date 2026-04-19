namespace DForge.DSP.GoDaddy;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(IDSSettings), DSPEnum.GoDaddy)]
public class DSSettings4GoDaddy : IDSOptRegistSetting
{
    public string Key { get; set; }

    public string Secret { get; set; }

    //public string BaseUrl => AppInitHelper.Environment == YogoPop.Core.CusEnum.EnvironmentEnum.PROD ? "https://api.godaddy.com" : "https://api.ote-godaddy.com";
    public string BaseUrl => "https://api.godaddy.com";

    /// <summary>
    /// Method: GET <br/>
    /// Route: /v1/domains/{0} <br/>
    /// Params-0: domain <br/>
    /// </summary>
    public string DomainQueryRoute => "/v1/domains/{0}";

    /// <summary>
    /// Method: GET <br/>
    /// Route: /v1/domains <br/>
    /// </summary>
    public string DomainsQueryRoute => "/v1/domains";

    /// <summary>
    /// Method: PATCH <br/>
    /// Route: /v1/domains/{0} <br/>
    /// Params-0: domain <br/>
    /// </summary>
    public string DomainNSModifyRoute => "/v1/domains/{0}";
    public string DomainNSsModifyRoute => "";
}