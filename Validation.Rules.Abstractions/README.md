# Validation Rule Abstractions

[![NuGet Status](https://img.shields.io/nuget/v/Zhomfr.Validation.Rules.Abstractions.svg)](https://www.nuget.org/packages/Zhomfr.Validation.Rules.Abstractions/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Zhomfr.Validation.Rules.Abstractions.svg)](https://www.nuget.org/packages/Zhomfr.Validation.Rules.Abstractions/)

A lightweight, extensible foundational library for building custom validation rules in .NET. It provides a robust base
class with essential infrastructure, property access helpers, and a dedicated set of validation exceptions to build
clean and predictable validation pipelines.

## Key Features

* **Base Rule Infrastructure:** Standardized core logic for defining custom, reusable validation rules.
* **Property Value Extraction:** Effortlessly extract object property values by name using built-in helper methods.
* **Display Name Resolution:** Automatically retrieve human-readable property names from attributes (such as
  `[Display(Name = "...")]`).
* **Null-Safe Evaluation:** Designed to return positive results on `null` inputs, ensuring `Required` rules can properly
  manage presence validation without unexpected side effects.
* **Error Message Formatting:** Built-in template formatting for consistent and customizable error output.
* **Validation Exception Hierarchy:** A comprehensive set of domain-specific validation exceptions for robust error
  handling.

## 📦 Installation

Install package:

```shell
dotnet add package Zhomfr.Validation.Rules.Abstractions
```

## Zhomfr.Validation.Rules.Abstractions

[AbstractRule](#abstractrule)

## Zhomfr.Validation.Rules.Abstractions.Errors

[UnsupportedTypeException](#unsupportedtypeexception)

[ValuesHasDifferentTypesException](#valueshasdifferenttypesexception)

# AbstractRule

Namespace: Zhomfr.Validation.Rules.Abstractions

```csharp
public abstract class AbstractRule : System.ComponentModel.DataAnnotations.ValidationAttribute
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [Attribute](https://learn.microsoft.com/en-us/dotnet/api/system.attribute) → [ValidationAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.validationattribute) → [AbstractRule](./zhomfr.validation.rules.abstractions.abstractrule.md)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [AttributeUsageAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.attributeusageattribute)

## Properties

### **MessageArgs**

Arguments for error message.

```csharp
protected List<object> MessageArgs { get; }
```

#### Property Value

[List&lt;Object&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

### **ErrorMessageString**

```csharp
protected string ErrorMessageString { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **RequiresValidationContext**

```csharp
public virtual bool RequiresValidationContext { get; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ErrorMessage**

```csharp
public string? ErrorMessage { get; set; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ErrorMessageResourceName**

```csharp
public string? ErrorMessageResourceName { get; set; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ErrorMessageResourceType**

```csharp
public Type? ErrorMessageResourceType { get; set; }
```

#### Property Value

[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type)<br>

### **TypeId**

```csharp
public virtual object TypeId { get; }
```

#### Property Value

[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>

## Constructors

### **AbstractRule ()**

```csharp
protected AbstractRule()
```

## Methods

### **CheckCondition (Object, ValidationContext)**

Checks the validation condition for the specified value.

```csharp
protected abstract bool CheckCondition(object value, ValidationContext context)
```

#### Parameters

`value` [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>
The value to validate.

`context` [ValidationContext](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.validationcontext)<br>
The validation context.

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the condition is met; otherwise, false.

#### Exceptions

[UnsupportedTypeException](./zhomfr.validation.rules.abstractions.errors.unsupportedtypeexception.md)<br>
Thrown when the rule cannot process the data type.

[Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception)<br>
Any other exceptions thrown by the logic.

### **FormatedMessage (String, Object)**

Formats the error message using the display name, message arguments, and value.

```csharp
protected virtual string FormatedMessage(string displayName, object value)
```

#### Parameters

`displayName` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The display name of the field being validated.

`value` [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>
The value that failed validation.

#### Returns

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The formatted error message string.

### **IsValid (Object, ValidationContext)**

```csharp
protected override ValidationResult? IsValid(object? value, ValidationContext context)
```

#### Parameters

`value` [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)?<br>

`context` [ValidationContext](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.validationcontext)<br>

#### Returns

[ValidationResult](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.validationresult)?<br>

#### Exceptions

[UnsupportedTypeException](./zhomfr.validation.rules.abstractions.errors.unsupportedtypeexception.md)<br>
Thrown when the rule cannot process the data type.

[Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception)<br>
Any other exceptions thrown by the logic.

### **ThrowIfFieldsHasDifferentTypes (Object, Object)**

Check if validated fields has same types.

```csharp
protected static void ThrowIfFieldsHasDifferentTypes(object value, object anotherValue)
```

#### Parameters

`value` [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>
Current value.

`anotherValue` [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>
Value of another field.

#### Exceptions

[ValuesHasDifferentTypesException](./zhomfr.validation.rules.abstractions.errors.valueshasdifferenttypesexception.md)<br>
If fields has different types/

### **GetFieldValue&lt;T&gt; (String, ValidationContext)**

Get field value by name.

```csharp
protected static T GetFieldValue<T>(string fieldName, ValidationContext context)
```

#### Type Parameters

`T`<br>
Expected type of value.

#### Parameters

`fieldName` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
Field name.

`context` [ValidationContext](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.validationcontext)<br>
Validation context.

#### Returns

T<br>
Value of field.

#### Exceptions

[MissingFieldException](https://learn.microsoft.com/en-us/dotnet/api/system.missingfieldexception)<br>
Then field or property not exists in type.

### **GetDisplayNameForProperty (String, ValidationContext)**

Return display name by field name.

```csharp
protected static string GetDisplayNameForProperty(string fieldName, ValidationContext context)
```

#### Parameters

`fieldName` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of field.

`context` [ValidationContext](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.validationcontext)<br>
Validation context.

#### Returns

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
Return display name of field.

#### Exceptions

[MissingFieldException](https://learn.microsoft.com/en-us/dotnet/api/system.missingfieldexception)<br>
Then field or property not exists in type.

### **AddListAsOneArgumentToMessageArgs (Object[])**

Add attribute argument to error message argument like one argument.

```csharp
protected void AddListAsOneArgumentToMessageArgs(Object[] args)
```

#### Parameters

`args` [Object[]](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>
Arguments what need add.

# UnsupportedTypeException

Namespace: Zhomfr.Validation.Rules.Abstractions.Errors

Exception what indicate what rule can't process type of value.

```csharp
public class UnsupportedTypeException : System.Exception, System.Runtime.Serialization.ISerializable
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception) → [UnsupportedTypeException](./zhomfr.validation.rules.abstractions.errors.unsupportedtypeexception.md)<br>
Implements [ISerializable](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iserializable)

## Properties

### **TargetSite**

```csharp
public MethodBase? TargetSite { get; }
```

#### Property Value

[MethodBase](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.methodbase)<br>

### **Message**

```csharp
public virtual string Message { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Data**

```csharp
public virtual IDictionary Data { get; }
```

#### Property Value

[IDictionary](https://learn.microsoft.com/en-us/dotnet/api/system.collections.idictionary)<br>

### **InnerException**

```csharp
public Exception? InnerException { get; }
```

#### Property Value

[Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception)<br>

### **HelpLink**

```csharp
public virtual string? HelpLink { get; set; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Source**

```csharp
public virtual string? Source { get; set; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **HResult**

```csharp
public int HResult { get; set; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **StackTrace**

```csharp
public virtual string? StackTrace { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **UnsupportedTypeException (Type)**

Exception what indicate what rule can't process type of value.

```csharp
public UnsupportedTypeException(Type type)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/en-us/dotnet/api/system.type)<br>
Type of value what can't be processed.

## Events

### **SerializeObjectState**

#### Caution

BinaryFormatter serialization is obsolete and should not be used. See https://aka.ms/binaryformatter for more
information.

---

```csharp
protected event EventHandler<SafeSerializationEventArgs>? SerializeObjectState;
```

# ValuesHasDifferentTypesException

Namespace: Zhomfr.Validation.Rules.Abstractions.Errors

Exception what indicates what 2 fields what validated has different type when rule expect same types.

```csharp
public class ValuesHasDifferentTypesException : System.Exception, System.Runtime.Serialization.ISerializable
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception) → [ValuesHasDifferentTypesException](./zhomfr.validation.rules.abstractions.errors.valueshasdifferenttypesexception.md)<br>
Implements [ISerializable](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iserializable)

## Properties

### **TargetSite**

```csharp
public MethodBase? TargetSite { get; }
```

#### Property Value

[MethodBase](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.methodbase)<br>

### **Message**

```csharp
public virtual string Message { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Data**

```csharp
public virtual IDictionary Data { get; }
```

#### Property Value

[IDictionary](https://learn.microsoft.com/en-us/dotnet/api/system.collections.idictionary)<br>

### **InnerException**

```csharp
public Exception? InnerException { get; }
```

#### Property Value

[Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception)<br>

### **HelpLink**

```csharp
public virtual string? HelpLink { get; set; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Source**

```csharp
public virtual string? Source { get; set; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **HResult**

```csharp
public int HResult { get; set; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **StackTrace**

```csharp
public virtual string? StackTrace { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **ValuesHasDifferentTypesException (String)**

Exception what indicates what 2 fields what validated has different type when rule expect same types.

```csharp
public ValuesHasDifferentTypesException(string message = "The validated values has different types.")
```

#### Parameters

`message` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
Custom error message.

## Events

### **SerializeObjectState**

#### Caution

BinaryFormatter serialization is obsolete and should not be used. See https://aka.ms/binaryformatter for more
information.

---

```csharp
protected event EventHandler<SafeSerializationEventArgs>? SerializeObjectState;
```
