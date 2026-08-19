namespace Zhomfr.Validation.Rules.Abstractions;

public class UnsupportedTypeException(string message = "The type is not supported by this validation rule.") : Exception(message);
