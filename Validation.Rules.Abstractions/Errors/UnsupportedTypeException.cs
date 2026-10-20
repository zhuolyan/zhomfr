namespace Zhomfr.Validation.Rules.Abstractions.Errors;

public class UnsupportedTypeException(Type type) : Exception($"The type {type.FullName} is not supported by this validation rule.");
