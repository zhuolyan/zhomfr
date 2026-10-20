using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.Max;

[TestFixture]
public class EnumerableMaxTests
{
    [TestCaseSource(typeof(EnumerableStubs), nameof(EnumerableStubs.Between))]
    public void Should_ReturnSuccessResult_When_IsCorrect(object input)
    {
        MaxAttribute      attribute         = new("6");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCaseSource(typeof(EnumerableStubs), nameof(EnumerableStubs.Between))]
    public void Should_ReturnValidationError_When_IsInvalid(object input)
    {
        MaxAttribute      attribute         = new("2") { ErrorMessage  = "The field {0} length must be less or equal {1}." };
        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp length must be less or equal 2."));
    }
}
