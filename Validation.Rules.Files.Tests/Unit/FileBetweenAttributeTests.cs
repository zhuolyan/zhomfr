using System.ComponentModel.DataAnnotations;

using Zhomfr.Helpers.SmartFile;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Files.Tests.Unit;

[TestFixture]
public class FileBetweenAttributeTests
{
    private const string FILES_PATH = "Files";
    private const string FILENAME   = "test.";

    public static readonly string[] Suffixes = ["avif", "bmp", "gif", "ico", "jpg", "pdf", "png", "svg", "wbmp", "webp"];

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        FileBetweenAttribute attribute         = new(2_054, 432_254);
        ValidationContext    validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        FileBetweenAttribute attribute         = new(2_054, 432_254);
        ValidationContext    validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [TestCaseSource(typeof(FileBetweenAttributeTests), nameof(FileBetweenAttributeTests.Suffixes))]
    public void Should_ReturnSuccess_When_FileSizeInRange(string suffix)
    {
        FileBetweenAttribute attribute = new(2_054, 432_254);

        using SmartFile file = new(File.OpenRead(Path.Combine(FileBetweenAttributeTests.FILES_PATH, $"{FileBetweenAttributeTests.FILENAME}{suffix}")));

        ValidationContext context = new(new());

        ValidationResult? result = attribute.GetValidationResult(file, context);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCaseSource(typeof(FileBetweenAttributeTests), nameof(FileBetweenAttributeTests.Suffixes))]
    public void Should_ReturnValidationError_When_FileSizeOutOfRange(string suffix)
    {
        FileBetweenAttribute attribute = new(20_480, 30_720) { ErrorMessage = "The {0} must be between {1} and {2}" };

        using SmartFile file = new(File.OpenRead(Path.Combine(FileBetweenAttributeTests.FILES_PATH, $"{FileBetweenAttributeTests.FILENAME}{suffix}")));

        ValidationContext context = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(file, context);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result.ErrorMessage, Is.Not.EqualTo("The MyProp must be between 20_480 and 30_720"));
    }
}
