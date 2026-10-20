using System.ComponentModel.DataAnnotations;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must have a different value than field.</summary>
/// <param name="anotherField">Name of another field.</param>
public class DifferentAttribute(string anotherField) : SameAttribute(anotherField)
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return !base.CheckCondition(value, context);
    }
}
