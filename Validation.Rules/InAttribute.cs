using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

public class InAttribute : AbstractRule
{
    private readonly List<string> _values;

    /// <summary>
    ///     The field under validation must be included in the given list of values.
    ///     <remarks>
    ///         For floating-point types (float, double), a tolerance epsilon of 0.000001 is used due to internal
    ///         precision limitations that can cause rounding errors.
    ///     </remarks>
    /// </summary>
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
        object test  = value;
        return test switch
               {
                   string str          => this._values.Any(s => s                                                                                    == str),
                   short number        => this._values.Any(s => short.Parse(s, NumberStyles.Integer, CultureInfo.InvariantCulture)                   == number),
                   ushort number       => this._values.Any(s => ushort.Parse(s, NumberStyles.Integer, CultureInfo.InvariantCulture)                  == number),
                   int number          => this._values.Any(s => int.Parse(s, NumberStyles.Integer, CultureInfo.InvariantCulture)                     == number),
                   uint number         => this._values.Any(s => uint.Parse(s, NumberStyles.Integer, CultureInfo.InvariantCulture)                    == number),
                   long number         => this._values.Any(s => long.Parse(s, NumberStyles.Integer, CultureInfo.InvariantCulture)                    == number),
                   ulong number        => this._values.Any(s => ulong.Parse(s, NumberStyles.Integer, CultureInfo.InvariantCulture)                   == number),
                   float number        => this._values.Any(s => MathF.Abs(float.Parse(s, NumberStyles.Float, CultureInfo.InvariantCulture) - number) < 0.000001f),
                   double number       => this._values.Any(s => Math.Abs(double.Parse(s, NumberStyles.Float, CultureInfo.InvariantCulture) - number) < 0.000001),
                   decimal number      => this._values.Any(s => decimal.Parse(s, NumberStyles.Float, CultureInfo.InvariantCulture)                   == number),
                   DateTime date       => this._values.Any(s => DateTime.Parse(s, CultureInfo.InvariantCulture)                                      == date),
                   DateTimeOffset date => this._values.Any(s => DateTimeOffset.Parse(s, CultureInfo.InvariantCulture)                                == date),
                   var _               => throw new UnsupportedTypeException(value.GetType()),
               };
    }
}
