using System.ComponentModel.DataAnnotations;

using Zhomfr.Helpers.SmartFile;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Files;

/// <summary>Validates that the file size (in bytes) does not below the specified minimum limit.</summary>
/// <example>
///     <code>
///         public class Dto
///         {
///             [FileMin(4_194_304)]
///             public SmartFile SomeFile;
///         }
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class FileMinAttribute : AbstractRule
{
    private readonly int _min;

    /// <summary>Validates that the file size (in bytes) does not below the specified minimum limit.</summary>
    /// <param name="min">The _minimum allowed file size in bytes (inclusive).</param>
    public FileMinAttribute(int min)
    {
        this._min = min;

        this.MessageArgs.Add(this._min);
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        if (value is not SmartFile file) {
            throw new UnsupportedTypeException(value.GetType());
        }

        return file.Length >= this._min;
    }
}
