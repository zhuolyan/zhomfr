using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be a MAC address..</summary>
/// <target-type>string</target-type>
/// <example>
///     <code>
///         public class MacAddressDto
///         {
///             [MacAddress]
///             public string StringInvalid = "192.168.1.1"; // ValidationError
///         
///             [MacAddress]
///             public string StringValid = "AC:DE:48:23:45:67"; // Success
///         }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class MacAddressAttribute : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value is not string str ? throw new UnsupportedTypeException(value.GetType()) : PhysicalAddress.TryParse(str, out PhysicalAddress? _);
    }
}
