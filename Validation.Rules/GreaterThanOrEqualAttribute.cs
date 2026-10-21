using System.Collections;
using System.ComponentModel.DataAnnotations;

using Zhomfr.Helpers.ScalarTypes;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must be greater than the given fielde. The two fields must be of the same type.
///     Strings, numerics, collections are evaluated using the same conventions as the size rule.
/// </summary>
/// <example>
///     <code>
///         public class GreaterThanOrEqualDto
///         {
///             [GreaterThanOrEqual("CollectionInvalidSecond")]
///             public ICollection CollectionInvalid = new List<int>
///             { 1, 2 }; // ValidationError public ICollection
///             CollectionInvalidSecond = new List
///             <int>
///                 { 1, 2, 3 }; [GreaterThanOrEqual("CollectionValidSecond")] public ICollection CollectionValid = new
///                 List
///                 <int>
///                     { 1, 2, 3 }; // Success public ICollection CollectionValidSecond = new List
///                     <int>
///                         { 1, 2, 3 }; [GreaterThanOrEqual("DateTimeInvalidSecond")] public DateTime DateTimeInvalid =
///                         DateTime.Parse("2026-10-18 07:00:01"); // ValidationError public DateTime DateTimeInvalidSecond
///                         = DateTime.Parse("2026-10-18 07:00:00"); [GreaterThanOrEqual("DateTimeOffsetInvalidSecond")]
///                         public DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:01"); //
///                         ValidationError public DateTimeOffset DateTimeOffsetInvalidSecond =
///                         DateTimeOffset.Parse("2026-10-18 07:00:00"); [GreaterThanOrEqual("DateTimeOffsetValidSecond")]
///                         public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:00"); //
///                         Success public DateTimeOffset DateTimeOffsetValidSecond = DateTimeOffset.Parse("2026-10-18
///                         07:00:00"); [GreaterThanOrEqual("DateTimeValidSecond")] public DateTime DateTimeValid =
///                         DateTime.Parse("2026-10-18 07:00:00"); // Success public DateTime DateTimeValidSecond =
///                         DateTime.Parse("2026-10-18 07:00:00"); [GreaterThanOrEqual("DecimalInvalidSecond")] public
///                         decimal DecimalInvalid = 3.3M; // ValidationError public decimal DecimalInvalidSecond =
///                         3.2999M; [GreaterThanOrEqual("DecimalValidSecond")] public decimal DecimalValid = 3.3M; //
///                         Success public decimal DecimalValidSecond = 3.3M; [GreaterThanOrEqual("DoubleInvalidSecond")]
///                         public double DoubleInvalid = 3.3; // ValidationError public double DoubleInvalidSecond =
///                         3.2999; [GreaterThanOrEqual("DoubleValidSecond")] public double DoubleValid = 3.3; // Success
///                         public double DoubleValidSecond = 3.3; [GreaterThanOrEqual("EnumerableInvalidSecond")] public
///                         IEnumerable EnumerableInvalid = new List
///                         <int>
///                             { 1, 2 }; // ValidationError public IEnumerable EnumerableInvalidSecond = new List
///                             <int>
///                                 { 1, 2, 3 }; [GreaterThanOrEqual("EnumerableValidSecond")] public IEnumerable
///                                 EnumerableValid = new List
///                                 <int>
///                                     { 1, 2, 3 }; // Success public IEnumerable EnumerableValidSecond = new List
///                                     <int>
///                                         { 1, 2, 3 }; [GreaterThanOrEqual("FloatInvalidSecond")] public float
///                                         FloatInvalid = 3.3f; // ValidationError public float FloatInvalidSecond =
///                                         3.2999f; [GreaterThanOrEqual("FloatValidSecond")] public float FloatValid =
///                                         3.3f; // Success public float FloatValidSecond = 3.3f;
///                                         [GreaterThanOrEqual("IntInvalidSecond")] public int IntInvalid = 0; //
///                                         ValidationError public int IntInvalidSecond = 1;
///                                         [GreaterThanOrEqual("IntValidSecond")] public int IntValid = 1; // Success
///                                         public int IntValidSecond = 1; [GreaterThanOrEqual("LongInvalidSecond")] public
///                                         long LongInvalid = 0; // ValidationError public long LongInvalidSecond = 1;
///                                         [GreaterThanOrEqual("LongValidSecond")] public long LongValid = 1; // Success
///                                         public long LongValidSecond = 1; [GreaterThanOrEqual("ShortInvalidSecond")]
///                                         public short ShortInvalid = 0; // ValidationError public short
///                                         ShortInvalidSecond = 1; [GreaterThanOrEqual("ShortValidSecond")] public short
///                                         ShortValid = 1; // Success public short ShortValidSecond = 1;
///                                         [GreaterThanOrEqual("StringInvalidSecond")] public string StringInvalid =
///                                         "something"; // ValidationError public string StringInvalidSecond =
///                                         "something"; [GreaterThanOrEqual("StringValidSecond")] public string
///                                         StringValid = "somethin"; // Success public string StringValidSecond =
///                                         "something"; [GreaterThanOrEqual("UintInvalidSecond")] public uint UintInvalid
///                                         = 0; // ValidationError public uint UintInvalidSecond = 1;
///                                         [GreaterThanOrEqual("UintValidSecond")] public uint UintValid = 1; // Success
///                                         public uint UintValidSecond = 1; [GreaterThanOrEqual("UlongInvalidSecond")]
///                                         public ulong UlongInvalid = 0; // ValidationError public ulong
///                                         UlongInvalidSecond = 1; [GreaterThanOrEqual("UlongValidSecond")] public ulong
///                                         UlongValid = 1; // Success public ulong UlongValidSecond = 1;
///                                         [GreaterThanOrEqual("UshortInvalidSecond")] public ushort UshortInvalid = 0; //
///                                         ValidationError public ushort UshortInvalidSecond = 1;
///                                         [GreaterThanOrEqual("UshortValidSecond")] public ushort UshortValid = 1; //
///                                         Success public ushort UshortValidSecond = 1; }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class GreaterThanOrEqualAttribute : AbstractRule
{
    private readonly string _field;
    private          bool   _cached;

    /// <summary>
    ///     The field under validation must be greater than the given fielde. The two fields must be of the same type.
    ///     Strings, numerics, collections are evaluated using the same conventions as the size rule.
    /// </summary>
    /// <param name="field">Field name.</param>
    public GreaterThanOrEqualAttribute(string field)
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
                   string str             => str.Length       >= ((string)anotherValue).Length,
                   ICollection collection => collection.Count >= ((ICollection)anotherValue).Count,
                   IEnumerable collection => collection.CountIsGreatThanOrEqual(((IEnumerable)anotherValue).Cast<object>().ToList().Count),
                   IComparable comparable => comparable.CompareTo(anotherValue) >= 0,
                   var _                  => throw new UnsupportedTypeException(value.GetType()),
               };
    }
}
