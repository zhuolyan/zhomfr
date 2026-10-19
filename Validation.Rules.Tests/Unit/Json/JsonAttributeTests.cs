using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Tests.Unit.Json;

[TestFixture]
public class JsonAttributeTests
{
    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        JsonAttribute     attribute         = new();
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        JsonAttribute     attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("{\"name\": \"John\", \"age\": 30, \"isVerified\": true}")]
    [TestCase("[1, 2, 3, \"four\", {\"nested\": null}]")]
    [TestCase(
        "{\n  \"user\": \"Alice\",\n  \"roles\": [\"admin\", \"moderator\"],\n  \"settings\": {\n    \"theme\": \"dark\",\n    \"notifications\": false\n  }\n}")]
    [TestCase("\"Just a string\"")]
    [TestCase("123.45")]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string input)
    {
        JsonAttribute     attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("{'name': 'John', 'age': 30}")]
    [TestCase("{name: \"John\", age: 30}")]
    [TestCase("{\"name\": \"John\", \"age\": 30,}")]
    [TestCase("{\"name\": \"John\", \"age\": 30")]
    [TestCase("{\"value\": undefined}")]
    [TestCase("{\n  \"name\": \"John\" // це коментар\n}")]
    public void Should_ReturnValidationError_When_ValueIsIncorrect(string input)
    {
        JsonAttribute     attribute         = new() { ErrorMessage     = "The {0} must be a valid json string." };
        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };
        ;

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must be a valid json string."));
    }
}
