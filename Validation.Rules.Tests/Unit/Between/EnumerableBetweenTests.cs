using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.Between;

[TestFixture]
public class EnumerableBetweenTests
{
    [TestCaseSource(typeof(EnumerableStubs), nameof(EnumerableStubs.Between))]
    public void Should_ReturnSuccessResult_When_IsCorrectEnumerable(object value)
    {
        BetweenAttribute  attribute         = new("3", "6");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCaseSource(typeof(EnumerableStubs), nameof(EnumerableStubs.Between))]
    public void Should_ReturnValidationError_When_IsIncorrectEnumerable(object value)
    {
        BetweenAttribute  attribute         = new("4", "5") { ErrorMessage = "The field {0} length must be between {1} and {2}." };
        ValidationContext validationContext = new(new()) { DisplayName     = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp length must be between 4 and 5."));
    }
}
