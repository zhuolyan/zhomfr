using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Sockets;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

// ReSharper disable InconsistentNaming

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be an IP address.</summary>
/// <param name="validationMode">The validation mode. By default any address family are correct.</param>
/// <target-type>string,IPAddress</target-type>
/// <example>
///     <code>
///         public class IpDto
///         {
///             [Ip(IpAttribute.Modes.IPv4)]
///             public IPAddress IPAddressInvalid = IPAddress.IPv6Any; // ValidationError
///         
///             [Ip(IpAttribute.Modes.IPv6)]
///             public IPAddress IPAddressValid = IPAddress.Any; // Success
///         
///             [Ip(IpAttribute.Modes.IPv4)]
///             public string StringInvalid = "2001:0db8:85a3:0000:0000:8a2e:0370:7334"; // ValidationError
///         
///             [Ip(IpAttribute.Modes.IPv6)]
///             public string StringValid = "2001:0db8:85a3:0000:0000:8a2e:0370:7334"; // Success
///         }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class IpAttribute(IpAttribute.Modes validationMode = IpAttribute.Modes.Any) : AbstractRule
{
    /// <summary>Modes for validation logic.</summary>
    public enum Modes
    {
        /// <summary>Any IP Address type are available.</summary>
        Any,

        /// <summary>Only IPv4 Address type are available.</summary>
        IPv4,

        /// <summary>Only IPv6 Address type are available.</summary>
        IPv6,
    }

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
