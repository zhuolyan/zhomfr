using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must be "yes", "on", 1, "1", true, or "true". This is useful for validating "Terms
///     of Service" acceptance or similar fields.
/// </summary>
/// <target-type>bool,string,int</target-type>
/// <example>
///     <code>
///         public class AcceptedDto
///         {
///             [Accepted]
///             public bool BoolInvalid = false; // ValidationError
///         
///             [Accepted]
///             public bool BoolValid = true; // Success
///         
///             [Accepted]
///             public int IntInvalid = 0; // ValidationError
///         
///             [Accepted]
///             public int IntValid = 1; // Success
///         
///             [Accepted]
///             public string StringInvalid = "something"; // ValidationError
///         
///             [Accepted]
///             public string StringValid = "yes"; // Success
///         }
///         
///     </code>
/// </example>
/// <target-type>bool,string,int</target-type>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class AcceptedAttribute : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value is true or 1 or "yes" or "on" or "1" or "true";
    }
}
