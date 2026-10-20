using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must be "yes", "on", 1, "1", true, or "true" if another field under validation is
///     equal to a specified value. This is useful for validating "Terms of Service" acceptance or similar fields.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class AcceptedIfAttribute<T>(string anotherField, string fixedValue) : AbstractRule where T : IEquatable<T>, IParsable<T>
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        T? anotherValue = AbstractRule.GetFieldValue<T>(anotherField, context);
        T  expected     = T.Parse(fixedValue, CultureInfo.InvariantCulture);

        return !expected.Equals(anotherValue) || value is "yes" or "on" or "1" or "true" or 1 or true;
    }
}
