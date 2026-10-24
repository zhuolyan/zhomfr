namespace Zhomfr.Hash.Argon2;

internal class ParsedHash()
{
    public ParsedHash(string hash) : this()
    {
        string[] parts = [.. hash.Split('$').Where(c => !string.IsNullOrWhiteSpace(c))];

        if (parts.Length != 6) {
            throw new ArgumentException("Invalid hash format", nameof(hash));
        }

        this.Iterations          = int.Parse(parts[1]);
        this.MemorySize          = int.Parse(parts[2]);
        this.DegreeOfParallelism = int.Parse(parts[3]);
        this.Salt                = Convert.FromBase64String(parts[4]);
        this.Hash                = Convert.FromBase64String(parts[5]);
    }

    public ParsedHash(int iterations, int memorySize, int degreeOfParallelism, byte[] salt, byte[] hash) : this()
    {
        this.Iterations          = iterations;
        this.MemorySize          = memorySize;
        this.DegreeOfParallelism = degreeOfParallelism;
        this.Salt                = salt;
        this.Hash                = hash;
    }

    public int    Iterations          { get; init; }
    public int    MemorySize          { get; init; }
    public int    DegreeOfParallelism { get; init; }
    public byte[] Salt                { get; init; } = [];
    public byte[] Hash                { get; }       = [];

    public static implicit operator string(ParsedHash parsedHash)
    {
        return $"v1${
            parsedHash.Iterations
        }${
            parsedHash.MemorySize
        }${
            parsedHash.DegreeOfParallelism
        }${
            Convert.ToBase64String(parsedHash.Salt)
        }${
            Convert.ToBase64String(parsedHash.Hash)
        }";
    }
}
