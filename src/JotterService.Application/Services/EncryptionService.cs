using JotterService.Application.Interfaces;
using JotterService.Application.Options;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace JotterService.Application.Services;

internal class EncryptionService : IEncryptionService
{
    private readonly EncryptionOptions _options;

    public EncryptionService(IOptions<EncryptionOptions> options)
    {
        _options = options.Value;
    }

    public string Encrypt(string plaintext)
    {
        ArgumentNullException.ThrowIfNull(_options.Key);

        string encryptedSecret;
        using (Aes aes = Aes.Create())
        {
            aes.GenerateIV();
            aes.Key = Convert.FromBase64String(_options.Key);
            using MemoryStream encryptedSecretStream = new();

            using CryptoStream cryptoStream = new(
                encryptedSecretStream,
                aes.CreateEncryptor(),
                CryptoStreamMode.Write);
            using (StreamWriter encryptWriter = new(cryptoStream))
            {
                encryptWriter.Write(plaintext);
            }
            byte[] encrypted = encryptedSecretStream.ToArray();
            encryptedSecret = Convert.ToBase64String(encrypted);
        }

        return encryptedSecret;
    }
}
