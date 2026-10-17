using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must contain a valid color value in hexadecimal format.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public partial class HexColorAttribute : AbstractRule
{
    /// <summary>Compiles a regex to validate the hexadecimal color structure.</summary>
    [GeneratedRegex("^#([A-Fa-f0-9]{3}|[A-Fa-f0-9]{4}|[A-Fa-f0-9]{6}|[A-Fa-f0-9]{8})$", RegexOptions.Compiled)]
    private static partial Regex HexColorRegex();

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value is not string str ? throw new UnsupportedTypeException() : HexColorAttribute.HexColorRegex().IsMatch(str);
    }
}
