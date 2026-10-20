namespace Zhomfr.Validation.Rules.Abstractions.Errors;

/// <summary>Exception what indicates what 2 fields what validated has different type when rule expect same types.</summary>
/// <param name="message">Custom error message.</param>
public class ValuesHasDifferentTypesException(string message = "The validated values has different types.") : Exception(message);
