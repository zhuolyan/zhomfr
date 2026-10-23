using System.ComponentModel.DataAnnotations;

using Zhomfr.Helpers.SmartFile;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Files.Tests.Unit;

[TestFixture]
public class FileExtensionAttributeTests
{
    private const string FILES_PATH = "Files";

    public static readonly string[] Correct = ["test.ico", "test.png", "wrong-jpeg.jpeg"];

    public static readonly string[] Invalid = ["test.avif", "test.bmp", "test.gif", "test.jpg", "test.pdf", "test.svg", "test.webp"];

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        FileExtensionAttribute attribute         = new("png");
        ValidationContext      validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        FileExtensionAttribute attribute         = new("png");
        ValidationContext      validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [TestCaseSource(typeof(FileExtensionAttributeTests), nameof(FileExtensionAttributeTests.Correct))]
    public void Should_ReturnSuccess_When_ValueIsCorrectFile(string input)
    {
        FileExtensionAttribute attribute         = new("png", "bin");
        ValidationContext      validationContext = new(new());

        using SmartFile file = new(File.OpenRead(Path.Combine(FileExtensionAttributeTests.FILES_PATH, input)));

        ValidationResult? result = attribute.GetValidationResult(file, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCaseSource(typeof(FileExtensionAttributeTests), nameof(FileExtensionAttributeTests.Invalid))]
    public void Should_ReturnValidationError_When_ValueIsInvalidFile(string input)
    {
        FileExtensionAttribute attribute         = new("png", "bin") { ErrorMessage = "The {0} must be file with one of extension: {1}" };
        ValidationContext      validationContext = new(new()) { DisplayName         = "MyProp" };

        using SmartFile file = new(File.OpenRead(Path.Combine(FileExtensionAttributeTests.FILES_PATH, input)));

        ValidationResult? result = attribute.GetValidationResult(file, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result.ErrorMessage, Is.EqualTo("The MyProp must be file with one of extension: \"png\", \"bin\""));
    }
}
