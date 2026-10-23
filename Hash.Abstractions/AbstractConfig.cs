namespace Zhomfr.Hash.Abstractions;

/// <summary>Abstract configuration.</summary>
public abstract class AbstractConfig
{
    /// <summary>Secret app string.</summary>
    public abstract string Secret { get; set; }
}
