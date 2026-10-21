using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be a value preceding the given date.</summary>
/// <example>
///     <code>
///         public class BeforeOrEqualDto
///         {
///             [BeforeOrEqual("2026-10-18 06:59:59")]
///             public DateTime DateTimeInvalid = DateTime.Parse("2026-10-18 07:00:00"); // ValidationError
///         
///             [BeforeOrEqual("2026-10-18 06:59:59")]
///             public DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // ValidationError
///         
///             [BeforeOrEqual("2026-10-18 07:00:00")]
///             public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // Success
///         
///             [BeforeOrEqual("2026-10-18 07:00:00")]
///             public DateTime DateTimeValid = DateTime.Parse("2026-10-18 07:00:00"); // Success
///         }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class BeforeOrEqualAttribute : AbstractRule
{
    /// <summary>Default argument.</summary>
    public const string DEFAULT = "now";

    private readonly string _maxDate;

    /// <summary>The field under validation must be a value preceding the given date.</summary>
    /// <param name="maxDate">The minimum datetime. Defaults to the current datetime.</param>
    public BeforeOrEqualAttribute(string maxDate = BeforeOrEqualAttribute.DEFAULT)
    {
        this._maxDate = maxDate;
        this.MessageArgs.Add(maxDate);
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value switch
               {
                   DateTime date => this._maxDate == BeforeOrEqualAttribute.DEFAULT ? date <= DateTime.Now : date <= DateTime.Parse(this._maxDate),
                   DateTimeOffset date => this._maxDate == BeforeOrEqualAttribute.DEFAULT
                                              ? date    <= DateTimeOffset.Now
                                              : date    <= DateTimeOffset.Parse(this._maxDate),
                   var _ => throw new UnsupportedTypeException(value.GetType()),
               };
    }
}
