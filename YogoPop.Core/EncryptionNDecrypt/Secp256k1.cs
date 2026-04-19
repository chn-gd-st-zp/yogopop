namespace YogoPop.Core.EncryptionNDecrypt;

public class Secp256k1
{
    private readonly Key _privateKey;
    private readonly PubKey _publicKey;

    public Secp256k1()
    {
        _privateKey = new Key();
        _publicKey = _privateKey.PubKey;
    }

    public Secp256k1(string privateKeyHex)
    {
        _privateKey = new Key(Convert.FromHexString(privateKeyHex));
        _publicKey = _privateKey.PubKey;
    }

    public string PublicKey => _publicKey.ToHex();

    public string PrivateKey => _privateKey.ToHex();

    public string SignMessage(string message)
    {
        byte[] hash = DoubleSHA256(message);
        var signature = _privateKey.Sign(new uint256(hash));

        // 使用 ToDER() 获取签名的 DER 编码
        return Encoders.Hex.EncodeData(signature.ToDER());
    }

    public bool VerifySignature(string message, string signatureHex, string publicKeyHex)
    {
        byte[] hash = DoubleSHA256(message);
        var pubKey = new PubKey(publicKeyHex);

        // 解析签名（从十六进制转换为 DER 格式的签名）
        var signatureBytes = Encoders.Hex.DecodeData(signatureHex);

        return pubKey.Verify(new uint256(hash), signatureBytes);
    }

    private static byte[] DoubleSHA256(string message)
    {
        using var sha256 = SHA256.Create();
        byte[] hash1 = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(message));
        return sha256.ComputeHash(hash1);
    }
}