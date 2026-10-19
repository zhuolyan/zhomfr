using System.ComponentModel.DataAnnotations;

namespace Zhomfr.Validation.Rules.Tests.Unit.In;

public class StringInTests
{
    [TestCase("test1")]
    [TestCase("test2")]
    [TestCase("test3")]
    public void Should_ReturnSuccess_When_IsCorrect(string input)
    {
        InAttribute       attribute         = new("test1", "test2", "test3");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("Test1")]
    [TestCase("test5")]
    public void Should_ReturnValidationError_When_IsIncorrect(string input)
    {
        InAttribute       attribute         = new("test1", "test2", "test3") { ErrorMessage = "The field {0} must be equal one of: {1}." };
        ValidationContext validationContext = new(new()) { DisplayName                      = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be equal one of: \"test1\", \"test2\", \"test3\"."));
    }
}
