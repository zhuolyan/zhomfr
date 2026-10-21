using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must match the given regular expression.</summary>
/// <target-type>string</target-type>
/// <example>
///     <code>
///         public class RegexpDto
///         {
///             [Regexp("^[0-9]+$")]
///             public string StringInvalid = "abc"; // ValidationError
///         
///             [Regexp("^[0-9]+$")]
///             public string StringValid = "123"; // Success
///         }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class RegexpAttribute : AbstractRule
{
    private readonly string _pattern;

    /// <summary>The field under validation must match the given regular expression.</summary>
    /// <param name="pattern">expected pattern.</param>
    public RegexpAttribute(string pattern)
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
        return value is string str ? Regex.IsMatch(str, this._pattern) : throw new UnsupportedTypeException(value.GetType());
    }
}
