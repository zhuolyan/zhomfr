using System.ComponentModel.DataAnnotations;
using System.Net;

using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Tests.Unit.Ip;

[TestFixture]
public class IpAttributeTests
{
    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        IpAttribute       attribute         = new();
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        IpAttribute       attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("192.168.1.1")]
    [TestCase("8.8.8.8")]
    [TestCase("0.0.0.0")]
    [TestCase("192.168.1.123")]
    [TestCase("2001:0db8:85a3:0000:0000:8a2e:0370:7334")]
    [TestCase("2001:db8::1")]
    [TestCase("::1")]
    public void Should_ReturnSuccess_When_ValueIsCorrectAndModeAny(string input)
    {
        IpAttribute attribute1 = new();
        IpAttribute attribute2 = new();

        ValidationContext validationContext = new(new());

        ValidationResult? result1 = attribute1.GetValidationResult(input, validationContext);
        ValidationResult? result2 = attribute2.GetValidationResult(IPAddress.Parse(input), validationContext);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result1, Is.EqualTo(ValidationResult.Success));
            Assert.That(result2, Is.EqualTo(ValidationResult.Success));
        }
    }

    [TestCase("192.168.1.1")]
    [TestCase("8.8.8.8")]
    [TestCase("0.0.0.0")]
    [TestCase("192.168.1.123")]
    public void Should_ReturnSuccess_When_ValueIsCorrectAndModeIpV4(string input)
    {
        IpAttribute attribute1 = new(IpAttribute.Modes.IPv4);
        IpAttribute attribute2 = new(IpAttribute.Modes.IPv4);

        ValidationContext validationContext = new(new());

        ValidationResult? result1 = attribute1.GetValidationResult(input, validationContext);
        ValidationResult? result2 = attribute2.GetValidationResult(IPAddress.Parse(input), validationContext);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result1, Is.EqualTo(ValidationResult.Success));
            Assert.That(result2, Is.EqualTo(ValidationResult.Success));
        }
    }

    [TestCase("2001:0db8:85a3:0000:0000:8a2e:0370:7334")]
    [TestCase("2001:db8::1")]
    [TestCase("::1")]
    public void Should_ReturnSuccess_When_ValueIsCorrectAndModeIpV6(string input)
    {
        IpAttribute attribute1 = new(IpAttribute.Modes.IPv6);
        IpAttribute attribute2 = new(IpAttribute.Modes.IPv6);

        ValidationContext validationContext = new(new());

        ValidationResult? result1 = attribute1.GetValidationResult(input, validationContext);
        ValidationResult? result2 = attribute2.GetValidationResult(IPAddress.Parse(input), validationContext);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result1, Is.EqualTo(ValidationResult.Success));
            Assert.That(result2, Is.EqualTo(ValidationResult.Success));
        }
    }

    [TestCase("256.100.1.1")]
    [TestCase("192.168.1.1.1")]
    [TestCase("abc.def.ghi.jkl")]
    [TestCase("2001:db8:::1")]
    public void Should_ReturnValidationError_When_ValueIsIncorrectAndModeAny(string input)
    {
        IpAttribute attribute = new() { ErrorMessage = "The {0} must be correct IP Address." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must be correct IP Address."));
    }

    [TestCase("256.100.1.1")]
    [TestCase("192.168.1.1.1")]
    [TestCase("abc.def.ghi.jkl")]
    [TestCase("2001:db8:::1")]
    [TestCase("192.168.1.1")]
    [TestCase("8.8.8.8")]
    [TestCase("0.0.0.0")]
    [TestCase("192.168.1.123")]
    public void Should_ReturnValidationError_When_ValueIsIncorrectStringAndModeIpV6(string input)
    {
        IpAttribute attribute = new(IpAttribute.Modes.IPv6) { ErrorMessage = "The {0} must be correct IPv6 Address." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must be correct IPv6 Address."));
    }

    [TestCase("256.100.1.1")]
    [TestCase("192.168.1.1.1")]
    [TestCase("abc.def.ghi.jkl")]
    [TestCase("2001:db8:::1")]
    [TestCase("2001:0db8:85a3:0000:0000:8a2e:0370:7334")]
    [TestCase("2001:db8::1")]
    [TestCase("::1")]
    public void Should_ReturnValidationError_When_ValueIsIncorrectStringAndModeIpV4(string input)
    {
        IpAttribute attribute = new(IpAttribute.Modes.IPv4) { ErrorMessage = "The {0} must be correct IPv4 Address." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must be correct IPv4 Address."));
    }

    [TestCase("192.168.1.1")]
    [TestCase("8.8.8.8")]
    [TestCase("0.0.0.0")]
    [TestCase("192.168.1.123")]
    public void Should_ReturnValidationError_When_ValueIsIncorrectIpAddressAndModeIpV6(string input)
    {
        IpAttribute attribute = new(IpAttribute.Modes.IPv6) { ErrorMessage = "The {0} must be correct IPv6 Address." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(IPAddress.Parse(input), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must be correct IPv6 Address."));
    }

    [TestCase("2001:0db8:85a3:0000:0000:8a2e:0370:7334")]
    [TestCase("2001:db8::1")]
    [TestCase("::1")]
    public void Should_ReturnValidationError_When_ValueIsIncorrectIpAddressAndModeIpV4(string input)
    {
        IpAttribute attribute = new(IpAttribute.Modes.IPv4) { ErrorMessage = "The {0} must be correct IPv4 Address." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(IPAddress.Parse(input), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must be correct IPv4 Address."));
    }
}
