using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be "no", "off", 0, "0", false, or "false".</summary>
/// <example>
///     <code>
///         public class DeclinedDto
///         {
///             [Declined]
///             public bool BoolInvalid = true; // ValidationError
///         
///             [Declined]
///             public bool BoolValid = false; // Success
///         
///             [Declined]
///             public int IntInvalid = 1; // ValidationError
///         
///             [Declined]
///             public int IntValid = 0; // Success
///         
///             [Declined]
///             public string StringInvalid = "something"; // ValidationError
///         
///             [Declined]
///             public string StringValid = "off"; // Success
///         }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class DeclinedAttribute : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value is false or 0 or "no" or "off" or "0" or "false";
    }
}
