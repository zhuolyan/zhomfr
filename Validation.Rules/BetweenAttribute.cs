using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must have a size between the given min and max (inclusive). Strings, numerics,
///     arrays, and files are evaluated in the same fashion as the size rule.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class BetweenAttribute(string min, string max) : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return value switch
               {
                   string str          => str.Length > int.Parse(min)            && str.Length < int.Parse(max),
                   short number        => number     > short.Parse(min)          && number     < short.Parse(max),
                   ushort number       => number     > ushort.Parse(min)         && number     < ushort.Parse(max),
                   int number          => number     > int.Parse(min)            && number     < int.Parse(max),
                   uint number         => number     > uint.Parse(min)           && number     < uint.Parse(max),
                   long number         => number     > long.Parse(min)           && number     < long.Parse(max),
                   ulong number        => number     > ulong.Parse(min)          && number     < ulong.Parse(max),
                   float number        => number     > float.Parse(min)          && number     < float.Parse(max),
                   double number       => number     > double.Parse(min)         && number     < double.Parse(max),
                   decimal number      => number     > decimal.Parse(min)        && number     < decimal.Parse(max),
                   DateTime date       => date       > DateTime.Parse(min)       && date       < DateTime.Parse(max),
                   DateTimeOffset date => date       > DateTimeOffset.Parse(min) && date       < DateTimeOffset.Parse(max),
                   var _               => throw new UnsupportedTypeException(),
               };
    }
}
