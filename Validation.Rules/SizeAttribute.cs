using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Helpers.ScalarTypes;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must have a size matching the given value. For string data, value corresponds to
///     the number of characters. For numeric data, value corresponds to an expected value. For collection, size
///     corresponds to the count of the collection.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class SizeAttribute : AbstractRule
{
    private readonly CultureInfo _culture = CultureInfo.InvariantCulture;
    private readonly string      _size;

    /// <summary>
    ///     The field under validation must have a size matching the given value. For string data, value corresponds to
    ///     the number of characters. For numeric data, value corresponds to an expected value. For collection, size
    ///     corresponds to the count of the collection.
    /// </summary>
    /// <param name="size">Expected value.</param>
    public SizeAttribute(string size)
    {
        this._size = size;
        this.MessageArgs.Add(size);
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
                   IEnumerable collection => collection.CountIsBetween(int.Parse(this._size, this._culture), int.Parse(this._size, this._culture)),
                   var _                  => throw new UnsupportedTypeException(value.GetType()),
               };
    }

    private bool Check(ICollection value)
    {
        return value.Count == int.Parse(this._size, this._culture);
    }

    private bool Check(string value)
    {
        return value.Length == int.Parse(this._size, this._culture);
    }

    private bool Check<T>(T value) where T : IComparable, IParsable<T>
    {
        return value.CompareTo(T.Parse(this._size, this._culture)) == 0;
    }
}
