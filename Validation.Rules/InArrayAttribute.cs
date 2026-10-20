using System.Collections;
using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must exist in anotherField's values.</summary>
/// <param name="anotherField">field with collection for check value..</param>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class InArrayAttribute(string anotherField) : AbstractRule
{
    private bool _cached;

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        if (!this._cached) {
            this.MessageArgs.Add(AbstractRule.GetDisplayNameForProperty(anotherField, context));
            this._cached = true;
        }

        object? anotherValue = AbstractRule.GetFieldValue<object>(anotherField, context);

        if (anotherValue == null) {
            return false;
        }

        return anotherValue switch
               {
                   IDictionary dictionary => dictionary.Values.Cast<object>().Contains(value),
                   IEnumerable enumerable => enumerable.Cast<object>().Contains(value),
                   var _                  => throw new UnsupportedTypeException(anotherValue.GetType()),
               };
    }
}
