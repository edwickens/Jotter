using Xunit;
using JotterService.Application.Services;
using JotterService.Application.Configuration;
using Microsoft.Extensions.Options;
using System;
using FluentAssertions;
using System.Text;
using System.Linq;
using System.Security.Cryptography;

namespace JotterService.Application.Tests.Services;

public class EncryptionServiceTests
{
    private readonly IOptions<EncryptionOptions> _encryptionOptions ;
    public EncryptionServiceTests()
    {
        _encryptionOptions = Options.Create(new EncryptionOptions() { Key = "SECRETSECRETSECRET/SECRETSECRETSECRETSECRET=" });
    }

    [Fact]
    public void Encrypt_ObscuresPlaintext()
    {
        // Arrange 
        var plaintext = "myDeepestDarkestSecret123!";

        var uut = new EncryptionService(_encryptionOptions);

        // Act 
        var result = uut.Encrypt(plaintext);
        // Assert
        result.Text.Should().NotBe(plaintext);
    }

    [Fact]
    public void Decrypt_RecoversPlaintext()
    {
        // Arrange 
        var plaintext = "myDeepestDarkestSecret123!";

        var uut = new EncryptionService(_encryptionOptions);
        var cypherText = uut.Encrypt(plaintext);

        // Act 
        var result = uut.Decrypt(cypherText);
        // Assert
        result.Should().Be(plaintext);
    }

    [Fact]
    public void Encrypt_UsesNewIv()
    {
        // Arrange 
        var plaintext = "myDeepestDarkestSecret123!";

        var uut = new EncryptionService(_encryptionOptions);

        // Act 
        var result1 = uut.Encrypt(plaintext);
        var result2 = uut.Encrypt(plaintext);
        // Assert
        result1.Iv.Should().NotBeEquivalentTo(result2.Iv);
    }

    [Fact]
    public void Decrypt_WithIncorrectKey_Fails()
    {
        // Arrange 
        var plaintext = "myDeepestDarkestSecret123!";
        var uut = new EncryptionService(_encryptionOptions);
        var cypherText = uut.Encrypt(plaintext);

        // new 256 bit (32 byte) key
        Aes aes = Aes.Create();
        _encryptionOptions.Value.Key = Convert.ToBase64String(aes.Key);

        // Act 
        Action act = () => uut.Decrypt(cypherText);
        // Assert
        act.Should().Throw<CryptographicException>();
    }

}
