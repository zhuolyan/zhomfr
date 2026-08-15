using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must be greater than the given field or value. The two fields must be of the same
///     type. Strings, numerics, arrays are evaluated using the same conventions as the size rule.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class GreaterThanAttribute : AbstractRule
{
    public GreaterThanAttribute(string anotherFiledName)
    {
    }

    public GreaterThanAttribute(int min)
    {
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        throw new NotImplementedException();
    }
}
