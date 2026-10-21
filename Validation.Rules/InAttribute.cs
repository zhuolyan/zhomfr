using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be included in the given list of values.</summary>
/// <example>
///     <code>
///         public class InDto
///         {
///             [In("2026-10-17 07:00:00", "2026-10-19 07:00:00")]
///             public DateTime DateTimeInvalid = DateTime.Parse("2026-10-18 07:00:00"); // ValidationError
///         
///             [In("2026-10-17 07:00:00", "2026-10-19 07:00:00")]
///             public DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // ValidationError
///         
///             [In("2026-10-18 07:00:00", "2026-10-19 07:00:00")]
///             public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // Success
///         
///             [In("2026-10-17", "2026-10-19 07:00:00")]
///             public DateTime DateTimeValid = DateTime.Parse("2026-10-17"); // Success
///         
///             [In("0.000001", "1.2", "-0.000001")]
///             public decimal DecimalInvalid = 3.3M; // ValidationError
///         
///             [In("0.000001", "1.2", "-0.000001")]
///             public decimal DecimalValid = 1.2M; // Success
///         
///             [In("0.000001", "1.2", "-0.000001")]
///             public double DoubleInvalid = 3.3; // ValidationError
///         
///             [In("0.000001", "1.2", "-0.000001")]
///             public double DoubleValid = -0.000001; // Success
///         
///             [In("0.000001", "1.2", "-0.000001")]
///             public float FloatInvalid = 3.3f; // ValidationError
///         
///             [In("0.000001", "1.2", "-0.000001")]
///             public float FloatValid = 1.2f; // Success
///         
///             [In("1", "2", "3")]
///             public int IntInvalid = 0; // ValidationError
///         
///             [In("1", "2", "3")]
///             public int IntValid = 1; // Success
///         
///             [In("1", "2", "3")]
///             public long LongInvalid = 0; // ValidationError
///         
///             [In("1", "2", "3")]
///             public long LongValid = 1; // Success
///         
///             [In("1", "2", "3")]
///             public short ShortInvalid = 0; // ValidationError
///         
///             [In("1", "2", "3")]
///             public short ShortValid = 1; // Success
///         
///             [In("test1", "test2", "test3")]
///             public string StringInvalid = "something"; // ValidationError
///         
///             [In("test1", "test2", "test3")]
///             public string StringValid = "test2"; // Success
///         
///             [In("1", "2", "3")]
///             public uint UintInvalid = 0; // ValidationError
///         
///             [In("1", "2", "3")]
///             public uint UintValid = 1; // Success
///         
///             [In("1", "2", "3")]
///             public ulong UlongInvalid = 0; // ValidationError
///         
///             [In("1", "2", "3")]
///             public ulong UlongValid = 1; // Success
///         
///             [In("1", "2", "3")]
///             public ushort UshortInvalid = 0; // ValidationError
///         
///             [In("1", "2", "3")]
///             public ushort UshortValid = 1; // Success
///         }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class InAttribute : AbstractRule
{
    private readonly CultureInfo  _culture = CultureInfo.InvariantCulture;
    private readonly List<string> _values;

    /// <summary>The field under validation must be included in the given list of values.</summary>
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
        return value switch
               {
                   string str          => this._values.Any(s => s == str),
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
        return this._values.Any(i => T.Parse(i, this._culture).Equals(value));
    }
}
