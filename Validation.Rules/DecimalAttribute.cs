using System.ComponentModel.DataAnnotations;

using Zhomfr.Helpers.ScalarTypes;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>The field under validation must be decimal and must contain the specified number of decimal places.</summary>
/// <target-type>decimal</target-type>
/// <example>
///     <code>
///         public class DecimalDto
///         {
///             [Decimal(5)]
///             public decimal DecimalInvalid = 3.3M; // ValidationError
///         
///             [Decimal(2, 5)]
///             public decimal DecimalValid = 3.345M; // Success
///         }
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class DecimalAttribute : AbstractRule
{
    private readonly int? _exactly;
    private readonly int  _max;
    private readonly int  _min;

    /// <summary>The field under validation must be decimal and must contain the specified number of decimal places.</summary>
    /// <param name="min">Expected min number of decimal places.</param>
    /// <param name="max">Expected max number of decimal places.</param>
    public DecimalAttribute(int min, int max)
    {
        this._min = min;
        this._max = max;

        this.MessageArgs.Add(min);
        this.MessageArgs.Add(max);
    }

    /// <summary>The field under validation must be decimal and must contain the specified number of decimal places.</summary>
    /// <param name="exactly">Expected number of decimal places.</param>
    public DecimalAttribute(int exactly)
    {
        this._exactly = exactly;

        this.MessageArgs.Add(exactly);
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        if (value is not decimal d) {
            throw new UnsupportedTypeException(value.GetType());
        }

        int count = d.GetDecimal();

        if (this._exactly.HasValue && count == this._exactly.Value) {
            return true;
        }

        return count >= this._min && count <= this._max;
    }
}
