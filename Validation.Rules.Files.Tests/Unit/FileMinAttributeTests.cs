using System.ComponentModel.DataAnnotations;

using Zhomfr.Helpers.SmartFile;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Files.Tests.Unit;

[TestFixture]
public class FileMinAttributeTests
{
    private const string FILES_PATH = "Files";
    private const string FILENAME   = "test.";

    public static readonly string[] Suffixes = ["avif", "bmp", "gif", "ico", "jpg", "pdf", "png", "svg", "wbmp", "webp"];

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        FileMinAttribute  attribute         = new(2_054);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        FileMinAttribute  attribute         = new(2_054);
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [TestCaseSource(typeof(FileMinAttributeTests), nameof(FileMinAttributeTests.Suffixes))]
    public void Should_ReturnSuccess_When_FileSizeIsCorrect(string suffix)
    {
        FileMinAttribute attribute = new(2_054);

        using SmartFile file = new(File.OpenRead(Path.Combine(FileMinAttributeTests.FILES_PATH, $"{FileMinAttributeTests.FILENAME}{suffix}")));

        ValidationContext context = new(new());

        ValidationResult? result = attribute.GetValidationResult(file, context);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCaseSource(typeof(FileMinAttributeTests), nameof(FileMinAttributeTests.Suffixes))]
    public void Should_ReturnValidationError_When_FileSizeInvalid(string suffix)
    {
        FileMinAttribute attribute = new(432_255) { ErrorMessage = "The {0} must be greater than or equal to {1} bytes." };

        using SmartFile file = new(File.OpenRead(Path.Combine(FileMinAttributeTests.FILES_PATH, $"{FileMinAttributeTests.FILENAME}{suffix}")));

        ValidationContext context = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(file, context);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result.ErrorMessage, Is.Not.EqualTo("The MyProp must be greater than or equal to 432_255 bytes."));
    }
}
