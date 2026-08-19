using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be formatted as an email address.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class EmailAttribute(params EmailAttribute.Mode[] modes) : AbstractRule
{
    public enum Mode
    {
        /// <summary>Validate the email address according to supported RFCs.</summary>
        Rfc,

        /// <summary>
        ///     Validate the email according to supported RFCs, failing when warnings are found (e.g. trailing periods and
        ///     multiple consecutive periods).
        /// </summary>
        Strict,

        /// <summary>Ensure the email address's domain has a valid MX record.</summary>
        Dns,

        /// <summary>Ensure the email address does not contain homograph or deceptive Unicode characters.</summary>
        Spoof,

        /// <summary>Simplified regex for checking the existence of a 'user@domain' pattern.</summary>
        Filter,

        /// <summary>Simplified regex for checking the existence of a 'user@domain' pattern,allowing some Unicode characters..</summary>
        FilterUnicode,
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        throw new NotImplementedException();
    }
}
