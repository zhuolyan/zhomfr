namespace Zhomfr.Cache.Database;

/// <summary>Entity for store cache data in DB.</summary>
public class CacheItem
{
    /// <summary>Primary key.</summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>Cached data.</summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>Expiration datetime.</summary>
    public DateTimeOffset? ExpiredAt { get; set; }
}
