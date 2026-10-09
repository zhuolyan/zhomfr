using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class SizeAttributeTests
{
    [TestCase("blah")]
    [TestCase("blaH")]
    public void Should_ReturnSuccess_When_ValueIsStringAndCorrect(string value)
    {
        SizeAttribute attribute = new(4);

        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("bla")]
    [TestCase("blahb")]
    public void Should_ReturnValidationError_When_ValueIsStringAndNotCorrect(string value)
    {
        SizeAttribute attribute = new(4) { ErrorMessage = "The {0} length must be {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp length must be 4."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsDictionaryAndCorrect()
    {
        SizeAttribute attribute = new(3);

        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Dictionary, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsDictionaryAndNotCorrect()
    {
        SizeAttribute attribute = new(4) { ErrorMessage = "The {0} length must be {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Dictionary, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp length must be 4."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsCollectionAndCorrect()
    {
        SizeAttribute attribute = new(3);

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Collection, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsCollectionAndNotCorrect()
    {
        SizeAttribute attribute = new(4) { ErrorMessage = "The {0} length must be {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Collection, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp length must be 4."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsEnumerationAndCorrect()
    {
        SizeAttribute attribute = new(3);

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.GetEnumerable(3), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsEnumerationAndNotCorrect()
    {
        SizeAttribute attribute = new(4) { ErrorMessage = "The {0} length must be {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.GetEnumerable(3), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp length must be 4."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        SizeAttribute     attribute         = new(3);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsUnhandledType()
    {
        SizeAttribute     attribute         = new(3);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(123, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The field Object is invalid."));
    }
}
