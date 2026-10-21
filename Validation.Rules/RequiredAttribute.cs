using System.Collections;
using System.ComponentModel.DataAnnotations;

using Zhomfr.Helpers.ScalarTypes;
using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be present in the input data and not empty.</summary>
/// <remarks>
///     A field is "empty" if it meets one of the following criteria:
///     <list type="bullet">
///         <item>
///             <term>All</term><description>The value is null.</description>
///         </item>
///         <item>
///             <term>string</term><description>The value is an empty or only whitespace string.</description>
///         </item>
///         <item>
///             <term>Collections</term><description>The value is an empty collection or empty enumeration.</description>
///         </item>
///     </list>
/// </remarks>
/// <target-type>object</target-type>
/// <example>
///     <code>
///         public class RequiredDto
///         {
///             [Required]
///             public object? ObjectInvalid = null; // ValidationError
///         
///             [Required]
///             public object ObjectValid = new(); // Success
///         }
///         
///     </code>
/// </example>
public class RequiredAttribute : AbstractRule
{
    /// <inheritdoc/>
    protected override ValidationResult? IsValid(object? value, ValidationContext context)
    {
        return this.CheckCondition(value, context) ? ValidationResult.Success : new(this.FormatedMessage(context.DisplayName, value ?? string.Empty));
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object? value, ValidationContext context)
    {
        return value switch
               {
                   string str             => !string.IsNullOrWhiteSpace(str),
                   ICollection collection => collection.Count != 0,
                   IEnumerable collection => collection.CountIsGreatThan(0),
                   var _                  => value is not null,
               };
    }
}
