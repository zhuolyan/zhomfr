using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must be greater than or equal the given field or value. The two fields must be of
///     the same type. Strings, numerics, arrays are evaluated using the same conventions as the size rule.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class GreaterThanOrEqualAttribute : AbstractRule
{
    public GreaterThanOrEqualAttribute(string anotherFiledName)
    {
    }

    public GreaterThanOrEqualAttribute(long min)
    {
    }
    public GreaterThanOrEqualAttribute(double min)
    {
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        throw new NotImplementedException();
    }
}
