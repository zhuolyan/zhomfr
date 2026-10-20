using System.ComponentModel.DataAnnotations;

namespace Zhomfr.Validation.Rules.Tests.Unit.Accepted;

[TestFixture]
public class AcceptedAttributeTests
{
    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        AcceptedAttribute attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("yes")]
    [TestCase("on")]
    [TestCase("true")]
    [TestCase("1")]
    [TestCase(1)]
    [TestCase(true)]
    public void Should_ReturnSuccess_When_ValueIsAcceptable(object input)
    {
        AcceptedAttribute attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("some text")]
    [TestCase("no")]
    [TestCase("off")]
    [TestCase("false")]
    [TestCase("0")]
    [TestCase("2")]
    [TestCase(0)]
    [TestCase(2)]
    [TestCase(false)]
    public void Should_ReturnFailure_When_ValueIsNotAcceptable(object input)
    {
        AcceptedAttribute attribute = new() { ErrorMessage = "The {0} must be accepted." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must be accepted."));
        }
    }
}
