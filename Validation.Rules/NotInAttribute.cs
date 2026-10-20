using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class NotInAttribute : AbstractRule
{
    private readonly CultureInfo  _culture = CultureInfo.InvariantCulture;
    private readonly List<string> _values;

    /// <summary>The field under validation must not be included in the given list of values.</summary>
    /// <remarks>
    ///     For floating-point types (float, double), a tolerance epsilon of 0.000001 is used due to internal precision
    ///     limitations that can cause rounding errors.
    /// </remarks>
    /// <param name="items">Expected values.</param>
    public NotInAttribute(params string[] items)
    {
        if (items.Length == 0) {
            throw new ArgumentException($"{nameof(items)} is empty");
        }

        this._values = [.. items];

        this.AddListAsOneArgumentToMessageArgs([.. items]);
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value switch
               {
                   string str          => this._values.All(s => s != str),
                   short number        => this.Check(number),
                   ushort number       => this.Check(number),
                   int number          => this.Check(number),
                   uint number         => this.Check(number),
                   long number         => this.Check(number),
                   ulong number        => this.Check(number),
                   float number        => this.Check(number),
                   double number       => this.Check(number),
                   decimal number      => this.Check(number),
                   DateTime date       => this.Check(date),
                   DateTimeOffset date => this.Check(date),
                   var _               => throw new UnsupportedTypeException(value.GetType()),
               };
    }

    private bool Check<T>(T value) where T : IParsable<T>, IEquatable<T>
    {
        return !this._values.Any(i => T.Parse(i, this._culture).Equals(value));
    }
}
