namespace AgencyManager.Core.Interfaces;

public interface ISecurityService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
    string EncryptSecret(string plainText);
    string DecryptSecret(string cipherText);
}
