using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must end with one of the given values.</summary>
/// <param name="values"></param>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class EndsWithAttribute(params string[] values) : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value is not string str ? throw new UnsupportedTypeException() : values.Any(str.EndsWith);
    }
}
