using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must be "yes", "on", 1, "1", true, or "true". This is useful for validating "Terms
///     of Service" acceptance or similar fields.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class AcceptedAttribute : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value is true or 1 or "yes" or "on" or "1" or "true";
    }
}
