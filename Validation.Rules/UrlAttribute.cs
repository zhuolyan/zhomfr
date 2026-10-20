using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class UrlAttribute : AbstractRule
{
    private readonly List<string> _protocols;

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
