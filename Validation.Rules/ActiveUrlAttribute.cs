using System.ComponentModel.DataAnnotations;
using System.Net;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must have a valid A or AAAA record.</summary>
/// <example>
///     <code>
///         public class ActiveUrlDto
///         {
///             [ActiveUrl]
///             public string StringInvalid = "non-existent-domain-123456789.com"; // ValidationError
///         
///             [ActiveUrl]
///             public string StringValid = "https://microsoft.com"; // Success
///         }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class ActiveUrlAttribute : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        if (value is not string url) {
            throw new UnsupportedTypeException(value.GetType());
        }

        try {
            Uri    uri  = new(url.Contains("://") ? url : $"https://{url}");
            string host = uri.Host;

            IPAddress[] addresses = Dns.GetHostAddresses(host);

            return addresses.Length != 0;
        } catch {
            return false;
        }
    }
}
