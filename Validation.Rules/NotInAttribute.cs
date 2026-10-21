using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must not be included in the given list of values.</summary>
/// <remarks>
///     For floating-point types (float, double), a tolerance epsilon of 0.000001 is used due to internal precision
///     limitations that can cause rounding errors.
/// </remarks>
/// <example>
///     <code>
///         public class NotInDto
///         {
///             [NotIn("1", "2")]
///             public ICollection CollectionInvalid = new List<int>
///             { 1, 2, 3 }; // ValidationError [NotIn("1", "2")]
///             public ICollection CollectionValid = new List
///             <int>
///                 { 4, 5, 6 }; // Success [NotIn("2026-10-17 07:00:00", "2026-10-19 07:00:00")] public DateTime
///                 DateTimeInvalid = DateTime.Parse("2026-10-18 07:00:00"); // ValidationError [NotIn("2026-10-17
///                 07:00:00", "2026-10-19 07:00:00")] public DateTimeOffset DateTimeOffsetInvalid =
///                 DateTimeOffset.Parse("2026-10-18 07:00:00"); // ValidationError [NotIn("2026-10-18 07:00:00",
///                 "2026-10-19 07:00:00")] public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18
///                 07:00:00"); // Success [NotIn("2026-10-18 07:00:00", "2026-10-19 07:00:00")] public DateTime
///                 DateTimeValid = DateTime.Parse("2026-10-18 07:00:00"); // Success [NotIn("3.2", "3.4")] public decimal
///                 DecimalInvalid = 3.2M; // ValidationError [NotIn("3.2", "3.4")] public decimal DecimalValid = 3.3M; //
///                 Success [NotIn("3.2", "3.4")] public double DoubleInvalid = 3.2; // ValidationError [NotIn("3.2",
///                 "3.4")] public double DoubleValid = 3.3; // Success [NotIn("1", "2")] public IEnumerable
///                 EnumerableInvalid = new List
///                 <int>
///                     { 1, 2, 3 }; // ValidationError [NotIn("1", "2")] public IEnumerable EnumerableValid = new List
///                     <int>
///                         { 4, 5, 6 }; // Success [NotIn("3.2", "3.4")] public float FloatInvalid = 3.2f; //
///                         ValidationError [NotIn("3.2", "3.4")] public float FloatValid = 3.3f; // Success [NotIn("1",
///                         "2")] public int IntInvalid = 1; // ValidationError [NotIn("1", "2")] public int IntValid = 3;
///                         // Success [NotIn("1", "2")] public long LongInvalid = 1; // ValidationError [NotIn("1", "2")]
///                         public long LongValid = 3; // Success [NotIn("1", "2")] public short ShortInvalid = 1; //
///                         ValidationError [NotIn("1", "2")] public short ShortValid = 3; // Success [NotIn("something",
///                         "test")] public string StringInvalid = "something"; // ValidationError [NotIn("test", "else")]
///                         public string StringValid = "something"; // Success [NotIn("1", "2")] public uint UintInvalid =
///                         1; // ValidationError [NotIn("1", "2")] public uint UintValid = 3; // Success [NotIn("1", "2")]
///                         public ulong UlongInvalid = 1; // ValidationError [NotIn("1", "2")] public ulong UlongValid =
///                         3; // Success [NotIn("1", "2")] public ushort UshortInvalid = 1; // ValidationError [NotIn("1",
///                         "2")] public ushort UshortValid = 3; // Success }
///         
///     </code>
/// </example>
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
