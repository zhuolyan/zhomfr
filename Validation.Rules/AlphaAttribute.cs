using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be entirely Unicode alphabetic characters.</summary>
/// <target-type>string</target-type>
/// <example>
///     <code>
///         public class AlphaDto
///         {
///             [Alpha]
///             public string StringInvalid = "abc "; // ValidationError
///         
///             [Alpha]
///             public string StringValid = "abcABC"; // Success
///         }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class AlphaAttribute : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value is not string str ? throw new UnsupportedTypeException(value.GetType()) : str.All(char.IsLetter);
    }
}
