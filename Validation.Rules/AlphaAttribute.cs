using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must be entirely Unicode alphabetic characters. To restrict this validation rule to
///     characters in the ASCII range (a-z and A-Z), you may provide the ascii option to the validation rule.
/// </summary>
/// <param name="ascii"></param>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class AlphaAttribute(bool ascii = false) : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        throw new NotImplementedException();
    }
}
