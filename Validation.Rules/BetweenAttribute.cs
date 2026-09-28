using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must have a size between the given min and max (inclusive). Strings, numerics,
///     arrays, and files are evaluated in the same fashion as the size rule.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class BetweenAttribute : AbstractRule
{
    public BetweenAttribute(string min, string max)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        throw new NotImplementedException();
    }
}
