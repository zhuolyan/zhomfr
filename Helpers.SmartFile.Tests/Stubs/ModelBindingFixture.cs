using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

using Moq;

namespace Zhomfr.Helpers.SmartFile.Tests.Stubs;

public static class ModelBindingFixture
{
    public static DefaultModelBindingContext CreateContext(IFormFile? file, string modelName = "File")
    {
        DefaultHttpContext httpContext = new();

        FormFileCollection fileCollection = file != null ? [file] : [];

        httpContext.Request.Form = new FormCollection(new(), fileCollection);

        return new()
        {
            ActionContext = new() { HttpContext = httpContext }, ModelName = modelName, ModelState = new(), ValueProvider = new Mock<IValueProvider>().Object,
        };
    }

    public static Mock<ModelBinderProviderContext> CreateContextMock(Type modelType)
    {
        Mock<ModelMetadata>              metadata = new(ModelMetadataIdentity.ForType(modelType));
        Mock<ModelBinderProviderContext> context  = new();

        context.Setup(c => c.Metadata).Returns(metadata.Object);

        return context;
    }
}
