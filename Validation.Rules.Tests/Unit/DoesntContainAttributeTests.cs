using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Tests.Stubs;

// ReSharper disable CoVariantArrayConversion

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class DoesntContainAttributeTests
{
    private static string[] Correct    => ["test", "blah test"];
    private static string[] NotCorrect => ["exists", "wrong"];

    private const string ERROR_MESSAGE    = "The {0} must contains: {1}.";
    private const string EXPECTED_MESSAGE = "The MyProp must contains: \"exists\", \"wrong\".";
    private const string DISPLAY_NAME     = "MyProp";

    [Test]
    public void Should_ReturnSuccess_When_ValueIsDictionaryAndCorrect()
    {
        DoesntContainAttribute attribute = new(DoesntContainAttributeTests.Correct);

        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Dictionary, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsDictionaryAndNotCorrect()
    {
        DoesntContainAttribute attribute = new(DoesntContainAttributeTests.NotCorrect) { ErrorMessage = DoesntContainAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContext = new(new()) { DisplayName = DoesntContainAttributeTests.DISPLAY_NAME };

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Dictionary, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo(DoesntContainAttributeTests.EXPECTED_MESSAGE));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsCollectionAndCorrect()
    {
        DoesntContainAttribute attribute = new(DoesntContainAttributeTests.Correct);

        ValidationContext validationContext = new(new()) { DisplayName = DoesntContainAttributeTests.DISPLAY_NAME };

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Collection, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsCollectionAndNotCorrect()
    {
        DoesntContainAttribute attribute = new(DoesntContainAttributeTests.NotCorrect) { ErrorMessage = DoesntContainAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContext = new(new()) { DisplayName = DoesntContainAttributeTests.DISPLAY_NAME };

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Collection, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo(DoesntContainAttributeTests.EXPECTED_MESSAGE));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsStringAndCorrect()
    {
        DoesntContainAttribute attribute = new(DoesntContainAttributeTests.Correct);

        ValidationContext validationContext = new(new()) { DisplayName = DoesntContainAttributeTests.DISPLAY_NAME };

        ValidationResult? result = attribute.GetValidationResult("something value", validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsStringAndNotCorrect()
    {
        DoesntContainAttribute attribute = new(DoesntContainAttributeTests.NotCorrect) { ErrorMessage = DoesntContainAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContext = new(new()) { DisplayName = DoesntContainAttributeTests.DISPLAY_NAME };

        ValidationResult? result = attribute.GetValidationResult("test wrong", validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo(DoesntContainAttributeTests.EXPECTED_MESSAGE));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        DoesntContainAttribute attribute         = new(DoesntContainAttributeTests.NotCorrect);
        ValidationContext      validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        DoesntContainAttribute attribute         = new(DoesntContainAttributeTests.NotCorrect);
        ValidationContext      validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnSuccess_When_ExpectedEmpty()
    {
        DoesntContainAttribute attribute = new();

        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Collection, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }
}
