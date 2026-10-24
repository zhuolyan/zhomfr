using System.Security.Cryptography;
using System.Text;

using Konscious.Security.Cryptography;

using Zhomfr.Hash.Abstractions;

namespace Zhomfr.Hash.Argon2;

/// <inheritdoc/>
public class Argon2Hash(Argon2Config config) : IHash
{
    /// <inheritdoc/>
    public string Make(string password)
    {
        byte[] salt = new byte[config.SaltSize];
        RandomNumberGenerator.Fill(salt);

        byte[] hash = this.ComputeArgon2(password,
                                         new()
                                         {
                                             Iterations          = config.Iterations,
                                             MemorySize          = config.MemorySize,
                                             DegreeOfParallelism = config.DegreeOfParallelism,
                                             Salt                = salt,
                                         });

        return new ParsedHash(config.Iterations, config.MemorySize, config.DegreeOfParallelism, salt, hash);
    }

    /// <inheritdoc/>
    public bool Check(string password, string storedHash)
    {
        ParsedHash parsedHash = new(storedHash);
        byte[]     newHash    = this.ComputeArgon2(password, parsedHash);

        return CryptographicOperations.FixedTimeEquals(parsedHash.Hash, newHash);
    }

    /// <inheritdoc/>
    public bool NeedsRehash(string storedHash)
    {
        ParsedHash parsedHash = new(storedHash);

        return parsedHash.Iterations          != config.Iterations
            || parsedHash.MemorySize          != config.MemorySize
            || parsedHash.DegreeOfParallelism != config.DegreeOfParallelism
            || parsedHash.Salt.Length         != config.SaltSize;
    }

    private byte[] ComputeArgon2(string password, ParsedHash hash)
    {
        Argon2id argon2 = new(Encoding.UTF8.GetBytes(password))
        {
            Salt                = hash.Salt,
            DegreeOfParallelism = hash.DegreeOfParallelism,
            Iterations          = hash.Iterations,
            MemorySize          = hash.MemorySize,
            KnownSecret         = Encoding.ASCII.GetBytes(config.Secret),
        };

        return argon2.GetBytes(config.MemorySize);
    }
}
