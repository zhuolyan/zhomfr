using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Helpers.ScalarTypes;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must be grater than or equal to a minimum value. Strings, numerics, arrays, and
///     files are evaluated in the same fashion as the size rule.
/// </summary>
/// <example>
///     <code>
///         public class MinDto
///         {
///             [Min("3")]
///             public ICollection CollectionInvalid = new List<int>
///             { 1, 2 }; // ValidationError [Min("3")] public
///             ICollection CollectionValid = new List
///             <int>
///                 { 1, 2, 3 }; // Success [Min("2026-10-18 07:00:01")] public DateTime DateTimeInvalid =
///                 DateTime.Parse("2026-10-18 07:00:00"); // ValidationError [Min("2026-10-18 07:00:01")] public
///                 DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // ValidationError
///                 [Min("2026-10-18 07:00:00")] public DateTimeOffset DateTimeOffsetValid =
///                 DateTimeOffset.Parse("2026-10-18 07:00:02"); // Success [Min("2026-10-18 07:00:00")] public DateTime
///                 DateTimeValid = DateTime.Parse("2026-10-18 07:00:00"); // Success [Min("3.3")] public decimal
///                 DecimalInvalid = 3.2M; // ValidationError [Min("3.3")] public decimal DecimalValid = 3.3M; // Success
///                 [Min("3.3")] public double DoubleInvalid = 3.2; // ValidationError [Min("3.3")] public double
///                 DoubleValid = 3.3; // Success [Min("3")] public IEnumerable EnumerableInvalid = new List
///                 <int>
///                     { 1, 2 }; // ValidationError [Min("3")] public IEnumerable EnumerableValid = new List
///                     <int>
///                         { 1, 2, 4, 5 }; // Success [Min("3.3")] public float FloatInvalid = 3.2f; // ValidationError
///                         [Min("3.3")] public float FloatValid = 3.3f; // Success [Min("3")] public int IntInvalid = 1;
///                         // ValidationError [Min("3")] public int IntValid = 4; // Success [Min("3")] public long
///                         LongInvalid = 1; // ValidationError [Min("3")] public long LongValid = 4; // Success [Min("3")]
///                         public short ShortInvalid = 1; // ValidationError [Min("3")] public short ShortValid = 4; //
///                         Success [Min("3")] public string StringInvalid = "te"; // ValidationError [Min("3")] public
///                         string StringValid = "test"; // Success [Min("3")] public uint UintInvalid = 1; //
///                         ValidationError [Min("3")] public uint UintValid = 4; // Success [Min("3")] public ulong
///                         UlongInvalid = 1; // ValidationError [Min("3")] public ulong UlongValid = 4; // Success
///                         [Min("3")] public ushort UshortInvalid = 1; // ValidationError [Min("3")] public ushort
///                         UshortValid = 4; // Success }
///         
///     </code>
/// </example>
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
