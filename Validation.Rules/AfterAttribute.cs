using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be a value after a given date.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class AfterAttribute : AbstractRule
{
    public AfterAttribute(string dateTime = "now")
    {
    }

    public AfterAttribute(DateTime dateTime)
    {
    }

    public AfterAttribute(DateTimeOffset dateTime)
    {
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        throw new NotImplementedException();
    }
}
