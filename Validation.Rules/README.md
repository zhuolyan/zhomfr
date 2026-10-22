# Validation Rules

[![NuGet Status](https://img.shields.io/nuget/v/Zhomfr.Validation.Rules.svg)](https://www.nuget.org/packages/Zhomfr.Validation.Rules/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Zhomfr.Validation.Rules.svg)](https://www.nuget.org/packages/Zhomfr.Validation.Rules/)

A powerful, intuitive collection of custom validation attributes built directly on top of the standard .NET
`System.ComponentModel.DataAnnotations` infrastructure. Designed to eliminate boilerplate code, this library brings a
rich set of production-ready validation rules—inspired by robust ecosystem standards (like Laravel's validation
features)
—allowing you to validate models cleanly using declarative attributes without reinventing the wheel.

## Key Features

* **Standard DataAnnotations Integration:** Seamlessly works with ASP.NET Core, Entity Framework, and standard
  `Validator` methods out of the box.
* **Rich Set of Built-in Rules:** Covers frequent validation scenarios that are missing from standard .NET, preventing
  you from writing repetitive custom validators over and over again.
* **Intuitive Declarative Syntax:** Clean and expressive attributes to keep your domain models and DTOs readable and
  maintainable.
* **Customizable Error Messages:** Fully supports localized and custom error message templates via standard
  `ErrorMessage` properties.

## 📦 Installation

Install package:

```shell
dotnet add package Zhomfr.Validation.Rules
```

After add namespace in using section:

```csharp
using Zhomfr.Validation.Rules;
```

---

## Available Rules

### Booleans

<table>
<tr valign="top">
<td>

 - [Accepted](#accepted)
 - [AcceptedIf](#acceptedif)
 - [Declined](#declined)
 - [DeclinedIf](#declinedif)

</td>
</tr>
</table>


### Integer

<table>
<tr valign="top">
<td>

 - [Accepted](#accepted)
 - [AcceptedIf](#acceptedif)
 - [Between](#between)
 - [Declined](#declined)
 - [DeclinedIf](#declinedif)

</td>
<td>

 - [GreaterThan](#greaterthan)
 - [GreaterThanOrEqual](#greaterthanorequal)
 - [In](#in)
 - [LessThan](#lessthan)
 - [LessThanOrEqual](#lessthanorequal)

</td>
<td>

 - [Max](#max)
 - [Min](#min)
 - [NotIn](#notin)
 - [Size](#size)

</td>
</tr>
</table>


### Float

<table>
<tr valign="top">
<td>

 - [Between](#between)
 - [GreaterThan](#greaterthan)
 - [GreaterThanOrEqual](#greaterthanorequal)
 - [In](#in)

</td>
<td>

 - [LessThan](#lessthan)
 - [LessThanOrEqual](#lessthanorequal)
 - [Max](#max)
 - [Min](#min)

</td>
<td>

 - [NotIn](#notin)
 - [Size](#size)

</td>
</tr>
</table>


### Decimal

<table>
<tr valign="top">
<td>

 - [Between](#between)
 - [Decimal](#decimal)
 - [Digits](#digits)
 - [GreaterThan](#greaterthan)

</td>
<td>

 - [GreaterThanOrEqual](#greaterthanorequal)
 - [In](#in)
 - [LessThan](#lessthan)
 - [LessThanOrEqual](#lessthanorequal)

</td>
<td>

 - [Max](#max)
 - [Min](#min)
 - [NotIn](#notin)
 - [Size](#size)

</td>
</tr>
</table>


### String

<table>
<tr valign="top">
<td>

 - [Accepted](#accepted)
 - [AcceptedIf](#acceptedif)
 - [ActiveUrl](#activeurl)
 - [Alpha](#alpha)
 - [AlphaDash](#alphadash)
 - [AlphaNumeric](#alphanumeric)
 - [Ascii](#ascii)
 - [Between](#between)
 - [Contains](#contains)
 - [Declined](#declined)
 - [DeclinedIf](#declinedif)
 - [DoesntContain](#doesntcontain)

</td>
<td>

 - [DoesntEndWith](#doesntendwith)
 - [DoesntStartWith](#doesntstartwith)
 - [Email](#email)
 - [EndsWith](#endswith)
 - [GreaterThan](#greaterthan)
 - [GreaterThanOrEqual](#greaterthanorequal)
 - [HexColor](#hexcolor)
 - [In](#in)
 - [Ip](#ip)
 - [Json](#json)
 - [LessThan](#lessthan)
 - [LessThanOrEqual](#lessthanorequal)

</td>
<td>

 - [Lowercase](#lowercase)
 - [MacAddress](#macaddress)
 - [Max](#max)
 - [Min](#min)
 - [NotIn](#notin)
 - [NotRegexp](#notregexp)
 - [Regexp](#regexp)
 - [Size](#size)
 - [StartsWith](#startswith)
 - [Uppercase](#uppercase)
 - [Url](#url)

</td>
</tr>
</table>


### DateTime

<table>
<tr valign="top">
<td>

 - [After](#after)
 - [AfterOrEqual](#afterorequal)
 - [Before](#before)
 - [BeforeOrEqual](#beforeorequal)
 - [Between](#between)

</td>
<td>

 - [GreaterThan](#greaterthan)
 - [GreaterThanOrEqual](#greaterthanorequal)
 - [In](#in)
 - [LessThan](#lessthan)
 - [LessThanOrEqual](#lessthanorequal)

</td>
<td>

 - [Max](#max)
 - [Min](#min)
 - [NotIn](#notin)
 - [Size](#size)

</td>
</tr>
</table>


### Collection

<table>
<tr valign="top">
<td>

 - [Between](#between)
 - [Contains](#contains)
 - [Distinct](#distinct)
 - [DoesntContain](#doesntcontain)
 - [GreaterThan](#greaterthan)

</td>
<td>

 - [GreaterThanOrEqual](#greaterthanorequal)
 - [InArray](#inarray)
 - [LessThan](#lessthan)
 - [LessThanOrEqual](#lessthanorequal)
 - [Max](#max)

</td>
<td>

 - [Min](#min)
 - [NotIn](#notin)
 - [Size](#size)

</td>
</tr>
</table>


### Dictionary

<table>
<tr valign="top">
<td>

 - [DictionaryKeys](#dictionarykeys)
 - [Distinct](#distinct)
 - [DoesntContain](#doesntcontain)

</td>
</tr>
</table>


## 🛠️ Features and Examples

### Accepted

The field under validation must be "yes", "on", 1, "1", true, or "true". This is useful for validating "Terms
of Service" acceptance or similar fields.


```csharp
public class AcceptedDto
{
    [Accepted]
    public bool BoolInvalid = false; // ValidationError

    [Accepted]
    public bool BoolValid = true; // Success

    [Accepted]
    public int IntInvalid = 0; // ValidationError

    [Accepted]
    public int IntValid = 1; // Success

    [Accepted]
    public string StringInvalid = "something"; // ValidationError

    [Accepted]
    public string StringValid = "yes"; // Success
}
```

### AcceptedIf

The field under validation must be "yes", "on", 1, "1", true, or "true" if another field under validation is
equal to a specified value. This is useful for validating "Terms of Service" acceptance or similar fields.


```csharp
public class AcceptedIfDto
{
    [AcceptedIf<string>("ExpectedField", "expected value")]
    public bool BoolInvalid = false; // ValidationError

    [AcceptedIf<string>("ExpectedField", "expected value")]
    public bool BoolValid = true; // Success

    [AcceptedIf<string>("ExpectedField", "expected value")]
    public int IntInvalid = 0; // ValidationError

    [AcceptedIf<string>("ExpectedField", "expected value")]
    public int IntValid = 1; // Success

    [AcceptedIf<string>("ExpectedField", "expected value")]
    public string StringInvalid = "something"; // ValidationError

    [AcceptedIf<string>("ExpectedField", "expected value")]
    public string StringValid = "yes"; // Success

    [AcceptedIf<string>("ExpectedField", "another value")]
    public bool BoolNotValidateInvalid = false; // Success

    [AcceptedIf<string>("ExpectedField", "another value")]
    public bool BoolNotValidateValid = true; // Success

    [AcceptedIf<string>("ExpectedField", "another value")]
    public int IntNotValidateInvalid = 0; // Success

    [AcceptedIf<string>("ExpectedField", "another value")]
    public int IntNotValidateValid = 1; // Success

    [AcceptedIf<string>("ExpectedField", "another value")]
    public string StringNotValidateInvalid = "something"; // Success

    [AcceptedIf<string>("ExpectedField", "another value")]
    public string StringNotValidateValid = "yes"; // Success

    public string ExpectedField = "expected value";
}
```

### ActiveUrl

The field under validation must have a valid A or AAAA record.


```csharp
public class ActiveUrlDto
{
    [ActiveUrl]
    public string StringInvalid = "non-existent-domain-123456789.com"; // ValidationError

    [ActiveUrl]
    public string StringValid = "https://microsoft.com"; // Success
}
```

### After

The field under validation must be a value after a given date.


```csharp
public class AfterDto
{
    [After("2026-10-18 07:00:00")]
    public DateTime DateTimeInvalid = DateTime.Parse("2026-10-18 07:00:00"); // ValidationError

    [After("2026-10-18 07:00:00")]
    public DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // ValidationError

    [After("2026-10-18 06:59:59")]
    public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // Success

    [After("2026-10-18 06:59:59")]
    public DateTime DateTimeValid = DateTime.Parse("2026-10-18 07:00:00"); // Success
}
```

### AfterOrEqual

The field under validation must be a value after a given date.


```csharp
public class AfterOrEqualDto
{
    [AfterOrEqual("2026-10-18 07:00:01")]
    public DateTime DateTimeInvalid = DateTime.Parse("2026-10-18 07:00:00"); // ValidationError

    [AfterOrEqual("2026-10-18 07:00:01")]
    public DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // ValidationError

    [AfterOrEqual("2026-10-18 07:00:00")]
    public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // Success

    [AfterOrEqual("2026-10-18 07:00:00")]
    public DateTime DateTimeValid = DateTime.Parse("2026-10-18 07:00:00"); // Success
}
```

### Alpha

The field under validation must be entirely Unicode alphabetic characters.


```csharp
public class AlphaDto
{
    [Alpha]
    public string StringInvalid = "abc "; // ValidationError

    [Alpha]
    public string StringValid = "abcABC"; // Success
}
```

### AlphaDash

The field under validation must be entirely Unicode alphanumeric characters, as well as ASCII dashes (-) and
ASCII underscores (_).


```csharp
public class AlphaDashDto
{
    [AlphaDash]
    public string StringInvalid = "abc-123_"; // ValidationError

    [AlphaDash]
    public string StringValid = "abc 123"; // Success
}
```

### AlphaNumeric

The field under validation must be entirely Unicode alphanumeric characters.


```csharp
public class AlphaNumericDto
{
    [AlphaNumeric]
    public string StringInvalid = "abc123"; // ValidationError

    [AlphaNumeric]
    public string StringValid = "abc-123"; // Success
}
```

### Ascii

The field under validation must be entirely 7-bit ASCII characters.


```csharp
public class AsciiDto
{
    [Ascii]
    public string StringInvalid = "abc123!@#"; // ValidationError

    [Ascii]
    public string StringValid = "привіт"; // Success
}
```

### Before

The field under validation must be a value preceding the given date.


```csharp
public class BeforeDto
{
    [Before("2026-10-18 07:00:00")]
    public DateTime DateTimeInvalid = DateTime.Parse("2026-10-18 07:00:00"); // ValidationError

    [Before("2026-10-18 07:00:00")]
    public DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // ValidationError

    [Before("2026-10-18 07:00:01")]
    public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // Success

    [Before("2026-10-18 07:00:01")]
    public DateTime DateTimeValid = DateTime.Parse("2026-10-18 07:00:00"); // Success
}
```

### BeforeOrEqual

The field under validation must be a value preceding the given date.


```csharp
public class BeforeOrEqualDto
{
    [BeforeOrEqual("2026-10-18 06:59:59")]
    public DateTime DateTimeInvalid = DateTime.Parse("2026-10-18 07:00:00"); // ValidationError

    [BeforeOrEqual("2026-10-18 06:59:59")]
    public DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // ValidationError

    [BeforeOrEqual("2026-10-18 07:00:00")]
    public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // Success

    [BeforeOrEqual("2026-10-18 07:00:00")]
    public DateTime DateTimeValid = DateTime.Parse("2026-10-18 07:00:00"); // Success
}
```

### Between

The field under validation must have a size between the given min and max (inclusive). Strings, numerics,
arrays, and files are evaluated in the same fashion as the size rule.


```csharp
public class BetweenDto
{
    [Between("2026-10-17 07:00:00", "2026-10-18 06:59:59")]
    public DateTime DateTimeInvalid = DateTime.Parse("2026-10-18 07:00:00"); // ValidationError

    [Between("2026-10-18 07:00:01", "2026-10-19 07:00:00")]
    public DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // ValidationError

    [Between("2026-10-18 07:00:00", "2026-10-19 07:00:00")]
    public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // Success

    [Between("2026-10-17 07:00:00", "2026-10-18 07:00:00")]
    public DateTime DateTimeValid = DateTime.Parse("2026-10-18 07:00:00"); // Success

    [Between("1", "3.299999")]
    public decimal DecimalInvalid = 3.3M; // ValidationError

    [Between("3.3", "5.9")]
    public decimal DecimalValid = 3.3M; // Success

    [Between("1", "3.299999")]
    public double DoubleInvalid = 3.3; // ValidationError

    [Between("3.3", "5.9")]
    public double DoubleValid = 3.3; // Success

    [Between("1", "3.299999")]
    public float FloatInvalid = 3.3f; // ValidationError

    [Between("3.3", "5.9")]
    public float FloatValid = 3.3f; // Success

    [Between("2", "5")]
    public ICollection CollectionInvalid = new List<int>() { 1 }; // ValidationError

    [Between("2", "5")]
    public ICollection CollectionValid = new List<int>() { 1, 2 }; // Success

    [Between("2", "5")]
    public IEnumerable EnumerableInvalid = new List<int>() { 1 }; // ValidationError

    [Between("2", "5")]
    public IEnumerable EnumerableValid = new List<int>() { 1, 2, 3, 4, 5 }; // Success

    [Between("-5", "5")]
    public int IntInvalid = -6; // ValidationError

    [Between("-5", "5")]
    public int IntValid = -5; // Success

    [Between("-5", "5")]
    public long LongInvalid = 6; // ValidationError

    [Between("-5", "5")]
    public long LongValid = 5; // Success

    [Between("-5", "5")]
    public short ShortInvalid = 10; // ValidationError

    [Between("-5", "5")]
    public short ShortValid = 1; // Success

    [Between("2", "5")]
    public string StringInvalid = "something"; // ValidationError

    [Between("2", "5")]
    public string StringValid = "test"; // Success

    [Between("2", "5")]
    public uint UintInvalid = 1; // ValidationError

    [Between("2", "5")]
    public uint UintValid = 2; // Success

    [Between("2", "5")]
    public ulong UlongInvalid = 1; // ValidationError

    [Between("2", "5")]
    public ulong UlongValid = 2; // Success

    [Between("2", "5")]
    public ushort UshortInvalid = 1; // ValidationError

    [Between("2", "5")]
    public ushort UshortValid = 2; // Success
}
```

### Contains

The field under validation must be a collection or string that contains all the given parameter values.


```csharp
public class ContainsDto
{
    [Contains(1, 2)]
    public ICollection CollectionInvalid = new List<int>() { 3, 4, 5 }; // ValidationError

    [Contains(1, 2)]
    public ICollection CollectionValid = new List<int>() { 1, 2, 4 }; // Success

    [Contains("2", "3")]
    public IEnumerable EnumerableInvalid = new List<string>() { "something", "else" }; // ValidationError

    [Contains("something", "else")]
    public IEnumerable EnumerableValid = new List<string>() { "something", "else" }; // Success

    [Contains("test", "not")]
    public string StringInvalid = "something"; // ValidationError

    [Contains("some", "thin")]
    public string StringValid = "something"; // Success
}
```

### Decimal

The field under validation must be decimal and must contain the specified number of decimal places.


```csharp
public class DecimalDto
{
    [Decimal(5)]
    public decimal DecimalInvalid = 3.3M; // ValidationError

    [Decimal(2, 5)]
    public decimal DecimalValid = 3.345M; // Success
}
```

### Declined

The field under validation must be "no", "off", 0, "0", false, or "false".


```csharp
public class DeclinedDto
{
    [Declined]
    public bool BoolInvalid = true; // ValidationError

    [Declined]
    public bool BoolValid = false; // Success

    [Declined]
    public int IntInvalid = 1; // ValidationError

    [Declined]
    public int IntValid = 0; // Success

    [Declined]
    public string StringInvalid = "something"; // ValidationError

    [Declined]
    public string StringValid = "off"; // Success
}
```

### DeclinedIf

The field under validation must be "no", "off", 0, "0", false, or "false" if another field under validation is
equal to a specified value.


```csharp
public class DeclinedIfDto
{
    public string ExpectedField = "expected value";

    [DeclinedIf<string>("ExpectedField", "expected value")]
    public bool BoolInvalid = true; // ValidationError

    [DeclinedIf<string>("ExpectedField", "expected value")]
    public bool BoolValid = false; // Success

    [DeclinedIf<string>("ExpectedField", "expected value")]
    public int IntInvalid = 1; // ValidationError

    [DeclinedIf<string>("ExpectedField", "expected value")]
    public int IntValid = 0; // Success

    [DeclinedIf<string>("ExpectedField", "expected value")]
    public string StringInvalid = "something"; // ValidationError

    [DeclinedIf<string>("ExpectedField", "expected value")]
    public string StringValid = "0"; // Success
}
```

### DictionaryKeys

The field under validation must be a Dictionary whose keys are all included in the given list. At least one
key must be provided.


```csharp
public class DictionaryKeysDto
{
    [DictionaryKeys("first", "second")]
    public IDictionary DictionaryInvalid = new Dictionary<string, string>() { { "first", "test" }, { "second", "not" }, { "third", "blah" } }; // ValidationError
            
    [DictionaryKeys("first", "test")]
    public IDictionary DictionaryValid = new Dictionary<string, string>() { { "first", "test" }, { "second", "not" }, { "third", "blah" } }; // Success
}
```

### Different

The field under validation must have a different value than field.


```csharp
public class DifferentDto
{
    [Different("ObjectInvalidSecond")]
    public object ObjectInvalid = 1; // ValidationError

    public object ObjectInvalidSecond = 2;

    [Different("ObjectValidSecond")]
    public object ObjectValid = "test"; // Success

    public object ObjectValidSecond = "test";
}
```

### Digits

The decimal under validation must have an exact length of value or between the given min and max.


```csharp
public class DigitsDto
{
    [Digits(5)]
    public decimal DecimalInvalid = 12.01M; // ValidationError

    [Digits(3, 5)]
    public decimal DecimalValid = 12.35M; // Success
}
```

### Distinct

Under validation must not have any duplicate values:


```csharp
public class DistinctDto
{
    [Distinct]
    public IDictionary DictionaryInvalid = new Dictionary<string, string>() { { "first", "test" }, { "second", "test" }, { "third", "blah" } }; // ValidationError

    [Distinct]
    public IDictionary DictionaryValid = new Dictionary<string, string>() { { "first", "test" }, { "second", "not" }, { "third", "blah" } }; // Success

    [Distinct]
    public IEnumerable EnumerableInvalid = new List<int>() { 1, 2, 2 }; // ValidationError

    [Distinct]
    public IEnumerable EnumerableValid = new List<int>() { 1, 2, 3 }; // Success
}
```

### DoesntContain

The field under validation must be a collection or string that does not contain any of the given parameter
values.


```csharp
public class DoesntContainDto
{
    [DoesntContain("input", "sharp")]
    public IDictionary DictionaryInvalid = new Dictionary<string, string>() { { "first", "input" }, { "second", "not" }, { "third", "blah" } }; // ValidationError

    [DoesntContain("input", "sharp")]
    public IDictionary DictionaryValid = new Dictionary<string, string>() { { "first", "test" }, { "second", "not" }, { "third", "blah" } }; // Success

    [DoesntContain(4, 5)]
    public IEnumerable EnumerableInvalid = new List<int>() { 1, 2, 3 }; // ValidationError

    [DoesntContain(1, 2)]
    public IEnumerable EnumerableValid = new List<int>() { 1, 2, 3 }; // Success

    [DoesntContain("some")]
    public string StringInvalid = "something"; // ValidationError

    [DoesntContain("end", "start")]
    public string StringValid = "something"; // Success
}
```

### DoesntEndWith

The field under validation must not end with one of the given values.


```csharp
public class DoesntEndWithDto
{
    [DoesntEndWith("start","ing")]
    public string StringInvalid = "something"; // ValidationError

    [DoesntEndWith("start","end")]
    public string StringValid = "something"; // Success
}
```

### DoesntStartWith

The field under validation must not start with one of the given values.


```csharp
public class DoesntStartWithDto
{
    [DoesntStartWith("start","some")]
    public string StringInvalid = "something"; // ValidationError

    [DoesntStartWith("start","ing")]
    public string StringValid = "something"; // Success
}
```

### Email

Validates an email address using one or more configurable strategies.


```csharp
public class EmailDto
{
    [Email(EmailAttribute.SPOOF)]
    public string StringInvalid = "tést@exämple.com"; // ValidationError

    [Email(EmailAttribute.DNS)]
    public string StringValid = "test@google.com"; // Success
}
```

### EndsWith

The field under validation must end with one of the given values.


```csharp
public class EndsWithDto
{
    [EndsWith("ing","end")]
    public string StringInvalid = "something"; // ValidationError

    [EndsWith("start","end")]
    public string StringValid = "something"; // Success
}
```

### GreaterThan

The field under validation must be greater than the given field. The two fields must be of the same type.
Strings, numerics, collections are evaluated using the same conventions as the size rule.


```csharp
public class GreaterThanDto
{
    [GreaterThan("DateTimeInvalidSecond")]
    public DateTime DateTimeInvalid = DateTime.Parse("2026-10-18 07:00:00"); // ValidationError

    public DateTime DateTimeInvalidSecond = DateTime.Parse("2026-10-18 07:00:00");

    [GreaterThan("DateTimeOffsetInvalidSecond")]
    public DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:0"); // ValidationError

    public DateTimeOffset DateTimeOffsetInvalidSecond = DateTimeOffset.Parse("2026-10-18 07:00:00");

    [GreaterThan("DateTimeOffsetValidSecond")]
    public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:01"); // Success

    public DateTimeOffset DateTimeOffsetValidSecond = DateTimeOffset.Parse("2026-10-18 07:00:00");

    [GreaterThan("DateTimeValidSecond")]
    public DateTime DateTimeValid = DateTime.Parse("2026-10-18 07:00:01"); // Success

    public DateTime DateTimeValidSecond = DateTime.Parse("2026-10-18 07:00:00");

    [GreaterThan("DecimalInvalidSecond")]
    public decimal DecimalInvalid = 3.3M; // ValidationError

    public decimal DecimalInvalidSecond = 3.3M;

    [GreaterThan("DecimalValidSecond")]
    public decimal DecimalValid = 3.3M; // Success

    public decimal DecimalValidSecond = 3.29999M;

    [GreaterThan("DoubleInvalidSecond")]
    public double DoubleInvalid = 3.3; // ValidationError

    public double DoubleInvalidSecond = 3.3;

    [GreaterThan("DoubleValidSecond")]
    public double DoubleValid = 3.3; // Success

    public double DoubleValidSecond = 3.2999;

    [GreaterThan("FloatInvalidSecond")]
    public float FloatInvalid = 3.3f; // ValidationError

    public float FloatInvalidSecond = 3.3f;

    [GreaterThan("FloatValidSecond")]
    public float FloatValid = 3.3f; // Success

    public float FloatValidSecond = 3.29999f;

    [GreaterThan("CollectionInvalidSecond")]
    public ICollection CollectionInvalid = new List<int>() {1,2,3}; // ValidationError

    public ICollection CollectionInvalidSecond = new List<int>(){1,2,3};

    [GreaterThan("CollectionValidSecond")]
    public ICollection CollectionValid = new List<int>(){1,2,3}; // Success

    public ICollection CollectionValidSecond = new List<int>(){1,2};

    [GreaterThan("EnumerableInvalidSecond")]
    public IEnumerable EnumerableInvalid = new List<int>(){1,2,3}; // ValidationError

    public IEnumerable EnumerableInvalidSecond = new List<int>(){1,2,3};

    [GreaterThan("EnumerableValidSecond")]
    public IEnumerable EnumerableValid = new List<int>(){1,2,3}; // Success

    public IEnumerable EnumerableValidSecond = new List<int>(){1,2};

    [GreaterThan("IntInvalidSecond")]
    public int IntInvalid = 1; // ValidationError

    public int IntInvalidSecond = 1;

    [GreaterThan("IntValidSecond")]
    public int IntValid = 1; // Success

    public int IntValidSecond = 0;

    [GreaterThan("LongInvalidSecond")]
    public long LongInvalid = 1; // ValidationError

    public long LongInvalidSecond = 1;

    [GreaterThan("LongValidSecond")]
    public long LongValid = 1; // Success

    public long LongValidSecond = 0;

    [GreaterThan("ShortInvalidSecond")]
    public short ShortInvalid = 1; // ValidationError

    public short ShortInvalidSecond = 1;

    [GreaterThan("ShortValidSecond")]
    public short ShortValid = 1; // Success

    public short ShortValidSecond = 0;

    [GreaterThan("StringInvalidSecond")]
    public string StringInvalid = "something"; // ValidationError

    public string StringInvalidSecond = "something";

    [GreaterThan("StringValidSecond")]
    public string StringValid = "something"; // Success

    public string StringValidSecond = "Somethin";

    [GreaterThan("UintInvalidSecond")]
    public uint UintInvalid = 1; // ValidationError

    public uint UintInvalidSecond = 1;

    [GreaterThan("UintValidSecond")]
    public uint UintValid = 1; // Success

    public uint UintValidSecond = 0;

    [GreaterThan("UlongInvalidSecond")]
    public ulong UlongInvalid = 1; // ValidationError

    public ulong UlongInvalidSecond = 1;

    [GreaterThan("UlongValidSecond")]
    public ulong UlongValid = 1; // Success

    public ulong UlongValidSecond = 0;

    [GreaterThan("UshortInvalidSecond")]
    public ushort UshortInvalid = 1; // ValidationError

    public ushort UshortInvalidSecond = 1;

    [GreaterThan("UshortValidSecond")]
    public ushort UshortValid = 1; // Success

    public ushort UshortValidSecond = 0;
}
```

### GreaterThanOrEqual

The field under validation must be greater than the given fielde. The two fields must be of the same type.
Strings, numerics, collections are evaluated using the same conventions as the size rule.


```csharp
public class GreaterThanOrEqualDto
{
    [GreaterThanOrEqual("DateTimeInvalidSecond")]
    public DateTime DateTimeInvalid = DateTime.Parse("2026-10-18 07:00:01"); // ValidationError

    public DateTime DateTimeInvalidSecond = DateTime.Parse("2026-10-18 07:00:00");

    [GreaterThanOrEqual("DateTimeOffsetInvalidSecond")]
    public DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:01"); // ValidationError

    public DateTimeOffset DateTimeOffsetInvalidSecond = DateTimeOffset.Parse("2026-10-18 07:00:00");

    [GreaterThanOrEqual("DateTimeOffsetValidSecond")]
    public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // Success

    public DateTimeOffset DateTimeOffsetValidSecond = DateTimeOffset.Parse("2026-10-18 07:00:00");

    [GreaterThanOrEqual("DateTimeValidSecond")]
    public DateTime DateTimeValid = DateTime.Parse("2026-10-18 07:00:00"); // Success

    public DateTime DateTimeValidSecond = DateTime.Parse("2026-10-18 07:00:00");

    [GreaterThanOrEqual("DecimalInvalidSecond")]
    public decimal DecimalInvalid = 3.3M; // ValidationError

    public decimal DecimalInvalidSecond = 3.2999M;

    [GreaterThanOrEqual("DecimalValidSecond")]
    public decimal DecimalValid = 3.3M; // Success

    public decimal DecimalValidSecond = 3.3M;

    [GreaterThanOrEqual("DoubleInvalidSecond")]
    public double DoubleInvalid = 3.3; // ValidationError

    public double DoubleInvalidSecond = 3.2999;

    [GreaterThanOrEqual("DoubleValidSecond")]
    public double DoubleValid = 3.3; // Success

    public double DoubleValidSecond = 3.3;

    [GreaterThanOrEqual("FloatInvalidSecond")]
    public float FloatInvalid = 3.3f; // ValidationError

    public float FloatInvalidSecond = 3.2999f;

    [GreaterThanOrEqual("FloatValidSecond")]
    public float FloatValid = 3.3f; // Success

    public float FloatValidSecond = 3.3f;

    [GreaterThanOrEqual("CollectionInvalidSecond")]
    public ICollection CollectionInvalid = new List<int>(){1,2}; // ValidationError

    public ICollection CollectionInvalidSecond = new List<int>(){1,2,3};

    [GreaterThanOrEqual("CollectionValidSecond")]
    public ICollection CollectionValid = new List<int>(){1,2,3}; // Success

    public ICollection CollectionValidSecond = new List<int>(){1,2,3};

    [GreaterThanOrEqual("EnumerableInvalidSecond")]
    public IEnumerable EnumerableInvalid = new List<int>(){1,2}; // ValidationError

    public IEnumerable EnumerableInvalidSecond = new List<int>(){1,2,3};

    [GreaterThanOrEqual("EnumerableValidSecond")]
    public IEnumerable EnumerableValid = new List<int>(){1,2,3}; // Success

    public IEnumerable EnumerableValidSecond = new List<int>(){1,2,3};

    [GreaterThanOrEqual("IntInvalidSecond")]
    public int IntInvalid = 0; // ValidationError

    public int IntInvalidSecond = 1;

    [GreaterThanOrEqual("IntValidSecond")]
    public int IntValid = 1; // Success

    public int IntValidSecond = 1;

    [GreaterThanOrEqual("LongInvalidSecond")]
    public long LongInvalid = 0; // ValidationError

    public long LongInvalidSecond = 1;

    [GreaterThanOrEqual("LongValidSecond")]
    public long LongValid = 1; // Success

    public long LongValidSecond = 1;

    [GreaterThanOrEqual("ShortInvalidSecond")]
    public short ShortInvalid = 0; // ValidationError

    public short ShortInvalidSecond = 1;

    [GreaterThanOrEqual("ShortValidSecond")]
    public short ShortValid = 1; // Success

    public short ShortValidSecond = 1;

    [GreaterThanOrEqual("StringInvalidSecond")]
    public string StringInvalid = "something"; // ValidationError

    public string StringInvalidSecond = "something";

    [GreaterThanOrEqual("StringValidSecond")]
    public string StringValid = "somethin"; // Success

    public string StringValidSecond = "something";

    [GreaterThanOrEqual("UintInvalidSecond")]
    public uint UintInvalid = 0; // ValidationError

    public uint UintInvalidSecond = 1;

    [GreaterThanOrEqual("UintValidSecond")]
    public uint UintValid = 1; // Success

    public uint UintValidSecond = 1;

    [GreaterThanOrEqual("UlongInvalidSecond")]
    public ulong UlongInvalid = 0; // ValidationError

    public ulong UlongInvalidSecond = 1;

    [GreaterThanOrEqual("UlongValidSecond")]
    public ulong UlongValid = 1; // Success

    public ulong UlongValidSecond = 1;

    [GreaterThanOrEqual("UshortInvalidSecond")]
    public ushort UshortInvalid = 0; // ValidationError

    public ushort UshortInvalidSecond = 1;

    [GreaterThanOrEqual("UshortValidSecond")]
    public ushort UshortValid = 1; // Success

    public ushort UshortValidSecond = 1;
}
```

### HexColor

Validates that a string is a valid hexadecimal color code.


```csharp
public class HexColorDto
{
    [HexColor]
    public string StringInvalid = "FFF"; // ValidationError

    [HexColor]
    public string StringValid = "#FFFFFF"; // Success
}
```

### In

The field under validation must be included in the given list of values.


```csharp
public class InDto
{
    [In("2026-10-17 07:00:00", "2026-10-19 07:00:00")]
    public DateTime DateTimeInvalid = DateTime.Parse("2026-10-18 07:00:00"); // ValidationError

    [In("2026-10-17 07:00:00", "2026-10-19 07:00:00")]
    public DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // ValidationError

    [In("2026-10-18 07:00:00", "2026-10-19 07:00:00")]
    public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // Success

    [In("2026-10-17", "2026-10-19 07:00:00")]
    public DateTime DateTimeValid = DateTime.Parse("2026-10-17"); // Success

    [In("0.000001", "1.2", "-0.000001")]
    public decimal DecimalInvalid = 3.3M; // ValidationError

    [In("0.000001", "1.2", "-0.000001")]
    public decimal DecimalValid = 1.2M; // Success

    [In("0.000001", "1.2", "-0.000001")]
    public double DoubleInvalid = 3.3; // ValidationError

    [In("0.000001", "1.2", "-0.000001")]
    public double DoubleValid = -0.000001; // Success

    [In("0.000001", "1.2", "-0.000001")]
    public float FloatInvalid = 3.3f; // ValidationError

    [In("0.000001", "1.2", "-0.000001")]
    public float FloatValid = 1.2f; // Success

    [In("1", "2", "3")]
    public int IntInvalid = 0; // ValidationError

    [In("1", "2", "3")]
    public int IntValid = 1; // Success

    [In("1", "2", "3")]
    public long LongInvalid = 0; // ValidationError

    [In("1", "2", "3")]
    public long LongValid = 1; // Success

    [In("1", "2", "3")]
    public short ShortInvalid = 0; // ValidationError

    [In("1", "2", "3")]
    public short ShortValid = 1; // Success

    [In("test1", "test2", "test3")]
    public string StringInvalid = "something"; // ValidationError

    [In("test1", "test2", "test3")]
    public string StringValid = "test2"; // Success

    [In("1", "2", "3")]
    public uint UintInvalid = 0; // ValidationError

    [In("1", "2", "3")]
    public uint UintValid = 1; // Success

    [In("1", "2", "3")]
    public ulong UlongInvalid = 0; // ValidationError

    [In("1", "2", "3")]
    public ulong UlongValid = 1; // Success

    [In("1", "2", "3")]
    public ushort UshortInvalid = 0; // ValidationError

    [In("1", "2", "3")]
    public ushort UshortValid = 1; // Success
}
```

### InArray

The field under validation must exist in anotherField's values.


```csharp
public class InArrayDto
{
    [InArray(nameof(InArrayDto.ExpectedCollection))]
    public int CollectionInvalid = 5; // ValidationError

    [InArray(nameof(InArrayDto.ExpectedCollection))]
    public int CollectionValid = 1; // Success

    [InArray(nameof(InArrayDto.ExpectedEnumerable))]
    public int EnumerableInvalid = 0; // ValidationError

    [InArray(nameof(InArrayDto.ExpectedEnumerable))]
    public int EnumerableValid = 2; // Success

    public ICollection ExpectedCollection = new List<int> { 1, 2, 3 };
    public IEnumerable ExpectedEnumerable = new List<int> { 1, 2, 3 }; }
```

### Ip

The field under validation must be an IP address.


```csharp
public class IpDto
{
    [Ip(IpAttribute.Modes.IPv4)]
    public IPAddress IPAddressInvalid = IPAddress.IPv6Any; // ValidationError

    [Ip(IpAttribute.Modes.IPv6)]
    public IPAddress IPAddressValid = IPAddress.Any; // Success

    [Ip(IpAttribute.Modes.IPv4)]
    public string StringInvalid = "2001:0db8:85a3:0000:0000:8a2e:0370:7334"; // ValidationError

    [Ip(IpAttribute.Modes.IPv6)]
    public string StringValid = "2001:0db8:85a3:0000:0000:8a2e:0370:7334"; // Success
}
```

### Json

The field under validation must be a valid JSON string.


```csharp
public class JsonDto
{
    [Json]
    public string StringInvalid = "\"{'name': 'John', 'age': 30}\""; // ValidationError

    [Json]
    public string StringValid = "{\"name\": \"John\", \"age\": 30, \"isVerified\": true}"; // Success
}
```

### LessThan

The field under validation must be less than the given field. The two fields must be of the same type.
Strings, numerics, arrays, and files are evaluated using the same conventions as the size rule.


```csharp
public class LessThanDto
{
    [LessThan("DateTimeInvalidSecond")]
    public DateTime DateTimeInvalid = DateTime.Parse("2026-10-18 07:00:00"); // ValidationError

    public DateTime DateTimeInvalidSecond = DateTime.Parse("2026-10-18 07:00:00");

    [LessThan("DateTimeOffsetInvalidSecond")]
    public DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // ValidationError

    public DateTimeOffset DateTimeOffsetInvalidSecond = DateTimeOffset.Parse("2026-10-18 07:00:00");

    [LessThan("DateTimeOffsetValidSecond")]
    public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // Success

    public DateTimeOffset DateTimeOffsetValidSecond = DateTimeOffset.Parse("2026-10-18 07:00:01");

    [LessThan("DateTimeValidSecond")]
    public DateTime DateTimeValid = DateTime.Parse("2026-10-18 07:00:00"); // Success

    public DateTime DateTimeValidSecond = DateTime.Parse("2026-10-18 07:00:01");

    [LessThan("DecimalInvalidSecond")]
    public decimal DecimalInvalid = 3.3M; // ValidationError

    public decimal DecimalInvalidSecond = 3.3M;

    [LessThan("DecimalValidSecond")]
    public decimal DecimalValid = 3.3M; // Success

    public decimal DecimalValidSecond = 3.2999M;

    [LessThan("DoubleInvalidSecond")]
    public double DoubleInvalid = 3.3; // ValidationError

    public double DoubleInvalidSecond = 3.3;

    [LessThan("DoubleValidSecond")]
    public double DoubleValid = 3.3; // Success

    public double DoubleValidSecond = 3.2999;

    [LessThan("FloatInvalidSecond")]
    public float FloatInvalid = 3.3f; // ValidationError

    public float FloatInvalidSecond = 3.3f;

    [LessThan("FloatValidSecond")]
    public float FloatValid = 3.3f; // Success

    public float FloatValidSecond = 3.2999f;

    [LessThan("CollectionInvalidSecond")]
    public ICollection CollectionInvalid = new List<int>() { 1, 2, 3 }; // ValidationError

    public ICollection CollectionInvalidSecond = new List<int>() { 1, 2, 3 };

    [LessThan("CollectionValidSecond")]
    public ICollection CollectionValid = new List<int>() { 1, 2, 3 }; // Success

    public ICollection CollectionValidSecond = new List<int>() { 1, 2, };

    [LessThan("EnumerableInvalidSecond")]
    public IEnumerable EnumerableInvalid = new List<int>() { 1, 2, 3 }; // ValidationError

    public IEnumerable EnumerableInvalidSecond = new List<int>() { 1, 2, 3 };

    [LessThan("EnumerableValidSecond")]
    public IEnumerable EnumerableValid = new List<int>() { 1, 2, 3 }; // Success

    public IEnumerable EnumerableValidSecond = new List<int>() { 1, 2, };

    [LessThan("IntInvalidSecond")]
    public int IntInvalid = 1; // ValidationError

    public int IntInvalidSecond = 1;

    [LessThan("IntValidSecond")]
    public int IntValid = 1; // Success

    public int IntValidSecond = 0;

    [LessThan("LongInvalidSecond")]
    public long LongInvalid = 1; // ValidationError

    public long LongInvalidSecond = 1;

    [LessThan("LongValidSecond")]
    public long LongValid = 1; // Success

    public long LongValidSecond = 0;

    [LessThan("ShortInvalidSecond")]
    public short ShortInvalid = 1; // ValidationError

    public short ShortInvalidSecond = 1;

    [LessThan("ShortValidSecond")]
    public short ShortValid = 1; // Success

    public short ShortValidSecond = 0;

    [LessThan("StringInvalidSecond")]
    public string StringInvalid = "something"; // ValidationError

    public string StringInvalidSecond = "something";

    [LessThan("StringValidSecond")]
    public string StringValid = "something"; // Success

    public string StringValidSecond = "somethin";

    [LessThan("UintInvalidSecond")]
    public uint UintInvalid = 1; // ValidationError

    public uint UintInvalidSecond = 1;

    [LessThan("UintValidSecond")]
    public uint UintValid = 1; // Success

    public uint UintValidSecond = 0;

    [LessThan("UlongInvalidSecond")]
    public ulong UlongInvalid = 1; // ValidationError

    public ulong UlongInvalidSecond = 1;

    [LessThan("UlongValidSecond")]
    public ulong UlongValid = 1; // Success

    public ulong UlongValidSecond = 0;

    [LessThan("UshortInvalidSecond")]
    public ushort UshortInvalid = 1; // ValidationError

    public ushort UshortInvalidSecond = 1;

    [LessThan("UshortValidSecond")]
    public ushort UshortValid = 1; // Success

    public ushort UshortValidSecond = 0;
}
```

### LessThanOrEqual

The field under validation must be less than or equal to the given field. The two fields must be of the same
type. Strings, numerics, arrays, and files are evaluated using the same conventions as the size rule.


```csharp
public class LessThanOrEqualDto
{
    [LessThanOrEqual("DateTimeInvalidSecond")]
    public DateTime DateTimeInvalid = DateTime.Parse("2026-10-18 07:00:00"); // ValidationError

    public DateTime DateTimeInvalidSecond = DateTime.Parse("2026-10-18 07:00:01");

    [LessThanOrEqual("DateTimeOffsetInvalidSecond")]
    public DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // ValidationError

    public DateTimeOffset DateTimeOffsetInvalidSecond = DateTimeOffset.Parse("2026-10-18 07:00:01");

    [LessThanOrEqual("DateTimeOffsetValidSecond")]
    public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // Success

    public DateTimeOffset DateTimeOffsetValidSecond = DateTimeOffset.Parse("2026-10-18 07:00:00");

    [LessThanOrEqual("DateTimeValidSecond")]
    public DateTime DateTimeValid = DateTime.Parse("2026-10-18 07:00:00"); // Success

    public DateTime DateTimeValidSecond = DateTime.Parse("2026-10-18 07:00:00");

    [LessThanOrEqual("DecimalInvalidSecond")]
    public decimal DecimalInvalid = 3.3M; // ValidationError

    public decimal DecimalInvalidSecond = 3.30001M;

    [LessThanOrEqual("DecimalValidSecond")]
    public decimal DecimalValid = 3.3M; // Success

    public decimal DecimalValidSecond = 3.3M;

    [LessThanOrEqual("DoubleInvalidSecond")]
    public double DoubleInvalid = 3.3; // ValidationError

    public double DoubleInvalidSecond = 3.30001;

    [LessThanOrEqual("DoubleValidSecond")]
    public double DoubleValid = 3.3; // Success

    public double DoubleValidSecond = 3.3;

    [LessThanOrEqual("FloatInvalidSecond")]
    public float FloatInvalid = 3.3f; // ValidationError

    public float FloatInvalidSecond = 3.30001f;

    [LessThanOrEqual("FloatValidSecond")]
    public float FloatValid = 3.3f; // Success

    public float FloatValidSecond = 3.3f;

    [LessThanOrEqual("CollectionInvalidSecond")]
    public ICollection CollectionInvalid = new List<int>() { 1, 2 }; // ValidationError

    public ICollection CollectionInvalidSecond = new List<int>() { 1, 2, 3 };

    [LessThanOrEqual("CollectionValidSecond")]
    public ICollection CollectionValid = new List<int>() { 1, 2 }; // Success

    public ICollection CollectionValidSecond = new List<int>() { 1, 2 };

    [LessThanOrEqual("EnumerableInvalidSecond")]
    public IEnumerable EnumerableInvalid = new List<int>() { 1, 2 }; // ValidationError

    public IEnumerable EnumerableInvalidSecond = new List<int>() { 1, 2, 3 };

    [LessThanOrEqual("EnumerableValidSecond")]
    public IEnumerable EnumerableValid = new List<int>() { 1, 2 }; // Success

    public IEnumerable EnumerableValidSecond = new List<int>() { 1, 2 };

    [LessThanOrEqual("IntInvalidSecond")]
    public int IntInvalid = 1; // ValidationError

    public int IntInvalidSecond = 0;

    [LessThanOrEqual("IntValidSecond")]
    public int IntValid = 1; // Success

    public int IntValidSecond = 1;

    [LessThanOrEqual("LongInvalidSecond")]
    public long LongInvalid = 1; // ValidationError

    public long LongInvalidSecond = 0;

    [LessThanOrEqual("LongValidSecond")]
    public long LongValid = 1; // Success

    public long LongValidSecond = 1;

    [LessThanOrEqual("ShortInvalidSecond")]
    public short ShortInvalid = 1; // ValidationError

    public short ShortInvalidSecond = 0;

    [LessThanOrEqual("ShortValidSecond")]
    public short ShortValid = 1; // Success

    public short ShortValidSecond = 1;

    [LessThanOrEqual("StringInvalidSecond")]
    public string StringInvalid = "something"; // ValidationError

    public string StringInvalidSecond = "somethin";

    [LessThanOrEqual("StringValidSecond")]
    public string StringValid = "something"; // Success

    public string StringValidSecond = "something";

    [LessThanOrEqual("UintInvalidSecond")]
    public uint UintInvalid = 1; // ValidationError

    public uint UintInvalidSecond = 0;

    [LessThanOrEqual("UintValidSecond")]
    public uint UintValid = 1; // Success

    public uint UintValidSecond = 1;

    [LessThanOrEqual("UlongInvalidSecond")]
    public ulong UlongInvalid = 1; // ValidationError

    public ulong UlongInvalidSecond = 0;

    [LessThanOrEqual("UlongValidSecond")]
    public ulong UlongValid = 1; // Success

    public ulong UlongValidSecond = 1;

    [LessThanOrEqual("UshortInvalidSecond")]
    public ushort UshortInvalid = 1; // ValidationError

    public ushort UshortInvalidSecond = 0;

    [LessThanOrEqual("UshortValidSecond")]
    public ushort UshortValid = 1; // Success

    public ushort UshortValidSecond = 1;
}
```

### Lowercase

The field under validation must be lowercase.


```csharp
public class LowercaseDto
{
    [Lowercase]
    public string StringInvalid = "sOmething"; // ValidationError

    [Lowercase]
    public string StringValid = "something"; // Success
}
```

### MacAddress

The field under validation must be a MAC address..


```csharp
public class MacAddressDto
{
    [MacAddress]
    public string StringInvalid = "192.168.1.1"; // ValidationError

    [MacAddress]
    public string StringValid = "AC:DE:48:23:45:67"; // Success
}
```

### Max

The field under validation must be less than or equal to a maximum value. Strings, numerics, arrays, and files
are evaluated in the same fashion as the size rule.


```csharp
public class MaxDto
{
    [Max("2026-10-18 07:00:00")]
    public DateTime DateTimeInvalid = DateTime.Parse("2026-10-18 07:00:01"); // ValidationError

    [Max("2026-10-18 07:00:00")]
    public DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:01"); // ValidationError

    [Max("2026-10-18 07:00:00")]
    public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // Success

    [Max("2026-10-18 07:00:00")]
    public DateTime DateTimeValid = DateTime.Parse("2026-10-18 07:00:00"); // Success

    [Max("3.2")]
    public decimal DecimalInvalid = 3.3M; // ValidationError

    [Max("3.3")]
    public decimal DecimalValid = 3.3M; // Success

    [Max("3.2")]
    public double DoubleInvalid = 3.3; // ValidationError

    [Max("3.4")]
    public double DoubleValid = 3.3; // Success

    [Max("3.2")]
    public float FloatInvalid = 3.3f; // ValidationError

    [Max("3.3")]
    public float FloatValid = 3.3f; // Success

    [Max("4")]
    public ICollection CollectionInvalid = new List<int>() { 1, 2, 3 }; // ValidationError

    [Max("3")]
    public ICollection CollectionValid = new List<int>() { 1, 2, 3 }; // Success

    [Max("4")]
    public IEnumerable EnumerableInvalid = new List<int>() { 1, 2, 3 }; // ValidationError

    [Max("3")]
    public IEnumerable EnumerableValid = new List<int>() { 1, 2, 3 }; // Success

    [Max("3")]
    public int IntInvalid = 4; // ValidationError

    [Max("3")]
    public int IntValid = 1; // Success

    [Max("3")]
    public long LongInvalid = 4; // ValidationError

    [Max("3")]
    public long LongValid = 1; // Success

    [Max("3")]
    public short ShortInvalid = 4; // ValidationError

    [Max("3")]
    public short ShortValid = 1; // Success

    [Max("4")]
    public string StringInvalid = "something"; // ValidationError

    [Max("4")]
    public string StringValid = "test"; // Success

    [Max("3")]
    public uint UintInvalid = 4; // ValidationError

    [Max("3")]
    public uint UintValid = 1; // Success

    [Max("3")]
    public ulong UlongInvalid = 4; // ValidationError

    [Max("3")]
    public ulong UlongValid = 1; // Success

    [Max("3")]
    public ushort UshortInvalid = 4; // ValidationError

    [Max("3")]
    public ushort UshortValid = 1; // Success
}
```

### Min

The field under validation must be grater than or equal to a minimum value. Strings, numerics, arrays, and
files are evaluated in the same fashion as the size rule.


```csharp
public class MinDto
{
    [Min("2026-10-18 07:00:01")]
    public DateTime DateTimeInvalid = DateTime.Parse("2026-10-18 07:00:00"); // ValidationError

    [Min("2026-10-18 07:00:01")]
    public DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // ValidationError

    [Min("2026-10-18 07:00:00")]
    public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:02"); // Success

    [Min("2026-10-18 07:00:00")]
    public DateTime DateTimeValid = DateTime.Parse("2026-10-18 07:00:00"); // Success

    [Min("3.3")]
    public decimal DecimalInvalid = 3.2M; // ValidationError

    [Min("3.3")]
    public decimal DecimalValid = 3.3M; // Success

    [Min("3.3")]
    public double DoubleInvalid = 3.2; // ValidationError

    [Min("3.3")]
    public double DoubleValid = 3.3; // Success

    [Min("3.3")]
    public float FloatInvalid = 3.2f; // ValidationError

    [Min("3.3")]
    public float FloatValid = 3.3f; // Success

    [Min("3")]
    public ICollection CollectionInvalid = new List<int>() { 1, 2 }; // ValidationError

    [Min("3")]
    public ICollection CollectionValid = new List<int>() { 1, 2, 3 }; // Success

    [Min("3")]
    public IEnumerable EnumerableInvalid = new List<int>() { 1, 2 }; // ValidationError

    [Min("3")]
    public IEnumerable EnumerableValid = new List<int>() { 1, 2, 4, 5 }; // Success

    [Min("3")]
    public int IntInvalid = 1; // ValidationError

    [Min("3")]
    public int IntValid = 4; // Success

    [Min("3")]
    public long LongInvalid = 1; // ValidationError

    [Min("3")]
    public long LongValid = 4; // Success

    [Min("3")]
    public short ShortInvalid = 1; // ValidationError

    [Min("3")]
    public short ShortValid = 4; // Success

    [Min("3")]
    public string StringInvalid = "te"; // ValidationError

    [Min("3")]
    public string StringValid = "test"; // Success

    [Min("3")]
    public uint UintInvalid = 1; // ValidationError

    [Min("3")]
    public uint UintValid = 4; // Success

    [Min("3")]
    public ulong UlongInvalid = 1; // ValidationError

    [Min("3")]
    public ulong UlongValid = 4; // Success

    [Min("3")]
    public ushort UshortInvalid = 1; // ValidationError

    [Min("3")]
    public ushort UshortValid = 4; // Success
}
```

### Modes

Modes for validation logic.

### NotIn

The field under validation must not be included in the given list of values.


```csharp
public class NotInDto
{
    [NotIn("2026-10-17 07:00:00", "2026-10-19 07:00:00")]
    public DateTime DateTimeInvalid = DateTime.Parse("2026-10-18 07:00:00"); // ValidationError

    [NotIn("2026-10-17 07:00:00", "2026-10-19 07:00:00")]
    public DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // ValidationError

    [NotIn("2026-10-18 07:00:00", "2026-10-19 07:00:00")]
    public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // Success

    [NotIn("2026-10-18 07:00:00", "2026-10-19 07:00:00")]
    public DateTime DateTimeValid = DateTime.Parse("2026-10-18 07:00:00"); // Success

    [NotIn("3.2", "3.4")]
    public decimal DecimalInvalid = 3.2M; // ValidationError

    [NotIn("3.2", "3.4")]
    public decimal DecimalValid = 3.3M; // Success

    [NotIn("3.2", "3.4")]
    public double DoubleInvalid = 3.2; // ValidationError

    [NotIn("3.2", "3.4")]
    public double DoubleValid = 3.3; // Success

    [NotIn("3.2", "3.4")]
    public float FloatInvalid = 3.2f; // ValidationError

    [NotIn("3.2", "3.4")]
    public float FloatValid = 3.3f; // Success

    [NotIn("1", "2")]
    public ICollection CollectionInvalid = new List<int>() { 1, 2, 3 }; // ValidationError

    [NotIn("1", "2")]
    public ICollection CollectionValid = new List<int>() { 4, 5, 6 }; // Success

    [NotIn("1", "2")]
    public IEnumerable EnumerableInvalid = new List<int>() { 1, 2, 3 }; // ValidationError

    [NotIn("1", "2")]
    public IEnumerable EnumerableValid = new List<int>() { 4, 5, 6 }; // Success

    [NotIn("1", "2")]
    public int IntInvalid = 1; // ValidationError

    [NotIn("1", "2")]
    public int IntValid = 3; // Success

    [NotIn("1", "2")]
    public long LongInvalid = 1; // ValidationError

    [NotIn("1", "2")]
    public long LongValid = 3; // Success

    [NotIn("1", "2")]
    public short ShortInvalid = 1; // ValidationError

    [NotIn("1", "2")]
    public short ShortValid = 3; // Success

    [NotIn("something", "test")]
    public string StringInvalid = "something"; // ValidationError

    [NotIn("test", "else")]
    public string StringValid = "something"; // Success

    [NotIn("1", "2")]
    public uint UintInvalid = 1; // ValidationError

    [NotIn("1", "2")]
    public uint UintValid = 3; // Success

    [NotIn("1", "2")]
    public ulong UlongInvalid = 1; // ValidationError

    [NotIn("1", "2")]
    public ulong UlongValid = 3; // Success

    [NotIn("1", "2")]
    public ushort UshortInvalid = 1; // ValidationError

    [NotIn("1", "2")]
    public ushort UshortValid = 3; // Success
}
```

### NotRegexp

The field under validation must match the given regular expression.


```csharp
public class NotRegexpDto
{
    [NotRegexp("^[0-9]+$")]
    public string StringInvalid = "123"; // ValidationError

    [NotRegexp("^[0-9]+$")]
    public string StringValid = "abc"; // Success
}
```

### Regexp

The field under validation must match the given regular expression.


```csharp
public class RegexpDto
{
    [Regexp("^[0-9]+$")]
    public string StringInvalid = "abc"; // ValidationError

    [Regexp("^[0-9]+$")]
    public string StringValid = "123"; // Success
}
```

### Required

The field under validation must be present in the input data and not empty.


```csharp
public class RequiredDto
{
    [Required]
    public object? ObjectInvalid = null; // ValidationError

    [Required]
    public object ObjectValid = new(); // Success
}
```

### Same

The given field must match the field under validation.


```csharp
public class SameDto
{
    [Same("ObjectInvalidSecond")]
    public object ObjectInvalid = 2; // ValidationError

    public object ObjectInvalidSecond = 3;

    [Same("ObjectValidSecond")]
    public object ObjectValid = "test"; // Success

    public object ObjectValidSecond = "test";
}
```

### Size

The field under validation must have a size matching the given value. For string data, value corresponds to
the number of characters. For numeric data, value corresponds to an expected value. For collection, size
corresponds to the count of the collection.


```csharp
public class SizeDto
{
    [Size("2026-10-18 07:00:01")]
    public DateTime DateTimeInvalid = DateTime.Parse("2026-10-18 07:00:00"); // ValidationError

    [Size("2026-10-18 07:00:01")]
    public DateTimeOffset DateTimeOffsetInvalid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // ValidationError

    [Size("2026-10-18 07:00:00")]
    public DateTimeOffset DateTimeOffsetValid = DateTimeOffset.Parse("2026-10-18 07:00:00"); // Success

    [Size("2026-10-18 07:00:00")]
    public DateTime DateTimeValid = DateTime.Parse("2026-10-18 07:00:00"); // Success

    [Size("3.3")]
    public decimal DecimalInvalid = 3.2M; // ValidationError

    [Size("3.3")]
    public decimal DecimalValid = 3.3M; // Success

    [Size("3.3")]
    public double DoubleInvalid = 3.3; // ValidationError

    [Size("3.3")]
    public double DoubleValid = 3.3; // Success

    [Size("3.3")]
    public float FloatInvalid = 3.2f; // ValidationError

    [Size("3.3")]
    public float FloatValid = 3.3f; // Success

    [Size("3")]
    public ICollection CollectionInvalid = new List<int>() { 1, 2 }; // ValidationError

    [Size("3")]
    public ICollection CollectionValid = new List<int>() { 1, 2, 3 }; // Success

    [Size("3")]
    public IEnumerable EnumerableInvalid = new List<int>() { 1, 2 }; // ValidationError

    [Size("3")]
    public IEnumerable EnumerableValid = new List<int>() { 1, 2, 3 }; // Success

    [Size("3")]
    public int IntInvalid = 1; // ValidationError

    [Size("3")]
    public int IntValid = 3; // Success

    [Size("3")]
    public long LongInvalid = 1; // ValidationError

    [Size("3")]
    public long LongValid = 3; // Success

    [Size("3")]
    public short ShortInvalid = 1; // ValidationError

    [Size("3")]
    public short ShortValid = 3; // Success

    [Size("3")]
    public string StringInvalid = "something"; // ValidationError

    [Size("3")]
    public string StringValid = "tes"; // Success

    [Size("3")]
    public uint UintInvalid = 1; // ValidationError

    [Size("3")]
    public uint UintValid = 3; // Success

    [Size("3")]
    public ulong UlongInvalid = 1; // ValidationError

    [Size("3")]
    public ulong UlongValid = 3; // Success

    [Size("3")]
    public ushort UshortInvalid = 1; // ValidationError

    [Size("3")]
    public ushort UshortValid = 3; // Success
}
```

### StartsWith

The field under validation must start with one of the given values.


```csharp
public class StartsWithDto
{
    [StartsWith("end", "start")]
    public string StringInvalid = "something"; // ValidationError

    [StartsWith("some", "start")]
    public string StringValid = "something"; // Success
}
```

### Uppercase

The field under validation must be uppercase.


```csharp
public class UppercaseDto
{
    [Uppercase]
    public string StringInvalid = "something"; // ValidationError

    [Uppercase]
    public string StringValid = "SOMETHING"; // Success
}
```

### Url

The field under validation must be a valid URL.

If you would like to specify the URL protocols that should be considered valid, you may pass the protocols
as validation rule parameters.


```csharp
public class UrlDto
{
    [Url("https")]
    public string StringInvalid = "http://example.com"; // ValidationError

    [Url]
    public string StringValid = "http://example.com"; // Success
}
```

