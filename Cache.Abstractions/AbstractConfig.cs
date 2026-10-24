namespace Zhomfr.Cache.Abstractions;

/// <summary>Abstract configuration object.</summary>
public abstract class AbstractConfig
{
    /// <summary>Cache prefix, needed then you use more than one app for one provider.</summary>
    public abstract string Prefix { get; init; }

    /// <summary>Default time to life for saved data (in minutes).</summary>
    public abstract int Ttl { get; init; }
}
