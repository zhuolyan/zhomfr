using System.ComponentModel.DataAnnotations;

using Zhomfr.Helpers.SmartFile;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Files.Tests.Unit;

[TestFixture]
public class FileMaxAttributeTests
{
    private const string FILES_PATH = "Files";
    private const string FILENAME   = "test.";

    public static readonly string[] Suffixes = ["avif", "bmp", "gif", "ico", "jpg", "pdf", "png", "svg", "wbmp", "webp"];

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        FileMaxAttribute  attribute         = new(2_054);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        FileMaxAttribute  attribute         = new(2_054);
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [TestCaseSource(typeof(FileMaxAttributeTests), nameof(FileMaxAttributeTests.Suffixes))]
    public void Should_ReturnSuccess_When_FileSizeIsCorrect(string suffix)
    {
        FileMaxAttribute attribute = new(432_254);

        using SmartFile file = new(File.OpenRead(Path.Combine(FileMaxAttributeTests.FILES_PATH, $"{FileMaxAttributeTests.FILENAME}{suffix}")));

        ValidationContext context = new(new());

        ValidationResult? result = attribute.GetValidationResult(file, context);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCaseSource(typeof(FileMaxAttributeTests), nameof(FileMaxAttributeTests.Suffixes))]
    public void Should_ReturnValidationError_When_FileSizeInvalid(string suffix)
    {
        FileMaxAttribute attribute = new(2_053) { ErrorMessage = "The {0} must be less than or equal to {1} bytes." };

        using SmartFile file = new(File.OpenRead(Path.Combine(FileMaxAttributeTests.FILES_PATH, $"{FileMaxAttributeTests.FILENAME}{suffix}")));

        ValidationContext context = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(file, context);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result.ErrorMessage, Is.Not.EqualTo("The MyProp must be less than or equal to 2_053 bytes."));
    }
}
