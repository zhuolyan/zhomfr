using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must be entirely Unicode alphanumeric characters, as well as ASCII dashes (-) and
///     ASCII underscores (_).
/// </summary>
/// <target-type>string</target-type>
/// <example>
///     <code>
///         public class AlphaDashDto
///         {
///             [AlphaDash]
///             public string StringInvalid = "abc-123_"; // ValidationError
///         
///             [AlphaDash]
///             public string StringValid = "abc 123"; // Success
///         }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class AlphaDashAttribute : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value is not string str ? throw new UnsupportedTypeException(value.GetType()) : str.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_');
    }
}
