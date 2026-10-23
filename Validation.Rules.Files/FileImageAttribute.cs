using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Xml;

using SkiaSharp;

using Zhomfr.Helpers.SmartFile;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Files;

/// <summary>The file under validation must be an image (bmp, gif, ico, jpeg, png, wbmp, webp, pkm, ktx, astc, dng, heif, avif, jpegxl).</summary>
/// <param name="allowSvg">If set to <c>true</c>, SVG files will be accepted after XML validation.</param>
/// <remarks>
///     By default, the image rule does not allow SVG files due to the possibility of XSS vulnerabilities. If you need
///     to allow SVG files, you may provide the allowSvg directive to the  rule (true).
/// </remarks>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class FileImageAttribute(bool allowSvg = false) : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        if (value is not SmartFile file) {
            throw new UnsupportedTypeException(value.GetType());
        }

        string fileExtension = file.Extension.ToLower();

        if (allowSvg && (fileExtension == ".svg" || file.ContentType.ToLower() == "image/svg+xml")) {
            return FileImageAttribute.ValidateSvg(file);
        }

        using Stream stream = file.GetStream();
            
        using SKCodec? codec  = SKCodec.Create(stream);

        return codec != null;
    }

    /// <summary>
    ///     Performs security-focused validation of an SVG file. Checks if it's valid XML and doesn't contain obvious
    ///     script tags.
    /// </summary>
    private static bool ValidateSvg(SmartFile file)
    {
        try {
            using Stream stream = file.GetStream();

            XmlReaderSettings settings = new() { DtdProcessing = DtdProcessing.Prohibit, XmlResolver = null };

            using XmlReader reader = XmlReader.Create(stream, settings);

            while (reader.Read()) {
                if (reader.NodeType == XmlNodeType.Element && string.Equals(reader.Name, "script", StringComparison.OrdinalIgnoreCase)) {
                    return false;
                }
            }

            return true;
        } catch {
            return false;
        }
    }
}
