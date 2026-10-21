using System.Collections;
using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>Under validation must not have any duplicate values:</summary>
/// <example>
///     <code>
///         public class DistinctDto
///         {
///             [Distinct]
///             public IDictionary DictionaryInvalid = new Dictionary<string, string>
///             { { "first", "test" }, { "second",
///             "test" }, { "third", "blah" } }; // ValidationError [Distinct] public IDictionary DictionaryValid = new
///             Dictionary
///             <string, string>
///                 { { "first", "test" }, { "second", "not" }, { "third", "blah" } }; // Success [Distinct]
///                 public IEnumerable EnumerableInvalid = new List
///                 <int>
///                     { 1, 2, 2 }; // ValidationError [Distinct] public IEnumerable EnumerableValid = new List
///                     <int> { 1, 2, 3 }; // Success }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class DistinctAttribute : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value switch
               {
                   IDictionary dictionary => DistinctAttribute.IsDistinct(dictionary.Values),
                   IEnumerable enumerable => DistinctAttribute.IsDistinct(enumerable),
                   var _                  => throw new UnsupportedTypeException(value.GetType()),
               };
    }

    private static bool IsDistinct(IEnumerable value)
    {
        HashSet<object?> observedItems = [];

        foreach (object? element in value) {
            if (!observedItems.Add(element)) {
                return false;
            }
        }

        return true;
    }
}
