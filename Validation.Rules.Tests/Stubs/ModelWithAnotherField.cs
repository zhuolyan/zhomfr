using System.ComponentModel.DataAnnotations;

namespace Zhomfr.Validation.Rules.Tests.Stubs;

public class ModelWithAnotherField<T>
{
    public required T AnotherField2;

    [Display(Name = "SomeField")]
    public required T AnotherField { get; set; }
}
