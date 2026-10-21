using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>Validates that a string is a valid hexadecimal color code.</summary>
/// <remarks>
///     The string must start with a '#' symbol followed by 3, 4, 6, or 8 hexadecimal characters. Supported formats:
///     <list type="bullet">
///         <item>
///             <description>#RGB (e.g., #F00)</description>
///         </item>
///         <item>
///             <description>#RGBA (e.g., #F00F)</description>
///         </item>
///         <item>
///             <description>#RRGGBB (e.g., #FF0000)</description>
///         </item>
///         <item>
///             <description>#RRGGBBAA (e.g., #FF0000FF)</description>
///         </item>
///     </list>
/// </remarks>
/// <target-type>string</target-type>
/// <example>
///     <code>
///         public class HexColorDto
///         {
///             [HexColor]
///             public string StringInvalid = "FFF"; // ValidationError
///         
///             [HexColor]
///             public string StringValid = "#FFFFFF"; // Success
///         }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public partial class HexColorAttribute : AbstractRule
{
    /// <summary>Compiles a regex to validate the hexadecimal color structure.</summary>
    [GeneratedRegex("^#([A-Fa-f0-9]{3}|[A-Fa-f0-9]{4}|[A-Fa-f0-9]{6}|[A-Fa-f0-9]{8})$", RegexOptions.Compiled)]
    private static partial Regex HexColorRegex();

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value is string str ? HexColorAttribute.HexColorRegex().IsMatch(str) : throw new UnsupportedTypeException(value.GetType());
    }
}
