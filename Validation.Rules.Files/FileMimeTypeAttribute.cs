using System.ComponentModel.DataAnnotations;

using Zhomfr.Helpers.SmartFile;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Files;

/// <summary>The file under validation must match one of the given MIME types.</summary>
/// <remarks>To determine the MIME type of the uploaded file, the file's contents will be read and the framework will attempt to guess the MIME type, which may be different from the client's provided MIME type.</remarks>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class FileMimeTypeAttribute : AbstractRule
{
    private readonly string[] _types;

    /// <summary>Validates that the file's MIME type (Content-Type) matches one of the specified allowed media types.</summary>
    /// <remarks>To determine the MIME type of the uploaded file, the file's contents will be read and the framework will attempt to guess the MIME type, which may be different from the client's provided MIME type.</remarks>
    /// <param name="types">Expected mime types. Allows concrete MIME type like ["image/jpeg", "application/pdf"] or extensions ["jpg", "bmp", "png"](</param>
    public FileMimeTypeAttribute(string[] types)
    {
        this._types = types;

        this.AddListAsOneArgumentToMessageArgs([.. this._types]);
    }

    /// <inheritdoc />
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        if (value is not SmartFile file) {
            throw new UnsupportedTypeException(value.GetType());
        }

        return this._types.Any(type => file.ContentType.Equals(type, StringComparison.OrdinalIgnoreCase)
                                    || file.Extension.Equals(type, StringComparison.OrdinalIgnoreCase));
    }
}
