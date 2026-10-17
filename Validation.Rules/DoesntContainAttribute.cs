using System.Collections;
using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be a collection that does not contain any of the given parameter values.</summary>
/// <param name="items"></param>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class DoesntContainAttribute(params string[] items) : AbstractRule
{
    private readonly HashSet<object> _forbiddenItems = [.. items];

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value switch
               {
                   string str             => !items.Any(str.Contains),
                   IDictionary dictionary => this.IsDoesntContain(dictionary.Values),
                   IEnumerable enumerable => this.IsDoesntContain(enumerable),
                   var _                  => throw new UnsupportedTypeException(),
               };
    }

    protected bool IsDoesntContain(IEnumerable value)
    {
        foreach (object? element in value) {
            if (this._forbiddenItems.Contains(element!)) {
                return false;
            }
        }

        return true;
    }
}
