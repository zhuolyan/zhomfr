using System.ComponentModel.DataAnnotations;
using System.Text.Json;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be a valid JSON string.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class JsonAttribute : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        if (value is not string str) {
            throw new UnsupportedTypeException(value.GetType());
        }

        try {
            JsonDocument.Parse(str);

            return true;
        } catch (Exception) {
            return false;
        }
    }
}
