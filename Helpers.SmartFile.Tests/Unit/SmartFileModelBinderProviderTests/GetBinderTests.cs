using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

using Moq;

using Zhomfr.Helpers.SmartFile.Tests.Stubs;

namespace Zhomfr.Helpers.SmartFile.Tests.Unit.SmartFileModelBinderProviderTests;

[TestFixture]
public class GetBinderTests
{
    [SetUp]
    public void SetUp()
    {
        this._provider = new();
    }

    private SmartFileModelBinderProvider _provider;

    [Test]
    public void Should_ReturnBinderTypeModelBinder_When_ModelTypeIsStorageFile()
    {
        Mock<ModelBinderProviderContext> context = ModelBindingFixture.CreateContextMock(typeof(SmartFile));

        IModelBinder? result = this._provider.GetBinder(context.Object);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<BinderTypeModelBinder>());
    }

    [Test]
    public void Should_ReturnNull_When_ModelTypeIsNotStorageFile()
    {
        Mock<ModelBinderProviderContext> context = ModelBindingFixture.CreateContextMock(typeof(string));

        IModelBinder? result = this._provider.GetBinder(context.Object);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Should_ThrowArgumentNullException_When_ContextIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => this._provider.GetBinder(null!));
    }
}
