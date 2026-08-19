using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Zhomfr.Validation.Rules.Abstractions;

/// <inheritdoc/>
public abstract class AbstractRule : ValidationAttribute
{
    /// <summary>Arguments for error message.</summary>
    protected List<IFormattable> MessageArgs { get; } = [];

    /// <summary>Checks the validation condition for the specified value.</summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="context">The validation context.</param>
    /// <returns>True if the condition is met; otherwise, false.</returns>
    /// <exception cref="UnsupportedTypeException">Thrown when the rule cannot process the data type.</exception>
    /// <exception cref="Exception">Any other exceptions thrown by the logic.</exception>
    protected abstract bool CheckCondition(object value, ValidationContext context);

    /// <summary>Formats the error message using the display name, message arguments, and value.</summary>
    /// <param name="displayName">The display name of the field being validated.</param>
    /// <param name="value">The value that failed validation.</param>
    /// <returns>The formatted error message string.</returns>
    protected virtual string FormatedMessage(string displayName, object value)
    {
        object?[] arg = value is IEnumerable enumerable ? [displayName, .. this.MessageArgs, .. enumerable] : [displayName, .. this.MessageArgs, value];

        return string.Format(CultureInfo.CurrentCulture, this.ErrorMessageString, arg);
    }

    /// <inheritdoc/>
    /// <exception cref="UnsupportedTypeException">Thrown when the rule cannot process the data type.</exception>
    /// <exception cref="Exception">Any other exceptions thrown by the logic.</exception>
    protected override ValidationResult? IsValid(object? value, ValidationContext context)
    {
        if (value == null || value is string str && string.IsNullOrWhiteSpace(str)) {
            return ValidationResult.Success;
        }

        bool result = this.CheckCondition(value, context);

        return result ? ValidationResult.Success : new(this.FormatedMessage(context.DisplayName, value));
    }
}
