using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using Moq;

using Zhomfr.Helpers.SmartFile.Tests.Stubs;

namespace Zhomfr.Helpers.SmartFile.Tests.Unit.SmartFileModelBinderTests;

[TestFixture]
public class BindModelAsyncTests
{
    [SetUp]
    public void SetUp()
    {
        this._binder = new();
    }

    private SmartFileModelBinder _binder;

    [Test]
    public async Task Should_ReturnStorageFile_When_FileExists()
    {
        string       content  = "fake image content";
        string       fileName = "test.png";
        MemoryStream stream   = new(Encoding.UTF8.GetBytes(content));

        Mock<IFormFile> mockFile = new();
        mockFile.Setup(f => f.OpenReadStream()).Returns(stream);
        mockFile.Setup(f => f.FileName).Returns(fileName);
        mockFile.Setup(f => f.ContentType).Returns("image/png");
        mockFile.Setup(f => f.Name).Returns("Avatar");

        DefaultModelBindingContext context = ModelBindingFixture.CreateContext(mockFile.Object, "Avatar");

        await this._binder.BindModelAsync(context);

        Assert.That(context.Result.IsModelSet, Is.True);
        Assert.That(context.Result.Model, Is.InstanceOf<SmartFile>());

        SmartFile resultModel = (SmartFile)context.Result.Model!;
        Assert.That(resultModel.FileName, Is.EqualTo(fileName));
    }

    [Test]
    public async Task Should_ReturnNull_When_FileIsMissing()
    {
        DefaultModelBindingContext context = ModelBindingFixture.CreateContext(null, "Avatar");

        await this._binder.BindModelAsync(context);

        Assert.That(context.Result.IsModelSet, Is.True);
        Assert.That(context.Result.Model, Is.Null);
    }

    [Test]
    public async Task Should_SetFailedResult_When_ExceptionOccursDuringBinding()
    {
        Mock<IFormFile> mockFile = new();
        mockFile.Setup(f => f.OpenReadStream()).Throws(new Exception("Stream error"));
        mockFile.Setup(f => f.Name).Returns("Avatar");

        DefaultModelBindingContext context = ModelBindingFixture.CreateContext(mockFile.Object, "Avatar");

        await this._binder.BindModelAsync(context);

        Assert.That(context.Result, Is.EqualTo(ModelBindingResult.Failed()));
    }
}
