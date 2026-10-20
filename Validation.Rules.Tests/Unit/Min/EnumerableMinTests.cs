using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.Min;

[TestFixture]
public class EnumerableMinTests
{
    [TestCaseSource(typeof(EnumerableStubs), nameof(EnumerableStubs.Between))]
    public void Should_ReturnSuccessResult_When_IsCorrect(object input)
    {
        MinAttribute      attribute         = new("3");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCaseSource(typeof(EnumerableStubs), nameof(EnumerableStubs.Between))]
    public void Should_ReturnValidationError_When_IsInvalid(object input)
    {
        MinAttribute      attribute         = new("7") { ErrorMessage  = "The field {0} length must be greater or equal {1}." };
        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp length must be greater or equal 7."));
    }
}
