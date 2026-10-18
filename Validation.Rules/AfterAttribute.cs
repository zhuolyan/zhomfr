using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

public class AfterAttribute : AbstractRule
{
    public const     string DEFAULT = "now";
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
