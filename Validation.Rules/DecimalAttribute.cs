using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class DecimalAttribute : AbstractRule
{
    /// <summary>The field under validation must be decimal and must contain the specified number of decimal places.</summary>
    public DecimalAttribute(int size)
    {
    }

    /// <summary>The field under validation must be decimal and must have between the specified numbers of decimal places.</summary>
    public DecimalAttribute(int min, int max)
    {
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        throw new NotImplementedException();
    }
}
