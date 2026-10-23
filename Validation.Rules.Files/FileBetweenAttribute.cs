using System.ComponentModel.DataAnnotations;

using Zhomfr.Helpers.SmartFile;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Files;

/// <summary>Validates that the file size (in bytes) falls within the specified inclusive range.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class FileBetweenAttribute : AbstractRule
{
    private readonly int _max;
    private readonly int _min;

    /// <summary>Validates that the file size (in bytes) falls within the specified inclusive range.</summary>
    /// <param name="min">The minimum allowed file size in bytes (inclusive).</param>
    /// <param name="max">The maximum allowed file size in bytes (inclusive).</param>
    public FileBetweenAttribute(int min, int max)
    {
        this._min = min;
        this._max = max;
        
        this.MessageArgs.Add(min);
        this.MessageArgs.Add(max);
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        if (value is not SmartFile file) {
            throw new UnsupportedTypeException(value.GetType());
        }

        return file.Length >= this._min && file.Length <= this._max;
    }
}
