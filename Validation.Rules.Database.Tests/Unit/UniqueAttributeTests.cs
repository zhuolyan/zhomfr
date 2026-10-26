using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Database.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Database.Tests.Unit;

[TestFixture]
public class UniqueAttributeTests
{
    [TestCase(nameof(StubModel.Id), 2)]
    [TestCase(nameof(StubModel.Name), "Test1")]
    [TestCase(nameof(StubModel.Name), "test")]
    [TestCase(nameof(StubModel.Id), null)]
    [TestCase(nameof(StubModel.Name), null)]
    public void Should_ReturnSuccess_When_Exists(string field, object? input)
    {
        using StubSetup setup = new();

        UniqueAttribute<StubDbContext, StubModel> attribute = new(field);

        ValidationResult? result = attribute.GetValidationResult(input, setup.ValidationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase(nameof(StubModel.Id), 1)]
    [TestCase(nameof(StubModel.Name), "Test")]
    public void Should_ReturnValidationError_When_NotExists(string field, object? input)
    {
        using StubSetup setup = new();

        UniqueAttribute<StubDbContext, StubModel> attribute = new(field) { ErrorMessage = "{0} invalid. {2} exists in {1}" };

        ValidationResult? result = attribute.GetValidationResult(input, setup.ValidationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result.ErrorMessage, Is.EqualTo($"MyProp invalid. {input} exists in \"{field}\""));
    }

    [TestCase(nameof(StubModel.Id), 2)]
    [TestCase(nameof(StubModel.Name), "Test1")]
    [TestCase(nameof(StubModel.Name), "test")]
    public void Should_Throw_When_DbContextIsNull(string field, object? input)
    {
        using StubSetup setup = new();

        UniqueAttribute<StubNotExistDbContext, StubModel> attribute = new(field);

        Assert.Throws<ArgumentNullException>(() => attribute.GetValidationResult(input, setup.ValidationContext));
    }
}
