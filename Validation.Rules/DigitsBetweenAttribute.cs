using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class DigitsBetweenAttribute : AbstractRule
{
    /// <summary>The number under validation must have an exact length of value.</summary>
    /// <param name="size"></param>
    public DigitsBetweenAttribute(int size)
    {
    }

    /// <summary>The number under validation must have a length between the given min and max.</summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    public DigitsBetweenAttribute(int min, int max)
    {
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        throw new NotImplementedException();
    }
}
