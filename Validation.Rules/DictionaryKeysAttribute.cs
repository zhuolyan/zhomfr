using System.Collections;
using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must be a Dictionary whose keys are all included in the given list. At least one
///     key must be provided.
/// </summary>
/// <param name="keys">The seek values.</param>
/// <target-type>IDictionary</target-type>
/// <example>
///     <code>
///         public class DictionaryKeysDto
///         {
///             [DictionaryKeys("first", "second")]
///             public IDictionary DictionaryInvalid = new Dictionary&lt;string, string&gt;() { { "first", "test" }, { "second", "not" }, { "third", "blah" } }; // ValidationError
///
///             [DictionaryKeys("first", "test")]
///             public IDictionary DictionaryValid = new Dictionary&lt;string, string&gt;() { { "first", "test" }, { "second", "not" }, { "third", "blah" } }; // Success
///         }
///
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class DictionaryKeysAttribute(params object[] keys) : ContainsAttribute(keys)
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value is IDictionary dictionary ? this.IsContains(dictionary.Keys) : throw new UnsupportedTypeException(value.GetType());
    }
}
