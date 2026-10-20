namespace Zhomfr.Validation.Rules.Abstractions.Errors;

public class ValuesHasDifferentTypesException(string message = "The validated values has different types.") : Exception(message);
