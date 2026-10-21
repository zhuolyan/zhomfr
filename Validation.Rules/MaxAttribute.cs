using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Helpers.ScalarTypes;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must be less than or equal to a maximum value. Strings, numerics, arrays, and files
///     are evaluated in the same fashion as the size rule.
/// </summary>
/// <target-type>string,short,ushort,int,uint,long,ulong,float,double,decimal,DateTime,DateTimeOffset,ICollection,IEnumerable</target-type>
/// <example>
///     <code>
///         public class MaxDto
///         {
///             [Max("2026-10-18 07:00:00")]
///             public DateTime DateTimeInvalid = DateTime.Parse("2026-10-18 07:00:01"); // ValidationError
///         
///             [Max("2026-10-18 07:00:00")]
///             public DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:01"); // ValidationError
///         
///             [Max("2026-10-18 07:00:00")]
///             public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // Success
///         
///             [Max("2026-10-18 07:00:00")]
///             public DateTime DateTimeValid = DateTime.Parse("2026-10-18 07:00:00"); // Success
///         
///             [Max("3.2")]
///             public decimal DecimalInvalid = 3.3M; // ValidationError
///         
///             [Max("3.3")]
///             public decimal DecimalValid = 3.3M; // Success
///         
///             [Max("3.2")]
///             public double DoubleInvalid = 3.3; // ValidationError
///         
///             [Max("3.4")]
///             public double DoubleValid = 3.3; // Success
///         
///             [Max("3.2")]
///             public float FloatInvalid = 3.3f; // ValidationError
///         
///             [Max("3.3")]
///             public float FloatValid = 3.3f; // Success
///         
///             [Max("4")]
///             public ICollection CollectionInvalid = new List&lt;int&gt;() { 1, 2, 3 }; // ValidationError
///         
///             [Max("3")]
///             public ICollection CollectionValid = new List&lt;int&gt;() { 1, 2, 3 }; // Success
///         
///             [Max("4")]
///             public IEnumerable EnumerableInvalid = new List&lt;int&gt;() { 1, 2, 3 }; // ValidationError
///         
///             [Max("3")]
///             public IEnumerable EnumerableValid = new List&lt;int&gt;() { 1, 2, 3 }; // Success
///         
///             [Max("3")]
///             public int IntInvalid = 4; // ValidationError
///         
///             [Max("3")]
///             public int IntValid = 1; // Success
///         
///             [Max("3")]
///             public long LongInvalid = 4; // ValidationError
///         
///             [Max("3")]
///             public long LongValid = 1; // Success
///         
///             [Max("3")]
///             public short ShortInvalid = 4; // ValidationError
///         
///             [Max("3")]
///             public short ShortValid = 1; // Success
///         
///             [Max("4")]
///             public string StringInvalid = "something"; // ValidationError
///         
///             [Max("4")]
///             public string StringValid = "test"; // Success
///         
///             [Max("3")]
///             public uint UintInvalid = 4; // ValidationError
///         
///             [Max("3")]
///             public uint UintValid = 1; // Success
///         
///             [Max("3")]
///             public ulong UlongInvalid = 4; // ValidationError
///         
///             [Max("3")]
///             public ulong UlongValid = 1; // Success
///         
///             [Max("3")]
///             public ushort UshortInvalid = 4; // ValidationError
///         
///             [Max("3")]
///             public ushort UshortValid = 1; // Success
///         }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class MaxAttribute : AbstractRule
{
    private readonly CultureInfo _culture = CultureInfo.InvariantCulture;
    private readonly string      _max;

    /// <summary>
    ///     The field under validation must be less than or equal to a maximum value. Strings, numerics, arrays, and files
    ///     are evaluated in the same fashion as the size rule.
    /// </summary>
    /// <param name="max">Expected value.</param>
    public MaxAttribute(string max)
    {
        this._max = max;
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
                   IEnumerable collection => collection.CountIsLessThanOrEqual(int.Parse(this._max, this._culture)),
                   var _                  => throw new UnsupportedTypeException(value.GetType()),
               };
    }

    private bool Check(ICollection value)
    {
        return value.Count <= int.Parse(this._max, this._culture);
    }

    private bool Check(string value)
    {
        return value.Length <= int.Parse(this._max, this._culture);
    }

    private bool Check<T>(T value) where T : IComparable, IParsable<T>
    {
        return value.CompareTo(T.Parse(this._max, this._culture)) <= 0;
    }
}
