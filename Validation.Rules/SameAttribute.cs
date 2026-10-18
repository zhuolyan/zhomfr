using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class SameAttribute : AbstractRule
{
    private readonly string _propertyName;

    private bool _cached;

    /// <summary>The given field must match the field under validation.</summary>
    /// <param name="anotherField">Name of another field.</param>
    public SameAttribute(string anotherField)
    {
        this._propertyName = anotherField;
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        object? anotherValue = AbstractRule.GetAnotherField<object>(this._propertyName, context);

        if (!this._cached) {
            this.MessageArgs.Add(this.GetDisplayNameForProperty(this._propertyName, context) ?? this._propertyName);
            this._cached = true;
        }

        return value.Equals(anotherValue);
    }
}
