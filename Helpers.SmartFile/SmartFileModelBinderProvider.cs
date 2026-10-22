using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Zhomfr.Helpers.SmartFile;

/// <inheritdoc/>
public class SmartFileModelBinderProvider : IModelBinderProvider
{
    /// <inheritdoc/>
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        return context.Metadata.ModelType == typeof(SmartFile) ? new BinderTypeModelBinder(typeof(SmartFileModelBinder)) : null;
    }
}
