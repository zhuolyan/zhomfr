using System.ComponentModel.DataAnnotations;

using Zhomfr.Helpers.SmartFile;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Files;

/// <summary>Validates that the file extension matches the specified allowed extensions.</summary>
/// <remarks>
///     You should never rely on validating a file by its user-assigned extension alone. This rule should typically
///     always be used in combination with the mimetypes rules.
/// </remarks>
public class FileExtensionAttribute : AbstractRule
{
    private readonly string[] _extensions;

    /// <summary>Validates that the file extension matches the specified allowed extensions.</summary>
    /// <param name="extensions">An array of allowed file extensions (e.g., [".jpg", ".png"]).</param>
    /// <remarks>
    ///     You should never rely on validating a file by its user-assigned extension alone. This rule should typically
    ///     always be used in combination with the mimetypes rules.
    /// </remarks>
    public FileExtensionAttribute(string[] extensions)
    {
        this._extensions = extensions;

        this.AddListAsOneArgumentToMessageArgs([.. this._extensions]);
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value is not SmartFile file
                   ? throw new UnsupportedTypeException(value.GetType())
                   : this._extensions.Any(extension => string.Equals(extension, file.Extension, StringComparison.OrdinalIgnoreCase));
    }
}
