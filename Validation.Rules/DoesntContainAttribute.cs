using System.ComponentModel.DataAnnotations;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must be a collection or string that does not contain any of the given parameter
///     values.
/// </summary>
/// <param name="values">The seek values.</param>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class DoesntContainAttribute(params object[] values) : ContainsAttribute(values)
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return !base.CheckCondition(value, context);
    }
}
