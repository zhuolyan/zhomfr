using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class InAttribute : AbstractRule
{
    private readonly CultureInfo  _culture = CultureInfo.InvariantCulture;
    private readonly List<string> _values;

    /// <summary>The field under validation must be included in the given list of values.</summary>
    /// <remarks>
    ///     For floating-point types (float, double), a tolerance epsilon of 0.000001 is used due to internal precision
    ///     limitations that can cause rounding errors.
    /// </remarks>
    /// <param name="items">Expected values.</param>
    public InAttribute(params string[] items)
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
        object test = value;

        return test switch
               {
                   string str          => this._values.Any(s => s == str),
                   short number        => this.CheckIfExists(number),
                   ushort number       => this.CheckIfExists(number),
                   int number          => this.CheckIfExists(number),
                   uint number         => this.CheckIfExists(number),
                   long number         => this.CheckIfExists(number),
                   ulong number        => this.CheckIfExists(number),
                   float number        => this.CheckIfExists(number),
                   double number       => this.CheckIfExists(number),
                   decimal number      => this.CheckIfExists(number),
                   DateTime date       => this.CheckIfExists(date),
                   DateTimeOffset date => this.CheckIfExists(date),
                   var _               => throw new UnsupportedTypeException(value.GetType()),
               };
    }

    private bool CheckIfExists(float value)
    {
        return this._values.Any(i => MathF.Abs(float.Parse(i, NumberStyles.Float, CultureInfo.InvariantCulture) - value) < 0.000001f);
    }

    private bool CheckIfExists(double value)
    {
        return this._values.Any(i => Math.Abs(double.Parse(i, NumberStyles.Float, CultureInfo.InvariantCulture) - value) < 0.000001);
    }

    private bool CheckIfExists<T>(T value) where T : IParsable<T>, IEquatable<T>
    {
        return this._values.Any(i => T.Parse(i, this._culture).Equals(value));
    }
}
