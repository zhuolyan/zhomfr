using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class DeclinedAttributeTests
{
    [TestCase("no")]
    [TestCase("off")]
    [TestCase("false")]
    [TestCase("0")]
    [TestCase(0)]
    [TestCase(false)]
    public void Should_ReturnSuccess_When_TestedValueIsDeclinable(object value)
    {
        DeclinedAttribute attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("some text")]
    [TestCase("yes")]
    [TestCase("on")]
    [TestCase("true")]
    [TestCase("2")]
    [TestCase("1")]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(true)]
    public void Should_ReturnFailure_When_TestedValueIsNotDeclinable(object value)
    {
        DeclinedAttribute attribute = new() { ErrorMessage = "The {0} must be declined." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must be declined."));
        }
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        DeclinedAttribute attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        DeclinedAttribute attribute         = new();
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(new List<string>(), validationContext));
    }
}
