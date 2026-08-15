using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be a value preceding or equal to the given date.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class BeforeOrEqualAttribute : AbstractRule
{
    public BeforeOrEqualAttribute(string dateTime = "now")
    {
    }

    public BeforeOrEqualAttribute(DateTime dateTime)
    {
    }

    public BeforeOrEqualAttribute(DateTimeOffset dateTime)
    {
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        throw new NotImplementedException();
    }
}
