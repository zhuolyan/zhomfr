using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;
using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.GreaterThanOrEqual;

[TestFixture]
public class DefaultGreaterThanOrEqualTests
{
    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        GreaterThanOrEqualAttribute attribute         = new("3");
        ValidationContext           validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(new ModelWithAnotherField<int> { AnotherField = 32, AnotherField2 = 32 },
                                                                                    validationContext));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        GreaterThanOrEqualAttribute attribute         = new("3");
        ValidationContext           validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ThrowValuesHasDifferentTypesException_When_ValueIsNull()
    {
        int                         fieldVal  = 1;
        GreaterThanOrEqualAttribute attribute = new(nameof(ModelWithAnotherField<>.AnotherField2));

        ValidationContext validationContext = new(new ModelWithAnotherField<int> { AnotherField = fieldVal, AnotherField2 = fieldVal });

        Assert.Throws<ValuesHasDifferentTypesException>(() => attribute.GetValidationResult("test", validationContext));
    }
}
