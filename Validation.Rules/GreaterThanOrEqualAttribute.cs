using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Helpers.ScalarTypes;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class GreaterThanOrEqualAttribute : AbstractRule
{
    private readonly string _minOrField;
    private          bool   _cached;

    /// <summary>
    ///     The field under validation must be greater than the given field or value. The two fields must be of the same
    ///     type. Strings, numerics, collections are evaluated using the same conventions as the size rule.
    /// </summary>
    /// <param name="minValueOrField">Field name or value.</param>
    public GreaterThanOrEqualAttribute(string minValueOrField)
    {
        this._minOrField = minValueOrField;
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        bool isField = AbstractRule.CheckIfFieldExists(this._minOrField, context);

        if (!this._cached) {
            this.MessageArgs.Add(isField ? this.GetDisplayNameForProperty(this._minOrField, context) ?? this._minOrField : this._minOrField);
            this._cached = true;
        }

        return value switch
               {
                   string str     => isField ? this.IsGtFieldValue(str, context) : str.Length >= int.Parse(this._minOrField, CultureInfo.InvariantCulture),
                   short number   => isField ? this.IsGtFieldValue(number, context) : number  >= short.Parse(this._minOrField, CultureInfo.InvariantCulture),
                   ushort number  => isField ? this.IsGtFieldValue(number, context) : number  >= ushort.Parse(this._minOrField, CultureInfo.InvariantCulture),
                   int number     => isField ? this.IsGtFieldValue(number, context) : number  >= int.Parse(this._minOrField, CultureInfo.InvariantCulture),
                   uint number    => isField ? this.IsGtFieldValue(number, context) : number  >= uint.Parse(this._minOrField, CultureInfo.InvariantCulture),
                   long number    => isField ? this.IsGtFieldValue(number, context) : number  >= long.Parse(this._minOrField, CultureInfo.InvariantCulture),
                   ulong number   => isField ? this.IsGtFieldValue(number, context) : number  >= ulong.Parse(this._minOrField, CultureInfo.InvariantCulture),
                   float number   => isField ? this.IsGtFieldValue(number, context) : number  >= float.Parse(this._minOrField, CultureInfo.InvariantCulture),
                   double number  => isField ? this.IsGtFieldValue(number, context) : number  >= double.Parse(this._minOrField, CultureInfo.InvariantCulture),
                   decimal number => isField ? this.IsGtFieldValue(number, context) : number  >= decimal.Parse(this._minOrField, CultureInfo.InvariantCulture),
                   DateTime date  => isField ? this.IsGtFieldValue(date, context) : date      >= DateTime.Parse(this._minOrField, CultureInfo.InvariantCulture),
                   DateTimeOffset date => isField
                                              ? this.IsGtFieldValue(date, context)
                                              : date >= DateTimeOffset.Parse(this._minOrField, CultureInfo.InvariantCulture),
                   ICollection collection => isField
                                                 ? this.IsGtFieldValue(collection, context)
                                                 : collection.Count >= int.Parse(this._minOrField, CultureInfo.InvariantCulture),
                   IEnumerable collection => isField
                                                 ? this.IsGtFieldValue(collection, context)
                                                 : collection.CountIsGreatThanOrEqual(int.Parse(this._minOrField, CultureInfo.InvariantCulture)),
                   var _ => throw new UnsupportedTypeException(value.GetType()),
               };
    }

    private bool IsGtFieldValue(object value, ValidationContext context)
    {
        object? anotherValue = AbstractRule.GetAnotherField<object>(this._minOrField, context);

        if (anotherValue == null) {
            return false;
        }

        AbstractRule.ThrowIfFieldsHasDifferentTypes(value, anotherValue);

        return value switch
               {
                   string str             => str.Length       >= ((string)anotherValue).Length,
                   ICollection collection => collection.Count >= ((ICollection)anotherValue).Count,
                   IEnumerable collection => collection.CountIsGreatThanOrEqual(((IEnumerable)anotherValue).Cast<object>().ToList().Count),
                   IComparable comparable => comparable.CompareTo(anotherValue) >= 0,
                   var _                  => throw new UnsupportedTypeException(value.GetType()),
               };
    }
}
