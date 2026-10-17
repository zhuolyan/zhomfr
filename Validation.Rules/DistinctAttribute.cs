using System.Collections;
using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>When validating collection, the field under validation must not have any duplicate values.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class DistinctAttribute : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value switch
               {
                   IDictionary dictionary => DistinctAttribute.IsDistinct(dictionary.Values),
                   IEnumerable enumerable => DistinctAttribute.IsDistinct(enumerable),
                   var _                  => throw new UnsupportedTypeException(),
               };
    }

    private static bool IsDistinct(IEnumerable value)
    {
        HashSet<object?> observedItems = [];

        foreach (object? element in value) {
            if (!observedItems.Add(element)) {
                return false;
            }
        }

        return true;
    }
}
