using System.Collections;
using System.ComponentModel.DataAnnotations;

using Zhomfr.Helpers.ScalarTypes;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must be greater than the given field. The two fields must be of the same type.
///     Strings, numerics, collections are evaluated using the same conventions as the size rule.
/// </summary>
/// <example>
///     <code>
///         public class GreaterThanDto
///         {
///             [GreaterThan("CollectionInvalidSecond")]
///             public ICollection CollectionInvalid = new List<int>
///             { 1, 2, 3 }; // ValidationError public ICollection
///             CollectionInvalidSecond = new List
///             <int>
///                 { 1, 2, 3 }; [GreaterThan("CollectionValidSecond")] public ICollection CollectionValid = new List
///                 <int>
///                     { 1, 2, 3 }; // Success public ICollection CollectionValidSecond = new List
///                     <int>
///                         { 1, 2 }; [GreaterThan("DateTimeInvalidSecond")] public DateTime DateTimeInvalid =
///                         DateTime.Parse("2026-10-18 07:00:00"); // ValidationError public DateTime DateTimeInvalidSecond
///                         = DateTime.Parse("2026-10-18 07:00:00"); [GreaterThan("DateTimeOffsetInvalidSecond")] public
///                         DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:0"); //
///                         ValidationError public DateTimeOffset DateTimeOffsetInvalidSecond =
///                         DateTimeOffset.Parse("2026-10-18 07:00:00"); [GreaterThan("DateTimeOffsetValidSecond")] public
///                         DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:01"); // Success
///                         public DateTimeOffset DateTimeOffsetValidSecond = DateTimeOffset.Parse("2026-10-18 07:00:00");
///                         [GreaterThan("DateTimeValidSecond")] public DateTime DateTimeValid = DateTime.Parse("2026-10-18
///                         07:00:01"); // Success public DateTime DateTimeValidSecond = DateTime.Parse("2026-10-18
///                         07:00:00"); [GreaterThan("DecimalInvalidSecond")] public decimal DecimalInvalid = 3.3M; //
///                         ValidationError public decimal DecimalInvalidSecond = 3.3M; [GreaterThan("DecimalValidSecond")]
///                         public decimal DecimalValid = 3.3M; // Success public decimal DecimalValidSecond = 3.29999M;
///                         [GreaterThan("DoubleInvalidSecond")] public double DoubleInvalid = 3.3; // ValidationError
///                         public double DoubleInvalidSecond = 3.3; [GreaterThan("DoubleValidSecond")] public double
///                         DoubleValid = 3.3; // Success public double DoubleValidSecond = 3.2999;
///                         [GreaterThan("EnumerableInvalidSecond")] public IEnumerable EnumerableInvalid = new List
///                         <int>
///                             { 1, 2, 3 }; // ValidationError public IEnumerable EnumerableInvalidSecond = new List
///                             <int>
///                                 { 1, 2, 3 }; [GreaterThan("EnumerableValidSecond")] public IEnumerable
///                                 EnumerableValid = new List
///                                 <int>
///                                     { 1, 2, 3 }; // Success public IEnumerable EnumerableValidSecond = new List
///                                     <int>
///                                         { 1, 2 }; [GreaterThan("FloatInvalidSecond")] public float FloatInvalid =
///                                         3.3f; // ValidationError public float FloatInvalidSecond = 3.3f;
///                                         [GreaterThan("FloatValidSecond")] public float FloatValid = 3.3f; // Success
///                                         public float FloatValidSecond = 3.29999f; [GreaterThan("IntInvalidSecond")]
///                                         public int IntInvalid = 1; // ValidationError public int IntInvalidSecond = 1;
///                                         [GreaterThan("IntValidSecond")] public int IntValid = 1; // Success public int
///                                         IntValidSecond = 0; [GreaterThan("LongInvalidSecond")] public long LongInvalid
///                                         = 1; // ValidationError public long LongInvalidSecond = 1;
///                                         [GreaterThan("LongValidSecond")] public long LongValid = 1; // Success public
///                                         long LongValidSecond = 0; [GreaterThan("ShortInvalidSecond")] public short
///                                         ShortInvalid = 1; // ValidationError public short ShortInvalidSecond = 1;
///                                         [GreaterThan("ShortValidSecond")] public short ShortValid = 1; // Success
///                                         public short ShortValidSecond = 0; [GreaterThan("StringInvalidSecond")] public
///                                         string StringInvalid = "something"; // ValidationError public string
///                                         StringInvalidSecond = "something"; [GreaterThan("StringValidSecond")] public
///                                         string StringValid = "something"; // Success public string StringValidSecond =
///                                         "Somethin"; [GreaterThan("UintInvalidSecond")] public uint UintInvalid = 1; //
///                                         ValidationError public uint UintInvalidSecond = 1;
///                                         [GreaterThan("UintValidSecond")] public uint UintValid = 1; // Success public
///                                         uint UintValidSecond = 0; [GreaterThan("UlongInvalidSecond")] public ulong
///                                         UlongInvalid = 1; // ValidationError public ulong UlongInvalidSecond = 1;
///                                         [GreaterThan("UlongValidSecond")] public ulong UlongValid = 1; // Success
///                                         public ulong UlongValidSecond = 0; [GreaterThan("UshortInvalidSecond")] public
///                                         ushort UshortInvalid = 1; // ValidationError public ushort UshortInvalidSecond
///                                         = 1; [GreaterThan("UshortValidSecond")] public ushort UshortValid = 1; //
///                                         Success public ushort UshortValidSecond = 0; }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class GreaterThanAttribute : AbstractRule
{
    private readonly string _field;
    private          bool   _cached;

    /// <summary>
    ///     The field under validation must be greater than the given field. The two fields must be of the same type.
    ///     Strings, numerics, collections are evaluated using the same conventions as the size rule.
    /// </summary>
    /// <param name="field">Field name.</param>
    public GreaterThanAttribute(string field)
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
                   string str             => str.Length       > ((string)anotherValue).Length,
                   ICollection collection => collection.Count > ((ICollection)anotherValue).Count,
                   IEnumerable collection => collection.CountIsGreatThan(((IEnumerable)anotherValue).Cast<object>().ToList().Count),
                   IComparable comparable => comparable.CompareTo(anotherValue) > 0,
                   var _                  => throw new UnsupportedTypeException(value.GetType()),
               };
    }
}
