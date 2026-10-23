using System.ComponentModel.DataAnnotations;

using Zhomfr.Helpers.SmartFile;
using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Files.Tests.Unit;

[TestFixture]
public class FileImageAttributeTests
{
    private const string FILES_PATH = "Files";

    public static readonly string[] Correct = ["test.ico", "test.png", "wrong-jpeg.jpeg", "test.bmp", "test.gif", "test.jpg", "test.webp"];

    public static readonly string[] Invalid = ["test.svg", "test.avif", "test.pdf"];

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        FileImageAttribute attribute         = new();
        ValidationContext  validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        FileImageAttribute attribute         = new();
        ValidationContext  validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [TestCaseSource(typeof(FileImageAttributeTests), nameof(FileImageAttributeTests.Correct))]
    public void Should_ReturnSuccess_When_ValueIsCorrectFile(string input)
    {
        FileImageAttribute attribute         = new();
        ValidationContext  validationContext = new(new());

        using SmartFile file = new(File.OpenRead(Path.Combine(FileImageAttributeTests.FILES_PATH, input)));

        ValidationResult? result = attribute.GetValidationResult(file, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCaseSource(typeof(FileImageAttributeTests), nameof(FileImageAttributeTests.Invalid))]
    public void Should_ReturnValidationError_When_ValueIsInvalidFile(string input)
    {
        FileImageAttribute attribute         = new() { ErrorMessage     = "The {0} must be a image file." };
        ValidationContext  validationContext = new(new()) { DisplayName = "MyProp" };

        using SmartFile file = new(File.OpenRead(Path.Combine(FileImageAttributeTests.FILES_PATH, input)));

        ValidationResult? result = attribute.GetValidationResult(file, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result.ErrorMessage, Is.EqualTo("The MyProp must be a image file."));
    }
}
