using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Zhomfr.Validation.Rules.Tests.Unit.Min;

[TestFixture(typeof(short))]
[TestFixture(typeof(ushort))]
[TestFixture(typeof(int))]
[TestFixture(typeof(uint))]
[TestFixture(typeof(long))]
[TestFixture(typeof(ulong))]
public class IntMinTests<T> where T : IParsable<T>
{
    [TestCase("3")]
    [TestCase("4")]
    [TestCase("5")]
    public void Should_ReturnSuccessResult_When_IsCorrect(string input)
    {
        MinAttribute      attribute         = new("3");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(T.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("2")]
    public void Should_ReturnValidationError_When_IsInvalid(string input)
    {
        MinAttribute      attribute         = new("3") { ErrorMessage  = "The field {0} must be greater or equal {1}." };
        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(T.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be greater or equal 3."));
    }
}
