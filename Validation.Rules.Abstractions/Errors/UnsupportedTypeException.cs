namespace Zhomfr.Validation.Rules.Abstractions.Errors;

/// <summary>Exception what indicate what rule can't process type of value.</summary>
/// <param name="type">Type of value what can't be processed.</param>
public class UnsupportedTypeException(Type type) : Exception($"The type {type.FullName} is not supported by this validation rule.");
