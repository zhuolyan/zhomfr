using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Abstractions;

/// <inheritdoc/>
public abstract class AbstractRule : ValidationAttribute
{
    /// <summary>Arguments for error message.</summary>
    protected List<object> MessageArgs { get; } = [];

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

    /// <summary>Check if validated fields has same types.</summary>
    /// <param name="value">Current value.</param>
    /// <param name="anotherValue">Value of another field.</param>
    /// <exception cref="ValuesHasDifferentTypesException">If fields has different types/</exception>
    protected static void ThrowIfFieldsHasDifferentTypes(object value, object anotherValue)
    {
        if (value.GetType() != anotherValue.GetType()) {
            throw new ValuesHasDifferentTypesException();
        }
    }

    /// <summary>Get field value by name.</summary>
    /// <param name="fieldName">Field name.</param>
    /// <param name="context">Validation context.</param>
    /// <typeparam name="T">Expected type of value.</typeparam>
    /// <returns>Value of field.</returns>
    /// <exception cref="MissingFieldException">Then field or property not exists in type.</exception>
    protected static T? GetFieldValue<T>(string fieldName, ValidationContext context)
    {
        PropertyInfo? propertyInfo = context.ObjectType.GetProperty(fieldName);

        if (propertyInfo is not null) {
            return (T?)propertyInfo.GetValue(context.ObjectInstance);
        }

        FieldInfo? fieldInfo = context.ObjectType.GetField(fieldName);

        if (fieldInfo is not null) {
            return (T?)fieldInfo.GetValue(context.ObjectInstance);
        }

        throw new MissingFieldException($"The field {fieldName} not present in validated object.");
    }

    /// <summary>Return display name by field name.</summary>
    /// <param name="fieldName">Name of field.</param>
    /// <param name="context">Validation context.</param>
    /// <returns>Return display name of field.</returns>
    /// <exception cref="MissingFieldException">Then field or property not exists in type.</exception>
    protected static string GetDisplayNameForProperty(string fieldName, ValidationContext context)
    {
        PropertyInfo? propertyInfo = context.ObjectType.GetProperty(fieldName);

        if (propertyInfo is not null) {
            return AbstractRule.GetDisplayNameForProperty(propertyInfo);
        }

        FieldInfo? fieldInfo = context.ObjectType.GetField(fieldName);

        return fieldInfo != null
                   ? AbstractRule.GetDisplayNameForProperty(fieldInfo)
                   : throw new MissingFieldException($"The field {fieldName} not present in validated object.");
    }

    private static string GetDisplayNameForProperty(MemberInfo property)
    {
        IEnumerable<Attribute> attributes = CustomAttributeExtensions.GetCustomAttributes(property, true);

        foreach (Attribute attribute in attributes) {
            if (attribute is DisplayAttribute display) {
                return display.GetName() ?? property.Name;
            }
        }

        return property.Name;
    }

    /// <summary>Add attribute argument to error message argument like one argument.</summary>
    /// <param name="args">Arguments what need add.</param>
    protected void AddListAsOneArgumentToMessageArgs(object[] args)
    {
        this.MessageArgs.Add(string.Join(", ", args.Select(i => i is string str ? $"\"{str}\"" : i)));
    }
}
