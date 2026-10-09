namespace Zhomfr.Validation.Rules.Tests.Stubs;

public class ConfirmedModel
{
    public string Password             { get; set; } = string.Empty;
    public string PasswordConfirmation { get; set; } = string.Empty;
    public int    SomethingElse        { get; set; }
}
