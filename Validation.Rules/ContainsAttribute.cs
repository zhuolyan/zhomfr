using System.Collections;
using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be a collection or string that contains all the given parameter values.</summary>
/// <target-type>string,ICollection,IEnumerable</target-type>
/// <example>
///     <code>
///         public class ContainsDto
///         {
///             [Contains(1, 2)]
///             public ICollection CollectionInvalid = new List&lt;int&gt;() { 3, 4, 5 }; // ValidationError
///         
///             [Contains(1, 2)]
///             public ICollection CollectionValid = new List&lt;int&gt;() { 1, 2, 4 }; // Success
///         
///             [Contains("2", "3")]
///             public IEnumerable EnumerableInvalid = new List&lt;string&gt;() { "something", "else" }; // ValidationError
///         
///             [Contains("something", "else")]
///             public IEnumerable EnumerableValid = new List&lt;string&gt;() { "something", "else" }; // Success
///         
///             [Contains("test", "not")]
///             public string StringInvalid = "something"; // ValidationError
///         
///             [Contains("some", "thin")]
///             public string StringValid = "something"; // Success
///         }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class ContainsAttribute : AbstractRule
{
    private readonly HashSet<object> _requiredItems;

    /// <summary>The field under validation must be a collection or string that contains all the given parameter values.</summary>
    /// <param name="items">The seek values.</param>
    /// <exception cref="ArgumentException">If items is empty.</exception>
    public ContainsAttribute(params object[] items)
    {
        if (items.Length == 0) {
            throw new ArgumentException($"{nameof(items)} is empty");
        }

        this._requiredItems = [.. items];
        this.AddListAsOneArgumentToMessageArgs(items);
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value switch
               {
                   string str             => this._requiredItems.All(e => str.Contains((string)e)),
                   IDictionary dictionary => this.IsContains(dictionary.Values),
                   IEnumerable enumerable => this.IsContains(enumerable),
                   var _                  => throw new UnsupportedTypeException(value.GetType()),
               };
    }

    /// <summary>Check if enumerable contains value.</summary>
    /// <param name="value">Expected value.</param>
    /// <returns><c>true</c> if the value exists in enumerable otherwise <c>false</c>.</returns>
    protected bool IsContains(IEnumerable value)
    {
        foreach (object? element in value) {
            this._requiredItems.Remove(element!);

            if (this._requiredItems.Count == 0) {
                return true;
            }
        }

        return false;
    }
}
