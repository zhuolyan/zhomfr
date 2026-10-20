using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Zhomfr.Validation.Rules.Tests.Unit.Size;

[TestFixture(typeof(short))]
[TestFixture(typeof(ushort))]
[TestFixture(typeof(int))]
[TestFixture(typeof(uint))]
[TestFixture(typeof(long))]
[TestFixture(typeof(ulong))]
public class IntSizeTests<T> where T : IParsable<T>
{
    public void Should_ReturnSuccessResult_When_IsCorrect()
    {
        SizeAttribute     attribute         = new("3");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(T.Parse("3", CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("2")]
    [TestCase("4")]
    public void Should_ReturnValidationError_When_IsInvalid(string input)
    {
        SizeAttribute     attribute         = new("3") { ErrorMessage  = "The field {0} must be equal {1}." };
        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(T.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be equal 3."));
    }
}
