using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Sockets;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

// ReSharper disable InconsistentNaming

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be an IP address.</summary>
/// <param name="validationMode">The validation mode. By default any address family are correct.</param>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class IpAttribute(IpAttribute.Modes validationMode = IpAttribute.Modes.Any) : AbstractRule
{
    public enum Modes { Any, IPv4, IPv6 }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value switch
               {
                   string str        => this.CheckStringIp(str),
                   IPAddress address => this.CheckIpAddress(address),
                   var _             => throw new UnsupportedTypeException(value.GetType()),
               };
    }

    private bool CheckIpAddress(IPAddress address)
    {
        return validationMode switch
               {
                   Modes.IPv4 => address.AddressFamily == AddressFamily.InterNetwork,
                   Modes.IPv6 => address.AddressFamily == AddressFamily.InterNetworkV6,
                   var _      => true,
               };
    }

    private bool CheckStringIp(string value)
    {
        return IPAddress.TryParse(value, out IPAddress? ipAddress) && this.CheckIpAddress(ipAddress);
    }
}
