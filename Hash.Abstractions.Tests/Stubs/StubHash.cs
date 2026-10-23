namespace Zhomfr.Hash.Abstractions.Tests.Stubs;

public class StubHash(StubConfig config) : IHash
{
    public StubConfig Config => config;

    /// <inheritdoc/>
    public string Make(string password)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public bool Check(string password, string storedHash)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public bool NeedsRehash(string storedHash)
    {
        throw new NotImplementedException();
    }
}
