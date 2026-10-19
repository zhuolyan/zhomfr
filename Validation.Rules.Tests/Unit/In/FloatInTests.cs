using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Zhomfr.Validation.Rules.Tests.Unit.In;

[TestFixture(typeof(float))]
[TestFixture(typeof(double))]
public class FloatInTests<T> where T : IParsable<T>
{
    [TestCase("0.000001")]
    [TestCase("0.0000001")]
    [TestCase("-0.0000002")]
    [TestCase("1.2")]
    public void Should_ReturnSuccess_When_IsCorrect(string input)
    {
        InAttribute       attribute         = new("0.000001", "1.2", "-0.000001");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(T.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("0.000002")]
    [TestCase("1.3")]
    public void Should_ReturnValidationError_When_IsIncorrect(string input)
    {
        InAttribute       attribute         = new("0.000001", "1.2") { ErrorMessage = "The field {0} must be equal one of: {1}." };
        ValidationContext validationContext = new(new()) { DisplayName              = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(T.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be equal one of: \"0.000001\", \"1.2\"."));
    }
}
