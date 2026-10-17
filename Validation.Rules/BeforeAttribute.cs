using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be a value preceding the given date.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class BeforeAttribute(string dateTime = "now") : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value switch
               {
                   DateTime date       => date < (dateTime == "now" ? DateTime.Now : DateTime.Parse(dateTime)),
                   DateTimeOffset date => date < (dateTime == "now" ? DateTimeOffset.Now : DateTimeOffset.Parse(dateTime)),
                   var _               => throw new UnsupportedTypeException(),
               };
    }
}
