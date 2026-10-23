using System.ComponentModel.DataAnnotations;

using Zhomfr.Helpers.SmartFile;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Files;

/// <summary>The file under validation must have a MIME type corresponding to one of the listed extensions.</summary>
/// <remarks>
///     Even though you only need to specify the extensions, this rule actually validates the MIME type of the file by
///     reading the file's contents and guessing its MIME type.
/// </remarks>
/// <example>
///     <code>
///         public class Dto
///         {
///             [FileExtension("jpg", "bmp", "png")]
///             public SmartFile SomeFile;
///         }
///     </code>
/// </example>
public class FileExtensionAttribute : AbstractRule
{
    private readonly string[] _extensions;

    /// <summary>The file under validation must have a MIME type corresponding to one of the listed extensions.</summary>
    /// <remarks>
    ///     Even though you only need to specify the extensions, this rule actually validates the MIME type of the file by
    ///     reading the file's contents and guessing its MIME type.
    /// </remarks>
    /// <remarks>File type detection is powered by libmagic.</remarks>
    /// <param name="extensions">Expected mime types.</param>
    public FileExtensionAttribute(params string[] extensions)
    {
        this._extensions = extensions;

        this.AddListAsOneArgumentToMessageArgs([.. this._extensions]);
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        if (value is not SmartFile file) {
            throw new UnsupportedTypeException(value.GetType());
        }

        return this._extensions.Any(type => file.Extension.Equals(type, StringComparison.OrdinalIgnoreCase));
    }
}
