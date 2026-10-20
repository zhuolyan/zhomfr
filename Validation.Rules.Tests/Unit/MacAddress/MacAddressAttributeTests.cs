using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Tests.Unit.MacAddress;

[TestFixture]
public class MacAddressAttributeTests
{
    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        MacAddressAttribute attribute         = new();
        ValidationContext   validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        MacAddressAttribute attribute         = new();
        ValidationContext   validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("00-11-22-33-44-55")]
    [TestCase("AC:DE:48:23:45:67")]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string input)
    {
        MacAddressAttribute attribute = new();

        ValidationContext validationContext = new(new());

        ValidationResult? result1 = attribute.GetValidationResult(input, validationContext);

        Assert.That(result1, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("GG:11:22:33:44:55")]
    [TestCase("192.168.1.1")]
    public void Should_ReturnValidationError_When_ValueIsInvalid(string input)
    {
        MacAddressAttribute attribute = new() { ErrorMessage = "The {0} must be correct MAC Address." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must be correct MAC Address."));
    }
}
