using System.Collections;
using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be a collection that contains all the given parameter values.</summary>
/// <param name="items"></param>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class ContainsAttribute(params object[] items) : AbstractRule
{
    private readonly HashSet<object> _requiredItems = [.. items];

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value switch
               {
                   string str             => items.All(e => str.Contains((string)e)),
                   IDictionary dictionary => this.IsContains(dictionary.Values),
                   IEnumerable enumerable => this.IsContains(enumerable),
                   var _                  => throw new UnsupportedTypeException(),
               };
    }

    protected bool IsContains(IEnumerable value)
    {
        if (this._requiredItems.Count == 0) {
            return true;
        }

        foreach (object? element in value) {
            this._requiredItems.Remove(element!);

            if (this._requiredItems.Count == 0) {
                return true;
            }
        }

        return false;
    }
}
