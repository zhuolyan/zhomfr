using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must have a size matching the given value. For string data, value corresponds to
///     the number of characters. For numeric data, value corresponds to a given integer value. For an collection, size
///     corresponds to the count of the collection.
/// </summary>
/// <param name="size"></param>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class SizeAttribute(int size) : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        throw new NotImplementedException();
    }
}
