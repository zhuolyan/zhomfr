using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must end with one of the given values.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class EndsWithAttribute : AbstractRule
{
    private readonly List<string> _expected;

    /// <summary>The field under validation must end with one of the given values.</summary>
    /// <param name="items">Values what must be at end of string.</param>
    /// <exception cref="ArgumentException">If items is empty.</exception>
    public EndsWithAttribute(params string[] items)
    {
        if (items.Length == 0) {
            throw new ArgumentException($"{nameof(items)} is empty");
        }

        this.AddListAsOneArgumentToMessageArgs([.. items]);
        this._expected = [.. items];
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value is string str ? this._expected.Any(i => str.EndsWith(i)) : throw new UnsupportedTypeException(value.GetType());
    }
}
