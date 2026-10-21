using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be a value after a given date.</summary>
/// <target-type>DateTime,DateTimeOffset</target-type>
/// <example>
///     <code>
///         public class AfterOrEqualDto
///         {
///             [AfterOrEqual("2026-10-18 07:00:01")]
///             public DateTime DateTimeInvalid = DateTime.Parse("2026-10-18 07:00:00"); // ValidationError
///         
///             [AfterOrEqual("2026-10-18 07:00:01")]
///             public DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // ValidationError
///         
///             [AfterOrEqual("2026-10-18 07:00:00")]
///             public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // Success
///         
///             [AfterOrEqual("2026-10-18 07:00:00")]
///             public DateTime DateTimeValid = DateTime.Parse("2026-10-18 07:00:00"); // Success
///         }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class AfterOrEqualAttribute : AbstractRule
{
    /// <summary>Default argument.</summary>
    public const string DEFAULT = "now";

    private readonly string _minDate;

    /// <summary>The field under validation must be a value after a given date.</summary>
    /// <param name="minDate">The minimum datetime. Defaults to the current datetime.</param>
    public AfterOrEqualAttribute(string minDate = AfterOrEqualAttribute.DEFAULT)
    {
        this._minDate = minDate;
        this.MessageArgs.Add(minDate);
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value switch
               {
                   DateTime date => this._minDate == AfterOrEqualAttribute.DEFAULT ? date >= DateTime.Now : date >= DateTime.Parse(this._minDate),
                   DateTimeOffset date => this._minDate == AfterOrEqualAttribute.DEFAULT
                                              ? date    >= DateTimeOffset.Now
                                              : date    >= DateTimeOffset.Parse(this._minDate),
                   var _ => throw new UnsupportedTypeException(value.GetType()),
               };
    }
}
