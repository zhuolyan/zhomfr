using System.Collections;
using System.ComponentModel.DataAnnotations;

using Zhomfr.Helpers.ScalarTypes;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class LessThanOrEqualAttribute : AbstractRule
{
    private readonly string _field;
    private          bool   _cached;

    /// <summary>
    ///     The field under validation must be less than or equal to the given field. The two fields must be of the same
    ///     type. Strings, numerics, arrays, and files are evaluated using the same conventions as the size rule.
    /// </summary>
    /// <param name="field">Field name.</param>
    public LessThanOrEqualAttribute(string field)
    {
        this._field = field;
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        object? anotherValue = AbstractRule.GetAnotherField<object>(this._field, context);

        if (anotherValue == null) {
            return false;
        }

        AbstractRule.ThrowIfFieldsHasDifferentTypes(value, anotherValue);

        if (!this._cached) {
            this.MessageArgs.Add(this.GetDisplayNameForProperty(this._field, context) ?? this._field);
            this._cached = true;
        }

        AbstractRule.ThrowIfFieldsHasDifferentTypes(value, anotherValue);

        return value switch
               {
                   string str             => str.Length       <= ((string)anotherValue).Length,
                   ICollection collection => collection.Count <= ((ICollection)anotherValue).Count,
                   IEnumerable collection => collection.CountIsLessThanOrEqual(((IEnumerable)anotherValue).Cast<object>().ToList().Count),
                   IComparable comparable => comparable.CompareTo(anotherValue) <= 0,
                   var _                  => throw new UnsupportedTypeException(value.GetType()),
               };
    }
}
