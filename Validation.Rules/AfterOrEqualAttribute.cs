using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be a value after or equal to the given date.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class AfterOrEqualAttribute : AbstractRule
{
    public AfterOrEqualAttribute(string dateTime = "now")
    {
    }

    public AfterOrEqualAttribute(DateTime dateTime)
    {
    }

    public AfterOrEqualAttribute(DateTimeOffset dateTime)
    {
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        throw new NotImplementedException();
    }
}
