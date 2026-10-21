using System.ComponentModel.DataAnnotations;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must have a different value than field.</summary>
/// <param name="anotherField">Name of another field.</param>
/// <example>
///     <code>
///         public class DifferentDto
///         {
///             [Different("ObjectInvalidSecond")]
///             public object ObjectInvalid = 1; // ValidationError
///         
///             public object ObjectInvalidSecond = 2;
///         
///             [Different("ObjectValidSecond")]
///             public object ObjectValid = "test"; // Success
///         
///             public object ObjectValidSecond = "test";
///         }
///         
///     </code>
/// </example>
public class DifferentAttribute(string anotherField) : SameAttribute(anotherField)
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return !base.CheckCondition(value, context);
    }
}
