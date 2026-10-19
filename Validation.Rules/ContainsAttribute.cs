using System.Collections;
using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class ContainsAttribute : AbstractRule
{
    private readonly HashSet<object> _requiredItems;

    /// <summary>The field under validation must be a collection or string that contains all the given parameter values.</summary>
    /// <param name="items">The seek values.</param>
    /// <exception cref="ArgumentException">If items is empty.</exception>
    public ContainsAttribute(params object[] items)
    {
        if (items.Length == 0) {
            throw new ArgumentException($"{nameof(items)} is empty");
        }

        this._requiredItems = [.. items];
        this.AddListAsOneArgumentToMessageArgs(items);
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value switch
               {
                   string str             => this._requiredItems.All(e => str.Contains((string)e)),
                   IDictionary dictionary => this.IsContains(dictionary.Values),
                   IEnumerable enumerable => this.IsContains(enumerable),
                   var _                  => throw new UnsupportedTypeException(value.GetType()),
               };
    }

    protected bool IsContains(IEnumerable value)
    {
        foreach (object? element in value) {
            this._requiredItems.Remove(element!);

            if (this._requiredItems.Count == 0) {
                return true;
            }
        }

        return false;
    }
}
