namespace DForge.Support;

public static class Deploy
{
    /// <summary>
    /// 启动参数 <br />
    /// docker run **** param1=value1 param2=value2 param3=value3
    /// </summary>
    /// <param name="configBuilder"></param>
    /// <param name="args"></param>
    public static void LoadDeploySettings(IConfigurationBuilder configBuilder, string[] args)
    {
        args = args.IsNotEmpty() ? args : new string[0];

        var merchantExclusive = args.Where(o => o.StartsWith("MerchantExclusive=", StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
        var merchantPaymentIP = args.Where(o => o.StartsWith("MerchantPaymentIP=", StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
        var merchantPaymentPort = args.Where(o => o.StartsWith("MerchantPaymentPort=", StringComparison.OrdinalIgnoreCase)).SingleOrDefault();

        merchantExclusive = merchantExclusive.IsNotEmptyString() ? merchantExclusive : "";
        merchantPaymentIP = merchantPaymentIP.IsNotEmptyString() ? merchantPaymentIP : "";
        merchantPaymentPort = merchantPaymentPort.IsNotEmptyString() ? merchantPaymentPort : "";

        merchantExclusive = merchantExclusive.Replace("ExclusiveMerchant=", string.Empty, StringComparison.OrdinalIgnoreCase);
        merchantPaymentIP = merchantPaymentIP.Replace("MerchantPaymentIP=", string.Empty, StringComparison.OrdinalIgnoreCase);
        merchantPaymentPort = merchantPaymentPort.Replace("MerchantPaymentPort=", string.Empty, StringComparison.OrdinalIgnoreCase);

        var source = new Dictionary<string, string>
        {
            ["PaymentServiceSettings:ExclusiveMerchant"] = merchantExclusive,
            ["PaymentServiceSettings:MerchantPaymentIP"] = merchantPaymentIP,
            ["PaymentServiceSettings:MerchantPaymentPort"] = merchantPaymentPort,
        };

        configBuilder.AddInMemoryCollection(source);
    }
}