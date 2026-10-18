using System.IO;
using System.Security.Cryptography;
using System.Text;
using AgencyManager.Core.Interfaces;

namespace AgencyManager.Infrastructure.Services;

public class AdminPinService
{
    private readonly ISecurityService _security;
    private static readonly string SecurePinFilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "AgencyManager",
        "master_security.key"
    );

    // الرمز السري الافتراضي للمرة الأولى: 123456
    private const string DefaultPin = "123456";

    public bool IsLocked { get; private set; } = true;

    public AdminPinService(ISecurityService security)
    {
        _security = security;
        EnsurePinFileExists();
    }

    private void EnsurePinFileExists()
    {
        try
        {
            var dir = Path.GetDirectoryName(SecurePinFilePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (!File.Exists(SecurePinFilePath))
            {
                // حفظ الرمز الافتراضي مشفراً
                SaveEncryptedPin(DefaultPin);
            }
        }
        catch
        {
            // Fallback to local app directory
        }
    }

    public string GetStoredPin()
    {
        try
        {
            if (File.Exists(SecurePinFilePath))
            {
                var encrypted = File.ReadAllText(SecurePinFilePath);
                var decrypted = _security.DecryptSecret(encrypted);
                if (!string.IsNullOrWhiteSpace(decrypted) && decrypted.Length == 6)
                {
                    return decrypted;
                }
            }
        }
        catch
        {
            // fallback
        }

        return DefaultPin;
    }

    private void SaveEncryptedPin(string pin)
    {
        var encrypted = _security.EncryptSecret(pin);
        File.WriteAllText(SecurePinFilePath, encrypted);
    }

    public static string NormalizeDigits(string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        var chars = input.Select(c =>
            c >= '٠' && c <= '٩' ? (char)('0' + (c - '٠')) :
            c >= '۰' && c <= '۹' ? (char)('0' + (c - '۰')) : c
        ).Where(char.IsDigit).ToArray();
        return new string(chars);
    }

    public bool VerifyAndUnlock(string enteredPin)
    {
        var normalized = NormalizeDigits(enteredPin);
        if (normalized.Length != 6) return false;

        var stored = GetStoredPin();
        if (normalized == stored)
        {
            IsLocked = false;
            return true;
        }

        return false;
    }

    public bool ChangePin(string oldPin, string newPin)
    {
        var normalizedOld = NormalizeDigits(oldPin);
        var normalizedNew = NormalizeDigits(newPin);

        if (normalizedNew.Length != 6)
        {
            return false;
        }

        var stored = GetStoredPin();
        if (normalizedOld == stored)
        {
            SaveEncryptedPin(normalizedNew);
            return true;
        }

        return false;
    }

    public void Lock()
    {
        IsLocked = true;
    }

    public void UnlockDirectly()
    {
        IsLocked = false;
    }
}
