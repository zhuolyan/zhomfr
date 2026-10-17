using System.ComponentModel.DataAnnotations;

using Zhomfr.Helpers.ScalarTypes;
using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class DigitsAttribute : AbstractRule
{
    private readonly bool _isBetween;
    private readonly int  _max;
    private readonly int  _min;
    private readonly int  _size;

    /// <summary>The number under validation must have an exact length of value.</summary>
    /// <param name="size"></param>
    public DigitsAttribute(int size)
    {
        this._size      = size;
        this._isBetween = false;
    }

    /// <summary>The number under validation must have a length between the given min and max.</summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    public DigitsAttribute(int min, int max)
    {
        this._min       = min;
        this._max       = max;
        this._isBetween = false;
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        try {
            decimal number = Convert.ToDecimal(value);
            int     count  = number.GetDigits();

            return this._isBetween ? count == this._size : count >= this._min && count <= this._max;
        } catch (Exception e) {
            throw new UnsupportedTypeException(e.Message);
        }
    }
}
