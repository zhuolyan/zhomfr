using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must be a valid URL.
///     <remarks>
///         If you would like to specify the URL protocols that should be considered valid, you may pass the protocols
///         as validation rule parameters.
///     </remarks>
/// </summary>
/// <example>
///     <code>
///         public class UrlDto
///         {
///             [Url("https")]
///             public string StringInvalid = "http://example.com"; // ValidationError
///         
///             [Url]
///             public string StringValid = "http://example.com"; // Success
///         }
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class UrlAttribute : AbstractRule
{
    private readonly List<string> _protocols;

    /// <summary>
    ///     The field under validation must be a valid URL.
    ///     <remarks>
    ///         If you would like to specify the URL protocols that should be considered valid, you may pass the protocols
    ///         as validation rule parameters.
    ///     </remarks>
    /// </summary>
    public UrlAttribute(params string[] protocols)
    {
        this._protocols = [.. protocols.Select(x => x.ToLowerInvariant())];
        this.AddListAsOneArgumentToMessageArgs([.. this._protocols]);
    }

    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        if (value is not string url) {
            throw new UnsupportedTypeException(value.GetType());
        }

        if (!Uri.TryCreate(url, UriKind.Absolute, out Uri? uri)) {
            return false;
        }

        return this._protocols.Count == 0 || this._protocols.Any(p => string.Equals(p, uri.Scheme, StringComparison.OrdinalIgnoreCase));
    }
}
