using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Helpers.ScalarTypes;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must have a size between the given min and max (inclusive). Strings, numerics,
///     arrays, and files are evaluated in the same fashion as the size rule.
/// </summary>
/// <target-type>string,short,ushort,int,uint,long,ulong,float,double,decimal,DateTime,DateTimeOffset,ICollection,IEnumerable</target-type>
/// <example>
///     <code>
///         public class BetweenDto
///         {
///             [Between("2026-10-17 07:00:00", "2026-10-18 06:59:59")]
///             public DateTime DateTimeInvalid = DateTime.Parse("2026-10-18 07:00:00"); // ValidationError
///         
///             [Between("2026-10-18 07:00:01", "2026-10-19 07:00:00")]
///             public DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // ValidationError
///         
///             [Between("2026-10-18 07:00:00", "2026-10-19 07:00:00")]
///             public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // Success
///         
///             [Between("2026-10-17 07:00:00", "2026-10-18 07:00:00")]
///             public DateTime DateTimeValid = DateTime.Parse("2026-10-18 07:00:00"); // Success
///         
///             [Between("1", "3.299999")]
///             public decimal DecimalInvalid = 3.3M; // ValidationError
///         
///             [Between("3.3", "5.9")]
///             public decimal DecimalValid = 3.3M; // Success
///         
///             [Between("1", "3.299999")]
///             public double DoubleInvalid = 3.3; // ValidationError
///         
///             [Between("3.3", "5.9")]
///             public double DoubleValid = 3.3; // Success
///         
///             [Between("1", "3.299999")]
///             public float FloatInvalid = 3.3f; // ValidationError
///         
///             [Between("3.3", "5.9")]
///             public float FloatValid = 3.3f; // Success
///         
///             [Between("2", "5")]
///             public ICollection CollectionInvalid = new List&lt;int&gt;() { 1 }; // ValidationError
///         
///             [Between("2", "5")]
///             public ICollection CollectionValid = new List&lt;int&gt;() { 1, 2 }; // Success
///         
///             [Between("2", "5")]
///             public IEnumerable EnumerableInvalid = new List&lt;int&gt;() { 1 }; // ValidationError
///         
///             [Between("2", "5")]
///             public IEnumerable EnumerableValid = new List&lt;int&gt;() { 1, 2, 3, 4, 5 }; // Success
///         
///             [Between("-5", "5")]
///             public int IntInvalid = -6; // ValidationError
///         
///             [Between("-5", "5")]
///             public int IntValid = -5; // Success
///         
///             [Between("-5", "5")]
///             public long LongInvalid = 6; // ValidationError
///         
///             [Between("-5", "5")]
///             public long LongValid = 5; // Success
///         
///             [Between("-5", "5")]
///             public short ShortInvalid = 10; // ValidationError
///         
///             [Between("-5", "5")]
///             public short ShortValid = 1; // Success
///         
///             [Between("2", "5")]
///             public string StringInvalid = "something"; // ValidationError
///         
///             [Between("2", "5")]
///             public string StringValid = "test"; // Success
///         
///             [Between("2", "5")]
///             public uint UintInvalid = 1; // ValidationError
///         
///             [Between("2", "5")]
///             public uint UintValid = 2; // Success
///         
///             [Between("2", "5")]
///             public ulong UlongInvalid = 1; // ValidationError
///         
///             [Between("2", "5")]
///             public ulong UlongValid = 2; // Success
///         
///             [Between("2", "5")]
///             public ushort UshortInvalid = 1; // ValidationError
///         
///             [Between("2", "5")]
///             public ushort UshortValid = 2; // Success
///         }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class BetweenAttribute : AbstractRule
{
    private readonly CultureInfo _culture = CultureInfo.InvariantCulture;
    private readonly string      _max;
    private readonly string      _min;

    /// <summary>
    ///     The field under validation must have a size between the given min and max (inclusive). Strings, numerics,
    ///     arrays, and files are evaluated in the same fashion as the size rule.
    /// </summary>
    /// <param name="min">Min size.</param>
    /// <param name="max">Max size.</param>
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
                   IEnumerable collection => collection.CountIsBetween(int.Parse(this._min, this._culture), int.Parse(this._max, this._culture)),
                   var _                  => throw new UnsupportedTypeException(value.GetType()),
               };
    }

    private bool Check(ICollection value)
    {
        return value.Count >= int.Parse(this._min, this._culture) && value.Count <= int.Parse(this._max, this._culture);
    }

    private bool Check(string value)
    {
        return value.Length >= int.Parse(this._min, this._culture) && value.Length <= int.Parse(this._max, this._culture);
    }

    private bool Check<T>(T value) where T : IComparable, IParsable<T>
    {
        return value.CompareTo(T.Parse(this._min, this._culture)) >= 0 && value.CompareTo(T.Parse(this._max, this._culture)) <= 0;
    }
}
