using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Helpers.ScalarTypes;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class MinAttribute : AbstractRule
{
    private readonly CultureInfo _culture = CultureInfo.InvariantCulture;
    private readonly string      _min;

    /// <summary>
    ///     The field under validation must be grater than or equal to a minimum value. Strings, numerics, arrays, and
    ///     files are evaluated in the same fashion as the size rule.
    /// </summary>
    /// <param name="min">Expected value.</param>
    public MinAttribute(string min)
    {
        this._min = min;
        this.MessageArgs.Add(min);
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value switch
               {
                   string str             => this.Check(str),
                   short number           => this.Check(number),
                   ushort number          => this.Check(number),
                   int number             => this.Check(number),
                   uint number            => this.Check(number),
                   long number            => this.Check(number),
                   ulong number           => this.Check(number),
                   float number           => this.Check(number),
                   double number          => this.Check(number),
                   decimal number         => this.Check(number),
                   DateTime date          => this.Check(date),
                   DateTimeOffset date    => this.Check(date),
                   ICollection collection => this.Check(collection),
                   IEnumerable collection => collection.CountIsGreatThanOrEqual(int.Parse(this._min, this._culture)),
                   var _                  => throw new UnsupportedTypeException(value.GetType()),
               };
    }

    private bool Check(ICollection value)
    {
        return value.Count >= int.Parse(this._min, this._culture);
    }

    private bool Check(string value)
    {
        return value.Length >= int.Parse(this._min, this._culture);
    }

    private bool Check<T>(T value) where T : IComparable, IParsable<T>
    {
        return value.CompareTo(T.Parse(this._min, this._culture)) >= 0;
    }
}
