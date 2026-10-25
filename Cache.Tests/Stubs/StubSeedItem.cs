namespace Zhomfr.Cache.Tests.Stubs;

public class StubSeedItem
{
    public required string    Key   { get; set; }
    public required object    Value { get; set; }
    public          TimeSpan? Ttl   { get; set; } = null;
}
