using System.ComponentModel.DataAnnotations;

namespace Zhomfr.Validation.Rules.Tests.Stubs;

public enum TestEnum { TestEnumValue1 }

public class ModelWithAnotherField<T>
{
    public required T First;

    [Display(Name = "SomeField")]
    public required T Second { get; set; }

    public TestEnum DiffType { get; set; } = TestEnum.TestEnumValue1;
}
