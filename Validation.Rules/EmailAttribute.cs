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

        /// <summary>Ensure the email address's domain has a valid MX record.</summary>
        Dns,

        /// <summary>Ensure the email address does not contain homograph or deceptive Unicode characters.</summary>
        Spoof,

        /// <summary>Simplified regex for checking the existence of a 'user@domain' pattern,allowing some Unicode characters..</summary>
        Filter,
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        throw new NotImplementedException();
    }
}
