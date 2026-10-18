using System.ComponentModel.DataAnnotations;

namespace Zhomfr.Validation.Rules.Tests.Unit.AcceptedAttributeTests;
[TestFixture]
public class DefaultTests
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
    public void Should_ReturnSuccess_When_TestedValueIsAcceptable(object value)
    {
        AcceptedAttribute attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

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
    public void Should_ReturnFailure_When_TestedValueIsNotAcceptable(object value)
    {
        AcceptedAttribute attribute = new() { ErrorMessage = "The {0} must be accepted." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must be accepted."));
        }
    }
}
