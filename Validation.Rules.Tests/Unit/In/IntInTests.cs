using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Zhomfr.Validation.Rules.Tests.Unit.In;

[TestFixture(typeof(short))]
[TestFixture(typeof(ushort))]
[TestFixture(typeof(int))]
[TestFixture(typeof(uint))]
[TestFixture(typeof(long))]
[TestFixture(typeof(ulong))]
public class IntInTests<T> where T : IParsable<T>
{
    [TestCase("3")]
    [TestCase("2")]
    [TestCase("1")]
    public void Should_ReturnSuccess_When_IsCorrect(string input)
    {
        InAttribute       attribute         = new("1", "2", "3");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(T.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("4")]
    [TestCase("5")]
    [TestCase("6")]
    public void Should_ReturnValidationError_When_IsInvalid(string input)
    {
        InAttribute       attribute         = new("1", "2", "3") { ErrorMessage = "The field {0} must be equal one of: {1}." };
        ValidationContext validationContext = new(new()) { DisplayName          = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(T.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be equal one of: \"1\", \"2\", \"3\"."));
    }
}
