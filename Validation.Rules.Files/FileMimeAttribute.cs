using System.ComponentModel.DataAnnotations;

using Zhomfr.Helpers.SmartFile;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Files;

/// <summary>The file under validation must match one of the given MIME types.</summary>
/// <remarks>
///     To determine the MIME type of the uploaded file, the file's contents will be read and the framework will
///     attempt to guess the MIME type, which may be different from the client's provided MIME type.
/// </remarks>
/// <example>
///     <code>
///         public class Dto
///         {
///             [FileMime("video/avi", "video/mpeg", "video/quicktime")]
///             public SmartFile SomeFile;
///         }
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class FileMimeAttribute : AbstractRule
{
    private readonly string[] _types;

    /// <summary>The file under validation must match one of the given MIME types.</summary>
    /// <remarks>
    ///     To determine the MIME type of the uploaded file, the file's contents will be read and the framework will
    ///     attempt to guess the MIME type, which may be different from the client's provided MIME type.
    /// </remarks>
    /// <param name="types">Expected mime types.</param>
    public FileMimeAttribute(params string[] types)
    {
        this._types = types;

        this.AddListAsOneArgumentToMessageArgs([.. this._types]);
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        if (value is not SmartFile file) {
            throw new UnsupportedTypeException(value.GetType());
        }

        return this._types.Any(type => file.ContentType.Equals(type, StringComparison.OrdinalIgnoreCase));
    }
}
