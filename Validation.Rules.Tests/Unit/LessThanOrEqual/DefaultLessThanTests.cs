using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;
using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.LessThanOrEqual;

[TestFixture]
public class DefaultLessThanOrEqualTests
{
    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        ModelWithAnotherField<object> model             = new() { Second = new(), First = new() };
        LessThanAttribute             attribute         = new(nameof(ModelWithAnotherField<>.First));
        ValidationContext             validationContext = new(model);

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(new(), validationContext));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        ModelWithAnotherField<int> model             = new() { Second = 32, First = 32 };
        LessThanAttribute          attribute         = new(nameof(ModelWithAnotherField<>.First));
        ValidationContext          validationContext = new(model);

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_Throw_When_ValueIsNull()
    {
        ModelWithAnotherField<int> model             = new() { Second = 32, First = 32 };
        LessThanAttribute          attribute         = new(nameof(ModelWithAnotherField<>.First));
        ValidationContext          validationContext = new(model);

        Assert.Throws<ValuesHasDifferentTypesException>(() => attribute.GetValidationResult("test", validationContext));
    }
}
