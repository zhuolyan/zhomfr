using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

using DnsClient;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public partial class EmailAttribute : AbstractRule
{
    /// <summary>Checks if the domain portion of the email has valid DNS records.</summary>
    public const string DNS = "dns";

    /// <summary>Ensures the email follows a basic user@domain structure.</summary>
    public const string FILTER_UNICODE = "filter_unicode";

    /// <summary>Validates the email against a strict RFC-compliant regular expression.</summary>
    public const string RFC = "rfc";

    /// <summary>Ensures the email contains only ASCII characters to prevent homograph spoofing.</summary>
    public const string SPOOF = "spoof";

    private static readonly Dictionary<string, Func<string, bool>> _strategies = new()
    {
        [EmailAttribute.DNS]            = EmailAttribute.IsValidDns,
        [EmailAttribute.FILTER_UNICODE] = email => EmailAttribute.BasicStructure().IsMatch(email),
        [EmailAttribute.RFC]            = email => EmailAttribute.Strict().IsMatch(email),
        [EmailAttribute.SPOOF]          = email => email.All(c => c < 128),
    };

    private readonly List<string> _modes;

    /// <summary>Validates an email address using one or more configurable strategies.</summary>
    /// <remarks>
    ///     This attribute supports multiple modes that can be combined:
    ///     <list type="bullet">
    ///         <item>
    ///             <term>dns</term><description>Verifies that the domain has valid DNS records.</description>
    ///         </item>
    ///         <item>
    ///             <term>filter_unicode</term><description>Checks for a basic "user@domain" structure.</description>
    ///         </item>
    ///         <item>
    ///             <term>rfc</term><description>Applies a strict, RFC-compliant regular expression.</description>
    ///         </item>
    ///         <item>
    ///             <term>spoof</term>
    ///             <description>Restricts the email to ASCII characters only to prevent homograph attacks.</description>
    ///         </item>
    ///     </list>
    /// </remarks>
    /// <param name="modes">
    ///     The list of validation modes to apply (e.g., <c>EmailAttribute.Modes.Rfc</c>,
    ///     <c>EmailAttribute.Modes.Dns</c>).
    /// </param>
    public EmailAttribute(params string[] modes)
    {
        if (modes.Length == 0) {
            throw new ArgumentException($"{nameof(modes)} is empty");
        }

        this._modes = [.. modes];
        this.AddListAsOneArgumentToMessageArgs([.. modes]);
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value is string email
                   ? this._modes.All(mode => EmailAttribute._strategies.TryGetValue(mode, out Func<string, bool>? validate) && validate(email))
                   : throw new UnsupportedTypeException(value.GetType());
    }

    /// <summary>RFC-compliant regular expression for email validation.</summary>
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
            LookupClient      lookupClient = new();
            IDnsQueryResponse result       = lookupClient.Query(host, QueryType.MX);

            return result.Answers.MxRecords().Any();
        } catch {
            return false;
        }
    }
}
