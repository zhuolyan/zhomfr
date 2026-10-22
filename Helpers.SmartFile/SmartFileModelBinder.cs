using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Zhomfr.Helpers.SmartFile;

/// <inheritdoc/>
public class SmartFileModelBinder : IModelBinder
{
    /// <inheritdoc/>
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        IFormFile? file = bindingContext.ActionContext.HttpContext.Request.Form.Files.GetFile(bindingContext.ModelName);

        if (file is null) {
            bindingContext.Result = ModelBindingResult.Success(null);

            return Task.CompletedTask;
        }

        try {
            SmartFile storageFile = new(file.OpenReadStream(), file.FileName);

            bindingContext.Result = ModelBindingResult.Success(storageFile);
        } catch {
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }
}
