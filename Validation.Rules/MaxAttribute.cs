using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class MaxAttribute : AbstractRule
{
    string _max;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="max"></param>
    public MaxAttribute(string max)
    {
        this._max = max;
    }
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        throw new NotImplementedException();
    }
}
