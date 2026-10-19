using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;
using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.In;

[TestFixture]
public class DefaultInTests
{
    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        InAttribute       attribute         = new("3");
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(new ModelWithAnotherField<int> { AnotherField = 32, AnotherField2 = 32 },
                                                                                    validationContext));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        InAttribute       attribute         = new("3");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ThrowArgumentException_When_ExpectedIsEmpty()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            InAttribute attribute = new();
        });
    }
}
