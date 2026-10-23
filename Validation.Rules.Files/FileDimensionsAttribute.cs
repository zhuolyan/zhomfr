using System.ComponentModel.DataAnnotations;

using SkiaSharp;

using Zhomfr.Helpers.SmartFile;
using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Files;

/// <summary>
///     Validates the dimensions (width and height) of an image file. Supports both exact match and range-based
///     validation.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class FileDimensionsAttribute : AbstractRule
{
    private readonly int? _height;
    private readonly int? _maxHeight;
    private readonly int? _maxWidth;
    private readonly int? _minHeight;
    private readonly int? _minWidth;
    private readonly int? _width;

    /// <summary>Initializes a new instance for exact dimension matching.</summary>
    /// <param name="width">The exact required width in pixels.</param>
    /// <param name="height">The exact required height in pixels.</param>
    public FileDimensionsAttribute(int? width = null, int? height = null)
    {
        this._width  = width;
        this._height = height;

        this.AddIfNotNull(this._height);
        this.AddIfNotNull(this._width);
    }

    /// <summary>Initializes a new instance for range-based dimension validation.</summary>
    /// <param name="minWidth">The minimum allowed width in pixels.</param>
    /// <param name="minHeight">The minimum allowed height in pixels.</param>
    /// <param name="maxWidth">The maximum allowed width in pixels.</param>
    /// <param name="maxHeight">The maximum allowed height in pixels.</param>
    public FileDimensionsAttribute(int? minWidth = null, int? minHeight = null, int? maxWidth = null, int? maxHeight = null)
    {
        this._minWidth  = minWidth;
        this._minHeight = minHeight;
        this._maxWidth  = maxWidth;
        this._maxHeight = maxHeight;

        this.AddIfNotNull(this._minWidth);
        this.AddIfNotNull(this._minHeight);
        this.AddIfNotNull(this._maxWidth);
        this.AddIfNotNull(this._maxHeight);
    }

    private void AddIfNotNull(int? value)
    {
        if (value.HasValue) {
            this.MessageArgs.Add(value.Value);
        }
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        if (value is not SmartFile file) {
            throw new UnsupportedTypeException(value.GetType());
        }

        try {
            using Stream  stream = file.GetStream();
            using SKCodec codec  = SKCodec.Create(stream);

            return FileDimensionsAttribute.IsDimensionValid(codec.Info.Width, this._width, this._minWidth, this._maxWidth)
                && FileDimensionsAttribute.IsDimensionValid(codec.Info.Height, this._height, this._minHeight, this._maxHeight);
        } catch {
            return false;
        }
    }

    private static bool IsDimensionValid(int actual, int? exact, int? min, int? max)
    {
        if (exact.HasValue) {
            return actual == exact.Value;
        }

        if (actual < min) {
            return false;
        }

        return actual <= max;
    }
}
