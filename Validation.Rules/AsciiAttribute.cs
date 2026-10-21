using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be entirely 7-bit ASCII characters.</summary>
/// <target-type>string</target-type>
/// <example>
///     <code>
///         public class AsciiDto
///         {
///             [Ascii]
///             public string StringInvalid = "abc123!@#"; // ValidationError
///         
///             [Ascii]
///             public string StringValid = "привіт"; // Success
///         }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class AsciiAttribute : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value is not string str ? throw new UnsupportedTypeException(value.GetType()) : str.All(char.IsAscii);
    }
}
