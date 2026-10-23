using System.ComponentModel.DataAnnotations;

using Zhomfr.Helpers.SmartFile;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Files.Tests.Unit;

[TestFixture]
public class FileDimensionsAttributeTests
{
    private const string FILES_PATH = "Files";
    private const string FILENAME   = "test.";

    public static readonly string[] Suffixes = ["bmp", "gif", "ico", "jpg", "png", "wbmp", "webp"];

    public static readonly string[] UnsupportedSuffixes = ["avif", "svg", "pdf"];

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        FileDimensionsAttribute attribute         = new(2_054, 432_254);
        ValidationContext       validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        FileDimensionsAttribute attribute         = new(2_054, 432_254);
        ValidationContext       validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [TestCaseSource(typeof(FileDimensionsAttributeTests), nameof(FileDimensionsAttributeTests.Suffixes))]
    public void Should_ReturnSuccess_When_FileDimensionsIsSame(string suffix)
    {
        FileDimensionsAttribute attribute = new(128, 128);

        using SmartFile file = new(File.OpenRead(Path.Combine(FileDimensionsAttributeTests.FILES_PATH, $"{FileDimensionsAttributeTests.FILENAME}{suffix}")));

        ValidationContext context = new(new());

        ValidationResult? result = attribute.GetValidationResult(file, context);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCaseSource(typeof(FileDimensionsAttributeTests), nameof(FileDimensionsAttributeTests.Suffixes))]
    public void Should_ReturnSuccess_When_FileDimensionsInRange(string suffix)
    {
        FileDimensionsAttribute attribute = new(120, 128, 128, 130);

        using SmartFile file = new(File.OpenRead(Path.Combine(FileDimensionsAttributeTests.FILES_PATH, $"{FileDimensionsAttributeTests.FILENAME}{suffix}")));

        ValidationContext context = new(new());

        ValidationResult? result = attribute.GetValidationResult(file, context);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCaseSource(typeof(FileDimensionsAttributeTests), nameof(FileDimensionsAttributeTests.Suffixes))]
    public void Should_ReturnValidationError_When_FileDimensionsIsDifferent(string suffix)
    {
        FileDimensionsAttribute attribute = new(100, 100) { ErrorMessage = "The {0} must be image with dimensions height equal {2}, width equal {1}." };

        using SmartFile file = new(File.OpenRead(Path.Combine(FileDimensionsAttributeTests.FILES_PATH, $"{FileDimensionsAttributeTests.FILENAME}{suffix}")));

        ValidationContext context = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(file, context);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result.ErrorMessage, Is.EqualTo("The MyProp must be image with dimensions height equal 100, width equal 100."));
    }

    [TestCaseSource(typeof(FileDimensionsAttributeTests), nameof(FileDimensionsAttributeTests.Suffixes))]
    public void Should_ReturnValidationError_When_FileDimensionsIsOutOfRange(string suffix)
    {
        FileDimensionsAttribute attribute = new(100, 130, 120, 140)
        {
            ErrorMessage = "The {0} must be image with dimensions height between {2} and {4}, width between {1} and {3}.",
        };

        using SmartFile file = new(File.OpenRead(Path.Combine(FileDimensionsAttributeTests.FILES_PATH, $"{FileDimensionsAttributeTests.FILENAME}{suffix}")));

        ValidationContext context = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(file, context);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result.ErrorMessage, Is.EqualTo("The MyProp must be image with dimensions height between 130 and 140, width between 100 and 120."));
    }

    [TestCaseSource(typeof(FileDimensionsAttributeTests), nameof(FileDimensionsAttributeTests.UnsupportedSuffixes))]
    public void Should_ReturnValidationError_When_FileIsNotImage(string suffix)
    {
        FileDimensionsAttribute attribute = new(100, 100) { ErrorMessage = "The {0} must be image with dimensions height equal {2}, width equal {1}." };

        using SmartFile file = new(File.OpenRead(Path.Combine(FileDimensionsAttributeTests.FILES_PATH, $"{FileDimensionsAttributeTests.FILENAME}{suffix}")));

        ValidationContext context = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(file, context);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result.ErrorMessage, Is.EqualTo("The MyProp must be image with dimensions height equal 100, width equal 100."));
    }
}
