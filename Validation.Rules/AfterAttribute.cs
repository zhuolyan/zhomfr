using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be a value after a given date.</summary>
/// <target-type>DateTime,DateTimeOffset</target-type>
/// <example>
///     <code>
///         public class AfterDto
///         {
///             [After("2026-10-18 07:00:00")]
///             public DateTime DateTimeInvalid = DateTime.Parse("2026-10-18 07:00:00"); // ValidationError
///         
///             [After("2026-10-18 07:00:00")]
///             public DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // ValidationError
///         
///             [After("2026-10-18 06:59:59")]
///             public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // Success
///         
///             [After("2026-10-18 06:59:59")]
///             public DateTime DateTimeValid = DateTime.Parse("2026-10-18 07:00:00"); // Success
///         }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class AfterAttribute : AbstractRule
{
    /// <summary>Default argument.</summary>
    public const string DEFAULT = "now";

    private readonly string _minDate;

    /// <summary>The field under validation must be a value after a given date.</summary>
    /// <param name="minDate">The minimum datetime. Defaults to the current datetime.</param>
    public AfterAttribute(string minDate = AfterAttribute.DEFAULT)
    {
        this._minDate = minDate;
        this.MessageArgs.Add(minDate);
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value switch
               {
                   DateTime date       => this._minDate == AfterAttribute.DEFAULT ? date > DateTime.Now : date       > DateTime.Parse(this._minDate),
                   DateTimeOffset date => this._minDate == AfterAttribute.DEFAULT ? date > DateTimeOffset.Now : date > DateTimeOffset.Parse(this._minDate),
                   var _               => throw new UnsupportedTypeException(value.GetType()),
               };
    }
}
