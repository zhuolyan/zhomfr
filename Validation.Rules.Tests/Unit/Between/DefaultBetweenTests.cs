using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;
using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.Between;

[TestFixture]
public class DefaultBetweenTests
{
    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        ModelWithAnotherField<int> model             = new() { Second = 32, First = 32 };
        BetweenAttribute           attribute         = new("3", "5");
        ValidationContext          validationContext = new(model);

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(model, validationContext));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        BetweenAttribute  attribute         = new("3", "5");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }
}
