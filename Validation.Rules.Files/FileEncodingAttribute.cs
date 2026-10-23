using System.ComponentModel.DataAnnotations;
using System.Text;

using Zhomfr.Helpers.SmartFile;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Files;

/// <summary>
///     Validates that the file content matches the specified character encoding. Equivalent to Laravel's 'encoding'
///     rule (uses strict decoder validation).
/// </summary>
/// <remarks>This rule read all data form file.</remarks>
/// <remarks>
///     This rule relies on <see cref="Encoding"/>. Available encodings depend on the encoding providers registered in
///     your application (such as <see cref="CodePagesEncodingProvider"/>).
/// </remarks>
/// <example>
///     <code>
///         public class Dto
///         {
///             [FileEncoding("ASCII")]
///             public SmartFile SomeFile;
///         }
///     </code>
/// </example>
public class FileEncodingAttribute : AbstractRule
{
    private readonly string _encodingName;

    /// <summary>
    ///     Validates that the file content matches the specified character encoding. Equivalent to Laravel's 'encoding'
    ///     rule (uses strict decoder validation).
    /// </summary>
    /// <param name="encodingName">The name of the encoding to verify (e.g., "UTF-8", "ASCII", "windows-1251").</param>
    public FileEncodingAttribute(string encodingName)
    {
        this._encodingName = encodingName;

        this.MessageArgs.Add(encodingName);
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        if (value is not SmartFile file) {
            throw new UnsupportedTypeException(value.GetType());
        }

        try {
            Encoding targetEncoding = Encoding.GetEncoding(this._encodingName, EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);

            using Stream stream = file.GetStream();

            char[] buffer = new char[4096];

            using StreamReader reader = new(stream, targetEncoding, false);

            while (!reader.EndOfStream) {
                reader.Read(buffer, 0, buffer.Length);
            }

            return true;
        } catch {
            return false;
        }
    }
}
