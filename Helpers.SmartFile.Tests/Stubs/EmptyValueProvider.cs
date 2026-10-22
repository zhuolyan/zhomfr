using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Zhomfr.Helpers.SmartFile.Tests.Stubs;

public class EmptyValueProvider : IValueProvider
{
    public bool ContainsPrefix(string prefix)
    {
        return false;
    }

    public ValueProviderResult GetValue(string key)
    {
        return ValueProviderResult.None;
    }
}
