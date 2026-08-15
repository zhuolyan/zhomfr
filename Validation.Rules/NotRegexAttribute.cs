using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must not match the given regular expression.</summary>
/// <param name="pattern"></param>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class NotRegexAttribute(string pattern) : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        throw new NotImplementedException();
    }
}
