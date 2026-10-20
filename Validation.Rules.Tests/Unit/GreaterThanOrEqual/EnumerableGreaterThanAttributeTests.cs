using System.Collections;
using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.GreaterThanOrEqual;

[TestFixture]
public class EnumerableGreaterThanOrEqualAttributeTests
{
    [TestCase(3)]
    [TestCase(4)]
    public void Should_ReturnSuccessResult_When_IsCorrect(int input)
    {
        GreaterThanOrEqualAttribute attribute = new(nameof(ModelWithAnotherField<>.First));

        IEnumerable value = EnumerableStubs.GetEnumerable(3);

        ValidationContext validationContext = new(new ModelWithAnotherField<IEnumerable> { Second = value, First = value });

        ValidationResult? result = attribute.GetValidationResult(EnumerableStubs.GetEnumerable(input), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase(3)]
    [TestCase(4)]
    public void Should_ReturnSuccessResult_When_IsCorrectDictionaryField(int input)
    {
        GreaterThanOrEqualAttribute attribute = new(nameof(ModelWithAnotherField<>.First));

        IDictionary value = DictionaryStubs.GetDictionary(3);

        ValidationContext validationContext = new(new ModelWithAnotherField<IDictionary> { Second = value, First = value });

        ValidationResult? result = attribute.GetValidationResult(DictionaryStubs.GetDictionary(input), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_IsInvalid()
    {
        GreaterThanOrEqualAttribute attribute = new(nameof(ModelWithAnotherField<>.Second)) { ErrorMessage = "The field {0} must be greater than or equal {1}." };

        IEnumerable value = EnumerableStubs.GetEnumerable(3);

        ValidationContext validationContext = new(new ModelWithAnotherField<IEnumerable> { Second = value, First = value }) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(EnumerableStubs.GetEnumerable(2), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be greater than or equal SomeField."));
    }

    [Test]
    public void Should_ReturnValidationError_When_IsInvalidDictionaryField()
    {
        GreaterThanOrEqualAttribute attribute = new(nameof(ModelWithAnotherField<>.First)) { ErrorMessage = "The field {0} must be greater than or equal {1}." };

        IDictionary value = DictionaryStubs.GetDictionary(3);

        ValidationContext validationContext = new(new ModelWithAnotherField<IDictionary> { Second = value, First = value }) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(DictionaryStubs.GetDictionary(2), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be greater than or equal First."));
    }
}
