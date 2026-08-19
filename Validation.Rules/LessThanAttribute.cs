using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must be less than the given field. The two fields must be of the same type.
///     Strings, numerics, arrays, and files are evaluated using the same conventions as the size rule.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class LessThanAttribute : AbstractRule
{
    public LessThanAttribute(string anotherFiledName)
    {
    }

    public LessThanAttribute(int max)
    {
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        throw new NotImplementedException();
    }
}
