namespace Zhomfr.Validation.Rules.Abstractions.Errors;

public class FiledNotExistsException(string fieldName) : Exception($"The filed {fieldName} not present in validated object.");
