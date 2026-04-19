namespace YogoPop.Core.EncryptionNDecrypt;

public static class GoogleMFA
{
    private static TwoFactorAuthenticator _factor = new TwoFactorAuthenticator();
    private static bool _isBase32 = true;

    /// <summary>
    /// 产生密匙 <br/>
    /// 生成并保存到服务器的“共享密钥”（建议随机 16–32 字节后做 Base32 表示）
    /// </summary>
    /// <returns></returns>
    public static string GenerateSecret() => Unique.GetRandomCode2(16).ToUpper();

    /// <summary>
    /// 生成二维码
    /// </summary>
    /// <param name="secret">密匙-base32</param>
    /// <param name="accoutInfo">账号信息</param>
    /// <param name="systemInfo">系统信息</param>
    /// <param name="qrCodeSize">二维码尺寸</param>
    /// <returns>图片base64</returns>
    public static string GenerateQRCode(string secret, string accoutInfo, string systemInfo = default, int qrCodeSize = 8)
    {
        var result = string.Empty;

        try
        {
            if (accoutInfo.IsEmptyString()) return result;

            systemInfo = systemInfo.IsNotEmptyString() ? systemInfo : string.Empty;

            result = _factor.GenerateSetupCode(systemInfo, accoutInfo, secret, _isBase32, qrCodeSize).QrCodeSetupImageUrl;
        }
        catch (Exception ex)
        {
            //
        }

        return result;
    }

    /// <summary>
    /// 验证
    /// </summary>
    /// <param name="secret">密匙-base32</param>
    /// <param name="input">输入的验证码</param>
    /// <param name="leranceSecond">容差秒数</param>
    /// <returns></returns>
    public static bool Verify(string secret, string input, int leranceSecond = 10)
    {
        var result = false;

        try
        {
            result = _factor.ValidateTwoFactorPIN(secret, input, TimeSpan.FromSeconds(leranceSecond), _isBase32);
        }
        catch (Exception ex)
        {
            //
        }

        return result;
    }
}