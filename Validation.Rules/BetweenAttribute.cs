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
    private readonly string _max;
    private readonly string _min;

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
                   string str => str.Length >= int.Parse(this._min, CultureInfo.InvariantCulture)
                              && str.Length <= int.Parse(this._max, CultureInfo.InvariantCulture),
                   short number => number >= short.Parse(this._min, CultureInfo.InvariantCulture)
                                && number <= short.Parse(this._max, CultureInfo.InvariantCulture),
                   ushort number => number >= ushort.Parse(this._min, CultureInfo.InvariantCulture)
                                 && number <= ushort.Parse(this._max, CultureInfo.InvariantCulture),
                   int number  => number >= int.Parse(this._min, CultureInfo.InvariantCulture)  && number <= int.Parse(this._max, CultureInfo.InvariantCulture),
                   uint number => number >= uint.Parse(this._min, CultureInfo.InvariantCulture) && number <= uint.Parse(this._max, CultureInfo.InvariantCulture),
                   long number => number >= long.Parse(this._min, CultureInfo.InvariantCulture) && number <= long.Parse(this._max, CultureInfo.InvariantCulture),
                   ulong number => number >= ulong.Parse(this._min, CultureInfo.InvariantCulture)
                                && number <= ulong.Parse(this._max, CultureInfo.InvariantCulture),
                   float number => number >= float.Parse(this._min, CultureInfo.InvariantCulture)
                                && number <= float.Parse(this._max, CultureInfo.InvariantCulture),
                   double number => number >= double.Parse(this._min, CultureInfo.InvariantCulture)
                                 && number <= double.Parse(this._max, CultureInfo.InvariantCulture),
                   decimal number => number >= decimal.Parse(this._min, CultureInfo.InvariantCulture)
                                  && number <= decimal.Parse(this._max, CultureInfo.InvariantCulture),
                   DateTime date => date >= DateTime.Parse(this._min, CultureInfo.InvariantCulture)
                                 && date <= DateTime.Parse(this._max, CultureInfo.InvariantCulture),
                   DateTimeOffset date => date >= DateTimeOffset.Parse(this._min, CultureInfo.InvariantCulture)
                                       && date <= DateTimeOffset.Parse(this._max, CultureInfo.InvariantCulture),
                   ICollection collection => collection.Count >= int.Parse(this._min, CultureInfo.InvariantCulture)
                                          && collection.Count <= int.Parse(this._max, CultureInfo.InvariantCulture),
                   IEnumerable collection => collection.CountIsBetween(int.Parse(this._min, CultureInfo.InvariantCulture),
                                                                       int.Parse(this._max, CultureInfo.InvariantCulture)),
                   var _ => throw new UnsupportedTypeException(value.GetType()),
               };
    }
}
