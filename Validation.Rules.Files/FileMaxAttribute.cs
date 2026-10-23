using System.ComponentModel.DataAnnotations;

using Zhomfr.Helpers.SmartFile;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Files;

/// <summary>Validates that the file size (in bytes) does not exceed the specified maximum limit.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class FileMaxAttribute : AbstractRule
{
    private readonly int _max;

    /// <summary>Validates that the file size (in bytes) does not exceed the specified maximum limit.</summary>
    /// <param name="max">The maximum allowed file size in bytes (inclusive).</param>
    public FileMaxAttribute(int max)
    {
        this._max = max;

        this.MessageArgs.Add(this._max);
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        if (value is not SmartFile file) {
            throw new UnsupportedTypeException(value.GetType());
        }
        
        return file.Length <= this._max;
    }
}
