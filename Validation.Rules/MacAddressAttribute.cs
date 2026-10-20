using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be a MAC address..</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class MacAddressAttribute : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value is not string str ? throw new UnsupportedTypeException(value.GetType()) : PhysicalAddress.TryParse(str, out PhysicalAddress? _);
    }
}
