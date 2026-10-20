using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;
using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.NotIn;

[TestFixture]
public class DefaultNotInTests
{
    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        ModelWithAnotherField<int> model             = new() { Second = 32, First = 32 };
        NotInAttribute             attribute         = new("3");
        ValidationContext          validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(model, validationContext));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        NotInAttribute    attribute         = new("3");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void ShouldThrow_When_ExpectedIsEmpty()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            NotInAttribute attribute = new();
        });
    }
}
