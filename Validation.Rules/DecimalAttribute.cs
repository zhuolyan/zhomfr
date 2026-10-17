using System.ComponentModel.DataAnnotations;

using Zhomfr.Helpers.ScalarTypes;
using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class DecimalAttribute : AbstractRule
{
    private readonly bool _isBetween;
    private readonly int  _max;
    private readonly int  _min;
    private readonly int  _size;

    /// <summary>The field under validation must be decimal and must contain the specified number of decimal places.</summary>
    public DecimalAttribute(int size)
    {
        this._size      = size;
        this._isBetween = false;
    }

    /// <summary>The field under validation must be decimal and must have between the specified numbers of decimal places.</summary>
    public DecimalAttribute(int min, int max)
    {
        this._min       = min;
        this._max       = max;
        this._isBetween = false;
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        if (value is not decimal number) {
            throw new UnsupportedTypeException();
        }

        int count = number.GetDecimal();

        return this._isBetween ? count == this._size : count >= this._min && count <= this._max;
    }
}
