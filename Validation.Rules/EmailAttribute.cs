using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.RegularExpressions;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be formatted as an email address.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public partial class EmailAttribute(params EmailAttribute.Mode[] modes) : AbstractRule
{
    public enum Mode
    {
        /// <summary>Validate the email address according to supported RFCs.</summary>
        Rfc,

        /// <summary>Ensure the email address's domain has a valid MX record.</summary>
        Dns,

        /// <summary>Ensure the email address does not contain homograph or deceptive Unicode characters.</summary>
        Spoof,

        /// <summary>Simplified regex for checking the existence of a 'user@domain' pattern,allowing some Unicode characters..</summary>
        Filter,
    }

    private static readonly Dictionary<Mode, Func<string, bool>> _strategies = new()
    {
        [Mode.Dns]    = EmailAttribute.IsValidDns,
        [Mode.Filter] = email => EmailAttribute.BasicStructure().IsMatch(email),
        [Mode.Rfc]    = email => EmailAttribute.Strict().IsMatch(email),
        [Mode.Spoof]  = email => email.All(c => c < 128),
    };

    [ExcludeFromCodeCoverage]
    [GeneratedRegex(
        """^(?!\.)("([^"\r\\]|\\["\r\\])+"|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)+)(?<!\.)@((?=[a-z0-9-]{1,63}\.)(xn--)?[a-z0-9]+(-[a-z0-9]+)*\.)+[a-z]{2,63}$""",
        RegexOptions.IgnoreCase | RegexOptions.Compiled)]
    private static partial Regex Strict();

    /// <summary>Simplified regex for checking the existence of a 'user@domain' pattern.</summary>
    [ExcludeFromCodeCoverage]
    [GeneratedRegex("^(.+)@(.+)$", RegexOptions.Compiled)]
    private static partial Regex BasicStructure();

    /// <summary>Performs a DNS lookup on the domain portion of the email address.</summary>
    private static bool IsValidDns(string email)
    {
        if (!email.Contains('@')) {
            return false;
        }

        string host = email.Split('@').Last();

        try {
            // Check if the domain has any IP addresses associated with it.
            IPHostEntry hostEntry = Dns.GetHostEntry(host);

            return hostEntry.AddressList.Length > 0;
        } catch {
            return false;
        }
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value is not string email
                   ? throw new UnsupportedTypeException()
                   : modes.All(mode => EmailAttribute._strategies.TryGetValue(mode, out Func<string, bool>? validate) && validate(email));
    }
}
