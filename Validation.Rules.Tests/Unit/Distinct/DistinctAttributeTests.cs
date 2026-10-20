using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;
using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.Distinct;

[TestFixture]
public class DistinctAttributeTests
{
    public static readonly List<object> TestedDistinct    = [DictionaryStubs.Default, EnumerableStubs.GetEnumerable(3)];
    public static readonly List<object> TestedNotDistinct = [DictionaryStubs.NotDistinct, EnumerableStubs.GetNotDistinctEnumerable(3)];

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        DistinctAttribute attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        DistinctAttribute attribute         = new();
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [TestCaseSource(typeof(DistinctAttributeTests), nameof(DistinctAttributeTests.TestedDistinct))]
    public void Should_ReturnSuccess_When_ValueIsDistinct(object input)
    {
        DistinctAttribute attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCaseSource(typeof(DistinctAttributeTests), nameof(DistinctAttributeTests.TestedNotDistinct))]
    public void Should_ReturnValidationError_When_ValueIsNotDistinct(object input)
    {
        DistinctAttribute attribute         = new() { ErrorMessage     = "The {0} must be distinct." };
        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must be distinct."));
    }
}
