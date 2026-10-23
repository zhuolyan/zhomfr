using System.ComponentModel.DataAnnotations;

using SkiaSharp;

using Zhomfr.Helpers.SmartFile;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Files;

/// <summary>The file under validation must be an image (bmp, gif, ico, jpeg, png, wbmp, webp).</summary>
/// <param name="allowSvg">If set to <c>true</c>, SVG files will be accepted after XML validation.</param>
/// <remarks>
///     By default, the image rule does not allow SVG files due to the possibility of XSS vulnerabilities. If you need
///     to allow SVG files, you may provide the allowSvg directive to the  rule (true).
/// </remarks>
/// <example>
///     <code>
///         public class Dto
///         {
///             [FileImage()]
///             public SmartFile SomeImageFile;
/// 
///             [FileImage(true)]
///             public SmartFile SomeSvgFile;
///         }
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class FileImageAttribute(bool allowSvg = false) : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        if (value is not SmartFile file) {
            throw new UnsupportedTypeException(value.GetType());
        }

        if (allowSvg && file.Extension.Equals("svg", StringComparison.CurrentCultureIgnoreCase)) {
            return true;
        }

        using Stream stream = file.GetStream();

        using SKCodec? codec = SKCodec.Create(stream);

        return codec != null;
    }
}
