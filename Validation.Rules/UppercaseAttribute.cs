using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be uppercase.</summary>
/// <target-type>string</target-type>
/// <example>
///     <code>
///         public class UppercaseDto
///         {
///             [Uppercase]
///             public string StringInvalid = "something"; // ValidationError
///         
///             [Uppercase]
///             public string StringValid = "SOMETHING"; // Success
///         }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class UppercaseAttribute : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value is not string str ? throw new UnsupportedTypeException(value.GetType()) : str.All(c => !char.IsLetter(c) || char.IsUpper(c));
    }
}
