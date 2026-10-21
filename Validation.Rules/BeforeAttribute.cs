using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be a value preceding the given date.</summary>
/// <target-type>DateTime,DateTimeOffset</target-type>
/// <example>
///     <code>
///         public class BeforeDto
///         {
///             [Before("2026-10-18 07:00:00")]
///             public DateTime DateTimeInvalid = DateTime.Parse("2026-10-18 07:00:00"); // ValidationError
///         
///             [Before("2026-10-18 07:00:00")]
///             public DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // ValidationError
///         
///             [Before("2026-10-18 07:00:01")]
///             public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // Success
///         
///             [Before("2026-10-18 07:00:01")]
///             public DateTime DateTimeValid = DateTime.Parse("2026-10-18 07:00:00"); // Success
///         }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class BeforeAttribute : AbstractRule
{
    /// <summary>Default argument.</summary>
    public const string DEFAULT = "now";

    private readonly string _maxDate;

    /// <summary>The field under validation must be a value preceding the given date.</summary>
    /// <param name="maxDate">The minimum datetime. Defaults to the current datetime.</param>
    public BeforeAttribute(string maxDate = BeforeAttribute.DEFAULT)
    {
        this._maxDate = maxDate;
        this.MessageArgs.Add(maxDate);
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value switch
               {
                   DateTime date       => this._maxDate == BeforeAttribute.DEFAULT ? date < DateTime.Now : date       < DateTime.Parse(this._maxDate),
                   DateTimeOffset date => this._maxDate == BeforeAttribute.DEFAULT ? date < DateTimeOffset.Now : date < DateTimeOffset.Parse(this._maxDate),
                   var _               => throw new UnsupportedTypeException(value.GetType()),
               };
    }
}
