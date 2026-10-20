using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must be "no", "off", 0, "0", false, or "false" if another field under validation is
///     equal to a specified value.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class DeclinedIfAttribute<T>(string anotherField, string fixedValue) : AbstractRule where T : IEquatable<T>, IParsable<T>
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        T? anotherValue = AbstractRule.GetAnotherField<T>(anotherField, context);
        T  expected     = T.Parse(fixedValue, CultureInfo.InvariantCulture);

        return !expected.Equals(anotherValue) || value is false or 0 or "no" or "off" or "0" or "false";
    }
}
