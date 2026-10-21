using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>The given field must match the field under validation.</summary>
/// <example>
///     <code>
///         public class SameDto
///         {
///             [Same("ObjectInvalidSecond")]
///             public object ObjectInvalid = 2; // ValidationError
///         
///             public object ObjectInvalidSecond = 3;
///         
///             [Same("ObjectValidSecond")]
///             public object ObjectValid = "test"; // Success
///         
///             public object ObjectValidSecond = "test";
///         }
///         
///     </code>
/// </example>
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
        if (!this._cached) {
            this.MessageArgs.Add(AbstractRule.GetDisplayNameForProperty(this._propertyName, context));
            this._cached = true;
        }

        object? anotherValue = AbstractRule.GetFieldValue<object>(this._propertyName, context);

        if (anotherValue is null) {
            return false;
        }

        AbstractRule.ThrowIfFieldsHasDifferentTypes(value, anotherValue);

        return value.Equals(anotherValue);
    }
}
