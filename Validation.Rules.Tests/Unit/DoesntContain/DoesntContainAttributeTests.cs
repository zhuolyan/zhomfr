using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;
using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.DoesntContain;

[TestFixture]
public class DoesntContainAttributeTests
{
    public static List<object> TestCases = [DictionaryStubs.Default, StringStubs.CONTAINS, EnumerableStubs.ContainsEnumeration];

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        DoesntContainAttribute attribute         = new("test", "blah test");
        ValidationContext      validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        DoesntContainAttribute attribute         = new("test", "blah test");
        ValidationContext      validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void Should_ThrowArgumentException_When_ExpectedIsEmpty()
    {
        DoesntContainAttribute attribute         = new();
        ValidationContext      validationContext = new(new());

        Assert.Throws<ArgumentException>(() => attribute.GetValidationResult(DictionaryStubs.Default, validationContext));
    }

    [TestCaseSource(typeof(DoesntContainAttributeTests), nameof(DoesntContainAttributeTests.TestCases))]
    public void Should_ReturnSuccess_When_IsCorrectValue(object value)
    {
        DoesntContainAttribute attribute         = new("value", "sharp");
        ValidationContext      validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCaseSource(typeof(DoesntContainAttributeTests), nameof(DoesntContainAttributeTests.TestCases))]
    public void Should_ReturnValidationError_When_IsIncorrectValue(object value)
    {
        DoesntContainAttribute attribute         = new("test", "blah") { ErrorMessage = "The {0} must doesnt contains: {1}." };
        ValidationContext      validationContext = new(new()) { DisplayName           = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must doesnt contains: \"test\", \"blah\"."));
    }
}
