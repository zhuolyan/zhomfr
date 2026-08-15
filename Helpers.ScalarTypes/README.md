# Helpers for scalar types

A collection of convenient and productive extension methods for primitive types in C# (`string` and `decimal`), designed
to simplify everyday data processing and formatting tasks.

## 📦 Installation

Install package:

```shell
dotnet add package Zhomfr.Helpers.ScalarTypes
```

After add namespace in using section:

```csharp
using Zhomfr.Helpers.ScalarTypes;
using Zhomfr.Helpers.ScalarTypes.Strings;
```

---

## Available Methods

### String Methods

<table><tr>
<td valign="top">

- [After](#after)
- [AfterLast](#afterlast)
- [Before](#before)
- [BeforeLast](#beforelast)
- [Between](#between)
- [BetweenFirst](#betweenfirst)
- [BetweenLast](#betweenlast)
- [CharAt](#charat)
- [ChopStart](#chopstart)
- [ChopEnd](#chopend)
- [Deduplicate](#deduplicate)
- [EndWith](#endwith)
- [Excerpt](#excerpt)
- [FromBase64](#frombase64)

</td>

<td valign="top">

- [Initials](#initials)
- [LcFirst](#lcfirst)
- [Limit](#limit)
- [Mask](#mask)
- [PadBoth](#padboth)
- [Remove](#remove)
- [RemoveIgnoreCase](#removeignorecase)
- [Replace](#replace)
- [ReplaceFirst](#replacefirst)
- [ReplaceLast](#replacelast)
- [ReplaceMatches](#replacematches)
- [Reverse](#reverse)
- [Squish](#squish)
- [StartWith](#startwith)

</td>

<td valign="top">

- [SubstringCount](#substringcount)
- [SubstringReplace](#substringreplace)
- [Swap](#swap)
- [ToBase64](#tobase64)
- [ToCamelCase](#tocamelcase)
- [ToSnakeCase](#tosnakecase)
- [ToKebabCase](#tokebabcase)
- [ToPascalCase](#topascalcase)
- [UcFirst](#ucfirst)
- [UcSplit](#ucsplit)
- [UcWord](#ucword)
- [WordCount](#wordcount)
- [GetDigits](#getdigits)
- [GetDecimal](#getdecimal)

</td>

</tr></table>

### Decimal Methods

<table><tr>
<td valign="top">

- [GetDigitsBeforeSeparator](#getdigitsbeforeseparator)

</td>

<td valign="top">

- [GetDigits](#getdigits)

</td>

<td valign="top">

- [GetDecimal](#getdecimal)

</td>

</tr></table>

---

## 🛠️ Features and Examples

#### After

Returns everything after the given value in a string, or the entire string if the value is not found.

```csharp
string value = "one/two/three";
string result = value.After("/"); // two/three
```

#### AfterLast

Returns everything after the last occurrence of the given value in a string, or the entire string if the value is not
found.

```csharp
string value = "one/two/three";
string result = value.AfterLast("/"); // three
```

#### Before

Returns everything before the given value in a string.

```csharp
string value = "one/two/three";
string result = value.Before("/"); // one
```

#### BeforeLast

Returns everything before the last occurrence of the given value in a string.

```csharp
string value = "one/two/three";
string result = value.BeforeLast("/"); // one/two
```

#### Between

Returns the portion of a string between two specified values.

```csharp
string value = "[hello] [world]";
string result = value.Between("[", "]"); // hello] [world
```

#### BetweenFirst

Returns the smallest possible portion of a string between two specified values.

```csharp
string value = "[hello] [world]";
string result = value.BetweenFirst("[", "]"); // hello
```

#### BetweenLast

Returns the last smallest possible portion of a string between two specified values.

```csharp
string value = "[hello] [world]";
string result = value.BetweenLast("[", "]"); // world
```

#### CharAt

Returns the character at the specified index, or null if the index is out of bounds.

```csharp
string value = "hello";
char? result = value.CharAt(1); // e
```

#### ChopStart

Removes the first occurrence of any of the specified values from the start of the string.

```csharp
string value = "https://example.com";

string result1 = value.ChopStart("https://"); // example.com
string result2 = value.ChopStart("https://", "http://"); // example.com
```

#### ChopEnd

Removes the first occurrence of any of the specified values from the end of the string.

```csharp
string value = "example.com/";

string result1 = value.ChopEnd("/"); // example.com
string result2 = value.ChopEnd("/", "."); // example.com
```

#### Deduplicate

Replaces consecutive instances of the specified substring with a single instance in the string. By default, deduplicates
spaces.

```csharp
string value = "hello   world";
string result = value.Deduplicate(); // hello world
```

#### EndWith

Ends the string with a single instance of the specified value if it does not already end with it.

```csharp
string value = "example";
string result = value.EndWith(".com"); // example.com
```

#### Excerpt

Extracts an excerpt from the string that matches the first instance of the specified phrase.

```csharp
string value = "The quick brown fox jumps over the lazy dog";
string result = value.Excerpt("fox", radius: 5); // ...own fox jump...
```

#### FromBase64

Decodes the given Base64 string, or returns null if the string is not a valid Base64 encoding.

```csharp
string value = "SGVsbG8=";
string result = value.FromBase64(); // Hello
```

#### GetDecimal

Gets the total number of digits before and after the decimal separator.

```csharp
decimal value = 123.45m;
int result = value.GetDecimal(); // 2
```

#### GetDigitsBeforeSeparator

Gets the number of digits in the integer part of the decimal value.

```csharp
decimal value = 123.45m;
int result = value.GetDigitsBeforeSeparator(); // 3
```

#### GetDigits

Gets the total number of digits before and after the decimal separator.

```csharp
decimal value = 123.45m;
int result = value.GetDigits(); // 5
```

#### Initials

Returns the character at the specified index, or null if the index is out of bounds.

```csharp
string value = "John Ronald Reuel Tolkien";
string result = value.Initials(); // JRRT
```

#### LcFirst

Returns the string with its first character converted to lowercase.

```csharp
string value = "Hello";
string result = value.LcFirst(); // hello
```

#### Limit

Truncates the string to the specified length, optionally appending a custom string to the end of the truncated result.
Can preserve complete words at the nearest word boundary when specified.

```csharp
string value = "The quick brown fox";
string result = value.Limit(12); // The quick ...
```

#### Mask

Masks a portion of a string with a repeated character, which can be used to obfuscate segments of sensitive strings such
as email addresses and phone numbers.

```csharp
string value = "john@example.com";
string result = value.Mask(4, 4); // john*********e.com
```

#### PadBoth

Pads both sides of a string with another string until the final string reaches the specified length.

```csharp
string value = "title";
string result = value.PadBoth(9, "-"); // --title--
```

#### Remove

Removes the specified values from the string in a case-sensitive manner.

```csharp
string value = "a-b-c";
string result = value.Remove("-"); // abc
```

#### RemoveIgnoreCase

Removes the specified values from the string in a case-insensitive manner.

```csharp
string value = "Hello HELLO";
string result = value.RemoveIgnoreCase("hello"); //  
```

#### Replace

Replaces a given value in the string sequentially using an array of replacement values.

```csharp
string value = "a-b-c";
string result = value.Replace("-", ["1", "2"]); // a1b2c
```

#### ReplaceFirst

Replaces the first occurrence of a specified string with another string, with an option to control case sensitivity.

```csharp
string value = "foo foo";
string result = value.ReplaceFirst("foo", "bar"); // bar foo
```

#### ReplaceLast

Replaces the last occurrence of a specified string with another string, with an option to control case sensitivity.

```csharp
string value = "foo foo";
string result = value.ReplaceLast("foo", "bar"); // foo bar
```

#### ReplaceMatches

Replaces all portions of a string matching a regular expression pattern with the specified replacement string.

```csharp
string value = "foo123 bar456";
string result = value.ReplaceMatches(@"\d+", "#"); // foo# bar#
```

#### Reverse

Replaces all portions of a string matching a regular expression pattern with the specified replacement string.

```csharp
string value = "hello";
string result = value.Reverse(); // olleh
```

#### Squish

Removes all extraneous white space from the string, including extra white space between words.

```csharp
string value = "  hello   world  ";
string result = value.Squish(); // hello world
```

#### StartWith

Removes all extraneous white space from the string, including extra white space between words.

```csharp
string value = "example.com";
string result = value.StartWith("https://"); // https://example.com
```

#### SubstringCount

Returns the number of occurrences of a given value in the string.

```csharp
string value = "one two one";
int result = value.SubstringCount("one"); // 2
```

#### SubstringReplace

Replaces text within a portion of a string, starting at the specified position and replacing the number of characters
specified by the length. Passing 0 for the length inserts the string without replacing any existing characters.

```csharp
string value = "hello world";
string result = value.SubstringReplace("C#", 6, 5); // hello C#
```

#### Swap

Replaces multiple values in the string.

```csharp
string value = "hello world";
string result = value.Swap(new() { ["hello"] = "hi", ["world"] = "there" }); // hi there
```

#### ToBase64

Converts the given string to its Base64-encoded representation.

```csharp
string value = "Hello";
string result = value.ToBase64(); // SGVsbG8=
```

#### ToCamelCase

Converts the given string to its Base64-encoded representation.

```csharp
string value = "hello_world";
string result = value.ToCamelCase(); // helloWorld
```

#### ToSnakeCase

Converts the given string to snake_case.

```csharp
string value = "helloWorld";
string result = value.ToSnakeCase(); // hello_world
```

#### ToKebabCase

Converts the given string to camelCase.

```csharp
string value = "hello world";
string result = value.ToKebabCase(); // hello-world
```

#### ToPascalCase

Converts the string to kebab-case.

```csharp
string value = "hello world";
string result = value.ToPascalCase(); // HelloWorld
```

#### UcFirst

Capitalizes the first character of the given string.

```csharp
string value = "hello";
string result = value.UcFirst(); // Hello
```

#### UcSplit

Splits the string into a list by uppercase characters.

```csharp
string value = "HelloWorld";
List<string> result = value.UcSplit(); // ["Hello", "World"]
```

#### UcWord

Converts the first character of each word in the given string to uppercase.

```csharp
string value = "hello world";
string result = value.UcWord(); // Hello World
```

#### WordCount

Returns the number of words that the string contains.

```csharp
string value = "hello world";
int result = value.WordCount(); // 2
```