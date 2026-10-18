using System.Collections;
using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must be a IDictionary whose keys are all included in the given list. At least one
///     key must be provided.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class DictionaryKeysAttribute(params object[] keys) : ContainsAttribute(keys)
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value is IDictionary dictionary ? this.IsContains(dictionary.Keys) : throw new UnsupportedTypeException(value.GetType());
    }
}
