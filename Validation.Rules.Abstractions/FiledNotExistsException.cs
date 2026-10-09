namespace Zhomfr.Validation.Rules.Abstractions;

public class FiledNotExistsException(string message = "The filed not present in validated object.") : Exception(message);
