using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class NotRegexpAttribute : AbstractRule
{
    private readonly string _pattern;

    /// <summary>The field under validation must match the given regular expression.</summary>
    /// <param name="pattern">expected pattern.</param>
    public NotRegexpAttribute(string pattern)
    {
        if (string.IsNullOrWhiteSpace(pattern)) {
            throw new ArgumentException($"{nameof(pattern)} is empty");
        }

        this._pattern = pattern;
        this.MessageArgs.Add($"\"{pattern}\"");
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value is string str ? !Regex.IsMatch(str, this._pattern) : throw new UnsupportedTypeException(value.GetType());
    }
}
