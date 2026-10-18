using System.Security.Cryptography;
using System.Text;
using AgencyManager.Core.Interfaces;

namespace AgencyManager.Infrastructure.Services;

public class SecurityService : ISecurityService
{
    // مفتاح تشفير ثابت افتراضي للنظام يمكن تخصيصه عبر الإعدادات
    private static readonly byte[] EncryptionKey = SHA256.HashData(Encoding.UTF8.GetBytes("Agency_SuperSecret_Master_Key_2026!#$@"));

    public string HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password)) return string.Empty;
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password + "_AgencySalt_2026"));
        return Convert.ToBase64String(bytes);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return HashPassword(password) == hashedPassword;
    }

    public string EncryptSecret(string plainText)
    {
        if (string.IsNullOrEmpty(plainText)) return string.Empty;

        using var aes = Aes.Create();
        aes.Key = EncryptionKey;
        aes.GenerateIV();

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream();
        ms.Write(aes.IV, 0, aes.IV.Length); // حفظ الـ IV في بداية البايتات

        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        using (var sw = new StreamWriter(cs, Encoding.UTF8))
        {
            sw.Write(plainText);
        }

        return Convert.ToBase64String(ms.ToArray());
    }

    public string DecryptSecret(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText)) return string.Empty;

        try
        {
            var fullCipher = Convert.FromBase64String(cipherText);
            using var aes = Aes.Create();
            aes.Key = EncryptionKey;

            var iv = new byte[aes.BlockSize / 8];
            Array.Copy(fullCipher, 0, iv, 0, iv.Length);
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(fullCipher, iv.Length, fullCipher.Length - iv.Length);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs, Encoding.UTF8);

            return sr.ReadToEnd();
        }
        catch
        {
            return "خطأ في فك التشفير";
        }
    }
}
