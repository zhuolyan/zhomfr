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
                   string str             => this.CheckIfBetween(str),
                   short number           => this.CheckIfBetween(number),
                   ushort number          => this.CheckIfBetween(number),
                   int number             => this.CheckIfBetween(number),
                   uint number            => this.CheckIfBetween(number),
                   long number            => this.CheckIfBetween(number),
                   ulong number           => this.CheckIfBetween(number),
                   float number           => this.CheckIfBetween(number),
                   double number          => this.CheckIfBetween(number),
                   decimal number         => this.CheckIfBetween(number),
                   DateTime date          => this.CheckIfBetween(date),
                   DateTimeOffset date    => this.CheckIfBetween(date),
                   ICollection collection => this.CheckIfBetween(collection),
                   IEnumerable collection => collection.CountIsBetween(int.Parse(this._min, this._culture), int.Parse(this._max, this._culture)),
                   var _                  => throw new UnsupportedTypeException(value.GetType()),
               };
    }

    private bool CheckIfBetween(ICollection value)
    {
        return value.Count >= int.Parse(this._min, this._culture) && value.Count <= int.Parse(this._max, this._culture);
    }

    private bool CheckIfBetween(string value)
    {
        return value.Length >= int.Parse(this._min, this._culture) && value.Length <= int.Parse(this._max, this._culture);
    }

    private bool CheckIfBetween<T>(T value) where T : IComparable, IParsable<T>
    {
        return value.CompareTo(T.Parse(this._min, this._culture)) >= 0 && value.CompareTo(T.Parse(this._max, this._culture)) <= 0;
    }
}
