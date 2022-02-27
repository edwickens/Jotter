using JotterService.Domain;
using static JotterService.Application.Services.EncryptionService;

namespace JotterService.Application.Interfaces;

public interface IEncryptionService
{
    public CypherText Encrypt(string plaintext);
    public string Decrypt(CypherText cypherText);
}


