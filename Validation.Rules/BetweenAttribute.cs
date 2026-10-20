using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Helpers.ScalarTypes;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class BetweenAttribute : AbstractRule
{
    private readonly CultureInfo _culture = CultureInfo.InvariantCulture;
    private readonly string      _max;
    private readonly string      _min;

    public BetweenAttribute(string min, string max)
    {
        this._min = min;
        this._max = max;

        this.MessageArgs.Add(min);
        this.MessageArgs.Add(max);
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value switch
               {
                   string str             => this.Check(str),
                   short number           => this.Checkn(number),
                   ushort number          => this.Checkn(number),
                   int number             => this.Checkn(number),
                   uint number            => this.Checkn(number),
                   long number            => this.Checkn(number),
                   ulong number           => this.Checkn(number),
                   float number           => this.Checkn(number),
                   double number          => this.Checkn(number),
                   decimal number         => this.Checkn(number),
                   DateTime date          => this.Checkn(date),
                   DateTimeOffset date    => this.Checkn(date),
                   ICollection collection => this.Check(collection),
                   IEnumerable collection => collection.CountIsBetween(int.Parse(this._min, this._culture), int.Parse(this._max, this._culture)),
                   var _                  => throw new UnsupportedTypeException(value.GetType()),
               };
    }

    private bool Check(ICollection value)
    {
        return value.Count >= int.Parse(this._min, this._culture) && value.Count <= int.Parse(this._max, this._culture);
    }

    private bool Check(string value)
    {
        return value.Length >= int.Parse(this._min, this._culture) && value.Length <= int.Parse(this._max, this._culture);
    }

    private bool Checkn<T>(T value) where T : IComparable, IParsable<T>
    {
        return value.CompareTo(T.Parse(this._min, this._culture)) >= 0 && value.CompareTo(T.Parse(this._max, this._culture)) <= 0;
    }
}
