using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be lowercase.</summary>
/// <example>
///     <code>
///         public class LowercaseDto
///         {
///             [Lowercase]
///             public string StringInvalid = "sOmething"; // ValidationError
///         
///             [Lowercase]
///             public string StringValid = "something"; // Success
///         }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class LowercaseAttribute : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value is not string str ? throw new UnsupportedTypeException(value.GetType()) : str.All(c => !char.IsLetter(c) || char.IsLower(c));
    }
}
