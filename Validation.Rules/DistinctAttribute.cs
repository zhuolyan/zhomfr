using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>When validating collection, the field under validation must not have any duplicate values.</summary>
/// <param name="ignoreCase">
///     You may add ignoreCase to the validation rule's arguments to make the rule ignore
///     capitalization differences when values is string.
/// </param>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class DistinctAttribute(bool ignoreCase = false) : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        throw new NotImplementedException();
    }
}
