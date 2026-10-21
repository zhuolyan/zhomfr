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
/// <example>
///     <code>
///         public class SizeDto
///         {
///             [Size("3")]
///             public ICollection CollectionInvalid = new List<int>
///             { 1, 2 }; // ValidationError [Size("3")] public
///             ICollection CollectionValid = new List
///             <int>
///                 { 1, 2, 3 }; // Success [Size("2026-10-18 07:00:01")] public DateTime DateTimeInvalid =
///                 DateTime.Parse("2026-10-18 07:00:00"); // ValidationError [Size("2026-10-18 07:00:01")] public
///                 DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // ValidationError
///                 [Size("2026-10-18 07:00:00")] public DateTimeOffset DateTimeOffsetValid =
///                 DateTimeOffset.Parse("2026-10-18 07:00:00"); // Success [Size("2026-10-18 07:00:00")] public DateTime
///                 DateTimeValid = DateTime.Parse("2026-10-18 07:00:00"); // Success [Size("3.3")] public decimal
///                 DecimalInvalid = 3.2M; // ValidationError [Size("3.3")] public decimal DecimalValid = 3.3M; // Success
///                 [Size("3.3")] public double DoubleInvalid = 3.3; // ValidationError [Size("3.3")] public double
///                 DoubleValid = 3.3; // Success [Size("3")] public IEnumerable EnumerableInvalid = new List
///                 <int>
///                     { 1, 2 }; // ValidationError [Size("3")] public IEnumerable EnumerableValid = new List
///                     <int>
///                         { 1, 2, 3 }; // Success [Size("3.3")] public float FloatInvalid = 3.2f; // ValidationError
///                         [Size("3.3")] public float FloatValid = 3.3f; // Success [Size("3")] public int IntInvalid = 1;
///                         // ValidationError [Size("3")] public int IntValid = 3; // Success [Size("3")] public long
///                         LongInvalid = 1; // ValidationError [Size("3")] public long LongValid = 3; // Success
///                         [Size("3")] public short ShortInvalid = 1; // ValidationError [Size("3")] public short
///                         ShortValid = 3; // Success [Size("3")] public string StringInvalid = "something"; //
///                         ValidationError [Size("3")] public string StringValid = "tes"; // Success [Size("3")] public
///                         uint UintInvalid = 1; // ValidationError [Size("3")] public uint UintValid = 3; // Success
///                         [Size("3")] public ulong UlongInvalid = 1; // ValidationError [Size("3")] public ulong
///                         UlongValid = 3; // Success [Size("3")] public ushort UshortInvalid = 1; // ValidationError
///                         [Size("3")] public ushort UshortValid = 3; // Success }
///         
///     </code>
/// </example>
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
