using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.Size;

[TestFixture]
public class EnumerableSizeTests
{
    public static readonly List<object> Tested = [EnumerableStubs.GetEnumerable(3), DictionaryStubs.GetDictionary(3)];

    public static readonly List<object> WrongTested =
    [
        EnumerableStubs.GetEnumerable(4), DictionaryStubs.GetDictionary(4), EnumerableStubs.GetEnumerable(2), DictionaryStubs.GetDictionary(2),
    ];

    [TestCaseSource(typeof(EnumerableSizeTests), nameof(EnumerableSizeTests.Tested))]
    public void Should_ReturnSuccessResult_When_IsCorrect(object input)
    {
        SizeAttribute     attribute         = new("3");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCaseSource(typeof(EnumerableSizeTests), nameof(EnumerableSizeTests.WrongTested))]
    public void Should_ReturnValidationError_When_IsInvalid(object input)
    {
        SizeAttribute     attribute         = new("3") { ErrorMessage  = "The field {0} length must be equal {1}." };
        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp length must be equal 3."));
    }
}
