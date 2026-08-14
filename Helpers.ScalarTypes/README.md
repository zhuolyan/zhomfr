# Helpers for scalar types

A collection of convenient and productive extension methods for primitive types in C# (`string` and `decimal`), designed
to simplify everyday data processing and formatting tasks.

## 📦 Installation

Add the extension files to your project in the appropriate namespaces (`Zhomfr.Helpers.ScalarTypes` and
`Zhomfr.Helpers.ScalarTypes.Strings`).

---

## 🛠️ Features and Examples

### 1. String Modification (`ModificationExtensions`)

This module provides methods for changing case, cleaning, masking, replacing, and formatting strings.

#### `Mask` (Masking Sensitive Data)

Ideal for hiding parts of email addresses, phone numbers, or credit card numbers.

```csharp
string phone = "+380991234567";
string masked = phone.Mask(4, -4, '*'); // Result: "+380********4567"

```

#### Case and Identifier Styles (`ToSnakeCase`, `ToCamelCase`, `ToPascalCase`, `ToKebabCase`)

```csharp
string text = "HelloWorld";

string snake = text.ToSnakeCase();   // "hello_world"
string kebab = text.ToKebabCase();   // "hello-world"
string camel = "hello_world".ToCamelCase(); // "helloWorld"
string pascal = "hello_world".ToPascalCase(); // "HelloWorld"

```

#### `ChopStart` / `ChopEnd`

Removes specific prefixes or suffixes from a string.

```csharp
string filename = "prefix_data_suffix.txt";
string clean = filename.ChopStart("prefix_").ChopEnd(".txt"); // "data_suffix"

```

#### `Squish` and `Deduplicate`

Removes extraneous whitespace (similar to SQL `TRIM` or Laravel's `Str::squish`).

```csharp
string messy = "   Hello    world!   ";
string clean = messy.Squish(); // "Hello world!"

```

#### `Excerpt`

Extracts a fragment around a search term with an omission indicator.

```csharp
string article = "This is a long article text where we search for a keyword for context.";
string excerpt = article.Excerpt("keyword", radius: 10); 
// Result: "...text where we search for a keyword for..."

```

---

### 2. Substrings (`SubstringsExtensions`)

Methods for searching, trimming, and extracting text fragments.

#### `After` / `Before`

```csharp
string path = "api/v1/users";
string after = path.After("api/");   // "v1/users"
string before = path.Before("/users"); // "api/v1"

```

#### `Between`

Extracts text between two markers.

```csharp
string html = "<div>Hello, Oleg</div>";
string name = html.Between("<div>", "</div>"); // "Hello, Oleg"

```

#### `Limit`

Truncates a string to a specified length with optional whole word preservation.

```csharp
string sentence = "This text is way too long to display.";
string shortText = sentence.Limit(18, "...", preserveWords: true); 
// Result: "This text ..."

```

---

### 3. General String Utilities (`OtherExtensions`)

Useful functions for analysis and counting.

#### `WordCount` and `SubstringCount`

```csharp
string phrase = "Cat eats fish, cat sleeps.";
int words = phrase.WordCount();          // 5
int count = phrase.SubstringCount("cat"); // 2 (case-dependent)

```

#### `Initials`

Generates initials from a full name.

```csharp
string fullName = "Oleg Ivanov";
string initials = fullName.Initials(); // "OI"

```

---

### 4. Decimal Extensions (`DecimalExtensions`)

Helper methods for working with `decimal` floating-point numbers.

```csharp
decimal price = 1234.56m;

int intDigits = price.GetDigitsBeforeSeparator(); // 4
int totalDigits = price.GetDigits();              // 6

```

---

## 📄 License

This project is licensed under the [MIT](https://github.com/zhuolyan/zhomfr/blob/master/LICENSE) License.
