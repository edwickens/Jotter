using JotterService.Application.Interfaces;
using JotterService.Application.Configuration;
using JotterService.Domain;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace JotterService.Application.Services;

public class EncryptionService : IEncryptionService
{
    private readonly EncryptionOptions _options;

    public EncryptionService(IOptions<EncryptionOptions> options)
    {
        _options = options.Value;
    }

    public CypherText Encrypt(string plaintext)
    {
        ArgumentNullException.ThrowIfNull(_options.Key);
        byte[] iv; 
        string encryptedSecret;
        using (Aes aes = Aes.Create())
        {
            iv = aes.IV;
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

        return new CypherText(encryptedSecret, iv);
    }

    public string Decrypt(CypherText cypherText)
    {
        ArgumentNullException.ThrowIfNull(_options.Key);
        string? plaintext = null; 
        using (Aes aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String(_options.Key);
            aes.IV = cypherText.Iv;
            byte[] cypher = Convert.FromBase64String(cypherText.Text);
            using MemoryStream encryptedSecretStream = new(cypher);

            using CryptoStream cryptoStream = new(
                encryptedSecretStream,
                aes.CreateDecryptor(),
                CryptoStreamMode.Read);
            using StreamReader encryptReader = new(cryptoStream);
            plaintext = encryptReader.ReadToEnd();
        }
        ArgumentNullException.ThrowIfNull(plaintext);

        return plaintext;
    }

}
