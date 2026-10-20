using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Zhomfr.Validation.Rules.Tests.Unit.Between;

[TestFixture(typeof(short))]
[TestFixture(typeof(ushort))]
[TestFixture(typeof(int))]
[TestFixture(typeof(uint))]
[TestFixture(typeof(long))]
[TestFixture(typeof(ulong))]
public class IntBetweenTests<T> where T : IParsable<T>
{
    [TestCase("3")]
    [TestCase("4")]
    [TestCase("5")]
    public void Should_ReturnSuccessResult_When_IsCorrect(string input)
    {
        BetweenAttribute  attribute         = new("3", "5");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(T.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("2")]
    [TestCase("6")]
    public void Should_ReturnValidationError_When_IsInvalid(string input)
    {
        BetweenAttribute  attribute         = new("3", "5") { ErrorMessage = "The field {0} must be between {1} and {2}." };
        ValidationContext validationContext = new(new()) { DisplayName     = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(T.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be between 3 and 5."));
    }
}
