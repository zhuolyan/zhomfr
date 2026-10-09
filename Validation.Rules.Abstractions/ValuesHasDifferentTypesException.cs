namespace Zhomfr.Validation.Rules.Abstractions;

public class ValuesHasDifferentTypesException(string message = "The validated values has different types.") : Exception(message);
