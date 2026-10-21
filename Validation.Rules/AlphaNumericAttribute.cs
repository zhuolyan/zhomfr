using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be entirely Unicode alphanumeric characters.</summary>
/// <target-type>string</target-type>
/// <example>
///     <code>
///         public class AlphaNumericDto
///         {
///             [AlphaNumeric]
///             public string StringInvalid = "abc123"; // ValidationError
///         
///             [AlphaNumeric]
///             public string StringValid = "abc-123"; // Success
///         }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class AlphaNumericAttribute : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value is not string str ? throw new UnsupportedTypeException(value.GetType()) : str.All(char.IsLetterOrDigit);
    }
}
