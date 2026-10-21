using System.Collections;
using System.ComponentModel.DataAnnotations;

using Zhomfr.Helpers.ScalarTypes;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must be less than or equal to the given field. The two fields must be of the same
///     type. Strings, numerics, arrays, and files are evaluated using the same conventions as the size rule.
/// </summary>
/// <target-type>string,short,ushort,int,uint,long,ulong,float,double,decimal,DateTime,DateTimeOffset,ICollection,IEnumerable</target-type>
/// <example>
///     <code>
///         public class LessThanOrEqualDto
///         {
///             [LessThanOrEqual("DateTimeInvalidSecond")]
///             public DateTime DateTimeInvalid = DateTime.Parse("2026-10-18 07:00:00"); // ValidationError
///         
///             public DateTime DateTimeInvalidSecond = DateTime.Parse("2026-10-18 07:00:01");
///         
///             [LessThanOrEqual("DateTimeOffsetInvalidSecond")]
///             public DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // ValidationError
///         
///             public DateTimeOffset DateTimeOffsetInvalidSecond = DateTimeOffset.Parse("2026-10-18 07:00:01");
///         
///             [LessThanOrEqual("DateTimeOffsetValidSecond")]
///             public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // Success
///         
///             public DateTimeOffset DateTimeOffsetValidSecond = DateTimeOffset.Parse("2026-10-18 07:00:00");
///         
///             [LessThanOrEqual("DateTimeValidSecond")]
///             public DateTime DateTimeValid = DateTime.Parse("2026-10-18 07:00:00"); // Success
///         
///             public DateTime DateTimeValidSecond = DateTime.Parse("2026-10-18 07:00:00");
///         
///             [LessThanOrEqual("DecimalInvalidSecond")]
///             public decimal DecimalInvalid = 3.3M; // ValidationError
///         
///             public decimal DecimalInvalidSecond = 3.30001M;
///         
///             [LessThanOrEqual("DecimalValidSecond")]
///             public decimal DecimalValid = 3.3M; // Success
///         
///             public decimal DecimalValidSecond = 3.3M;
///         
///             [LessThanOrEqual("DoubleInvalidSecond")]
///             public double DoubleInvalid = 3.3; // ValidationError
///         
///             public double DoubleInvalidSecond = 3.30001;
///         
///             [LessThanOrEqual("DoubleValidSecond")]
///             public double DoubleValid = 3.3; // Success
///         
///             public double DoubleValidSecond = 3.3;
///         
///             [LessThanOrEqual("FloatInvalidSecond")]
///             public float FloatInvalid = 3.3f; // ValidationError
///         
///             public float FloatInvalidSecond = 3.30001f;
///         
///             [LessThanOrEqual("FloatValidSecond")]
///             public float FloatValid = 3.3f; // Success
///         
///             public float FloatValidSecond = 3.3f;
///         
///             [LessThanOrEqual("CollectionInvalidSecond")]
///             public ICollection CollectionInvalid = new List&lt;int&gt;() { 1, 2 }; // ValidationError
///         
///             public ICollection CollectionInvalidSecond = new List&lt;int&gt;() { 1, 2, 3 };
///         
///             [LessThanOrEqual("CollectionValidSecond")]
///             public ICollection CollectionValid = new List&lt;int&gt;() { 1, 2 }; // Success
///         
///             public ICollection CollectionValidSecond = new List&lt;int&gt;() { 1, 2 };
///         
///             [LessThanOrEqual("EnumerableInvalidSecond")]
///             public IEnumerable EnumerableInvalid = new List&lt;int&gt;() { 1, 2 }; // ValidationError
///         
///             public IEnumerable EnumerableInvalidSecond = new List&lt;int&gt;() { 1, 2, 3 };
///         
///             [LessThanOrEqual("EnumerableValidSecond")]
///             public IEnumerable EnumerableValid = new List&lt;int&gt;() { 1, 2 }; // Success
///         
///             public IEnumerable EnumerableValidSecond = new List&lt;int&gt;() { 1, 2 };
///         
///             [LessThanOrEqual("IntInvalidSecond")]
///             public int IntInvalid = 1; // ValidationError
///         
///             public int IntInvalidSecond = 0;
///         
///             [LessThanOrEqual("IntValidSecond")]
///             public int IntValid = 1; // Success
///         
///             public int IntValidSecond = 1;
///         
///             [LessThanOrEqual("LongInvalidSecond")]
///             public long LongInvalid = 1; // ValidationError
///         
///             public long LongInvalidSecond = 0;
///         
///             [LessThanOrEqual("LongValidSecond")]
///             public long LongValid = 1; // Success
///         
///             public long LongValidSecond = 1;
///         
///             [LessThanOrEqual("ShortInvalidSecond")]
///             public short ShortInvalid = 1; // ValidationError
///         
///             public short ShortInvalidSecond = 0;
///         
///             [LessThanOrEqual("ShortValidSecond")]
///             public short ShortValid = 1; // Success
///         
///             public short ShortValidSecond = 1;
///         
///             [LessThanOrEqual("StringInvalidSecond")]
///             public string StringInvalid = "something"; // ValidationError
///         
///             public string StringInvalidSecond = "somethin";
///         
///             [LessThanOrEqual("StringValidSecond")]
///             public string StringValid = "something"; // Success
///         
///             public string StringValidSecond = "something";
///         
///             [LessThanOrEqual("UintInvalidSecond")]
///             public uint UintInvalid = 1; // ValidationError
///         
///             public uint UintInvalidSecond = 0;
///         
///             [LessThanOrEqual("UintValidSecond")]
///             public uint UintValid = 1; // Success
///         
///             public uint UintValidSecond = 1;
///         
///             [LessThanOrEqual("UlongInvalidSecond")]
///             public ulong UlongInvalid = 1; // ValidationError
///         
///             public ulong UlongInvalidSecond = 0;
///         
///             [LessThanOrEqual("UlongValidSecond")]
///             public ulong UlongValid = 1; // Success
///         
///             public ulong UlongValidSecond = 1;
///         
///             [LessThanOrEqual("UshortInvalidSecond")]
///             public ushort UshortInvalid = 1; // ValidationError
///         
///             public ushort UshortInvalidSecond = 0;
///         
///             [LessThanOrEqual("UshortValidSecond")]
///             public ushort UshortValid = 1; // Success
///         
///             public ushort UshortValidSecond = 1;
///         }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class LessThanOrEqualAttribute : AbstractRule
{
    private readonly string _field;
    private          bool   _cached;

    /// <summary>
    ///     The field under validation must be less than or equal to the given field. The two fields must be of the same
    ///     type. Strings, numerics, arrays, and files are evaluated using the same conventions as the size rule.
    /// </summary>
    /// <param name="field">Field name.</param>
    public LessThanOrEqualAttribute(string field)
    {
        this._field = field;
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        if (!this._cached) {
            this.MessageArgs.Add(AbstractRule.GetDisplayNameForProperty(this._field, context));
            this._cached = true;
        }

        object? anotherValue = AbstractRule.GetFieldValue<object>(this._field, context);

        if (anotherValue == null) {
            return false;
        }

        AbstractRule.ThrowIfFieldsHasDifferentTypes(value, anotherValue);

        return value switch
               {
                   string str             => str.Length       <= ((string)anotherValue).Length,
                   ICollection collection => collection.Count <= ((ICollection)anotherValue).Count,
                   IEnumerable collection => collection.CountIsLessThanOrEqual(((IEnumerable)anotherValue).Cast<object>().ToList().Count),
                   IComparable comparable => comparable.CompareTo(anotherValue) <= 0,
                   var _                  => throw new UnsupportedTypeException(value.GetType()),
               };
    }
}
