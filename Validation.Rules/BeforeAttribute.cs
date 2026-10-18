using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class BeforeAttribute : AbstractRule
{
    public const     string DEFAULT = "now";
    private readonly string _maxDate;

    /// <summary>The field under validation must be a value preceding the given date.</summary>
    /// <param name="maxDate">The minimum datetime. Defaults to the current datetime.</param>
    public BeforeAttribute(string maxDate = AfterAttribute.DEFAULT)
    {
        this._maxDate = maxDate;
        this.MessageArgs.Add(maxDate);
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value switch
               {
                   DateTime date       => this._maxDate == AfterAttribute.DEFAULT ? date < DateTime.Now : date       < DateTime.Parse(this._maxDate),
                   DateTimeOffset date => this._maxDate == AfterAttribute.DEFAULT ? date < DateTimeOffset.Now : date < DateTimeOffset.Parse(this._maxDate),
                   var _               => throw new UnsupportedTypeException(value.GetType()),
               };
    }
}
