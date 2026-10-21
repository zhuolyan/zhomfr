using System.Collections;
using System.ComponentModel.DataAnnotations;

using Zhomfr.Helpers.ScalarTypes;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must be less than the given field. The two fields must be of the same type.
///     Strings, numerics, arrays, and files are evaluated using the same conventions as the size rule.
/// </summary>
/// <example>
///     <code>
///         public class LessThanDto
///         {
///             [LessThan("CollectionInvalidSecond")]
///             public ICollection CollectionInvalid = new List<int>
///             { 1, 2, 3 }; // ValidationError public ICollection
///             CollectionInvalidSecond = new List
///             <int>
///                 { 1, 2, 3 }; [LessThan("CollectionValidSecond")] public ICollection CollectionValid = new List
///                 <int>
///                     { 1, 2, 3 }; // Success public ICollection CollectionValidSecond = new List
///                     <int>
///                         { 1, 2 }; [LessThan("DateTimeInvalidSecond")] public DateTime DateTimeInvalid =
///                         DateTime.Parse("2026-10-18 07:00:00"); // ValidationError public DateTime DateTimeInvalidSecond
///                         = DateTime.Parse("2026-10-18 07:00:00"); [LessThan("DateTimeOffsetInvalidSecond")] public
///                         DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:00"); //
///                         ValidationError public DateTimeOffset DateTimeOffsetInvalidSecond =
///                         DateTimeOffset.Parse("2026-10-18 07:00:00"); [LessThan("DateTimeOffsetValidSecond")] public
///                         DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // Success
///                         public DateTimeOffset DateTimeOffsetValidSecond = DateTimeOffset.Parse("2026-10-18 07:00:01");
///                         [LessThan("DateTimeValidSecond")] public DateTime DateTimeValid = DateTime.Parse("2026-10-18
///                         07:00:00"); // Success public DateTime DateTimeValidSecond = DateTime.Parse("2026-10-18
///                         07:00:01"); [LessThan("DecimalInvalidSecond")] public decimal DecimalInvalid = 3.3M; //
///                         ValidationError public decimal DecimalInvalidSecond = 3.3M; [LessThan("DecimalValidSecond")]
///                         public decimal DecimalValid = 3.3M; // Success public decimal DecimalValidSecond = 3.2999M;
///                         [LessThan("DoubleInvalidSecond")] public double DoubleInvalid = 3.3; // ValidationError public
///                         double DoubleInvalidSecond = 3.3; [LessThan("DoubleValidSecond")] public double DoubleValid =
///                         3.3; // Success public double DoubleValidSecond = 3.2999; [LessThan("EnumerableInvalidSecond")]
///                         public IEnumerable EnumerableInvalid = new List
///                         <int>
///                             { 1, 2, 3 }; // ValidationError public IEnumerable EnumerableInvalidSecond = new List
///                             <int>
///                                 { 1, 2, 3 }; [LessThan("EnumerableValidSecond")] public IEnumerable EnumerableValid =
///                                 new List
///                                 <int>
///                                     { 1, 2, 3 }; // Success public IEnumerable EnumerableValidSecond = new List
///                                     <int>
///                                         { 1, 2 }; [LessThan("FloatInvalidSecond")] public float FloatInvalid = 3.3f;
///                                         // ValidationError public float FloatInvalidSecond = 3.3f;
///                                         [LessThan("FloatValidSecond")] public float FloatValid = 3.3f; // Success
///                                         public float FloatValidSecond = 3.2999f; [LessThan("IntInvalidSecond")] public
///                                         int IntInvalid = 1; // ValidationError public int IntInvalidSecond = 1;
///                                         [LessThan("IntValidSecond")] public int IntValid = 1; // Success public int
///                                         IntValidSecond = 0; [LessThan("LongInvalidSecond")] public long LongInvalid =
///                                         1; // ValidationError public long LongInvalidSecond = 1;
///                                         [LessThan("LongValidSecond")] public long LongValid = 1; // Success public long
///                                         LongValidSecond = 0; [LessThan("ShortInvalidSecond")] public short ShortInvalid
///                                         = 1; // ValidationError public short ShortInvalidSecond = 1;
///                                         [LessThan("ShortValidSecond")] public short ShortValid = 1; // Success public
///                                         short ShortValidSecond = 0; [LessThan("StringInvalidSecond")] public string
///                                         StringInvalid = "something"; // ValidationError public string
///                                         StringInvalidSecond = "something"; [LessThan("StringValidSecond")] public
///                                         string StringValid = "something"; // Success public string StringValidSecond =
///                                         "somethin"; [LessThan("UintInvalidSecond")] public uint UintInvalid = 1; //
///                                         ValidationError public uint UintInvalidSecond = 1;
///                                         [LessThan("UintValidSecond")] public uint UintValid = 1; // Success public uint
///                                         UintValidSecond = 0; [LessThan("UlongInvalidSecond")] public ulong UlongInvalid
///                                         = 1; // ValidationError public ulong UlongInvalidSecond = 1;
///                                         [LessThan("UlongValidSecond")] public ulong UlongValid = 1; // Success public
///                                         ulong UlongValidSecond = 0; [LessThan("UshortInvalidSecond")] public ushort
///                                         UshortInvalid = 1; // ValidationError public ushort UshortInvalidSecond = 1;
///                                         [LessThan("UshortValidSecond")] public ushort UshortValid = 1; // Success
///                                         public ushort UshortValidSecond = 0; }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class LessThanAttribute : AbstractRule
{
    private readonly string _field;
    private          bool   _cached;

    /// <summary>
    ///     The field under validation must be less than the given field. The two fields must be of the same type.
    ///     Strings, numerics, arrays, and files are evaluated using the same conventions as the size rule.
    /// </summary>
    /// <param name="field">Field name.</param>
    public LessThanAttribute(string field)
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

        AbstractRule.ThrowIfFieldsHasDifferentTypes(value, anotherValue);

        return value switch
               {
                   string str             => str.Length       < ((string)anotherValue).Length,
                   ICollection collection => collection.Count < ((ICollection)anotherValue).Count,
                   IEnumerable collection => collection.CountIsLessThan(((IEnumerable)anotherValue).Cast<object>().ToList().Count),
                   IComparable comparable => comparable.CompareTo(anotherValue) < 0,
                   var _                  => throw new UnsupportedTypeException(value.GetType()),
               };
    }
}
