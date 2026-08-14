using System.Text;
using System.Text.RegularExpressions;

namespace Zhomfr.Helpers.ScalarTypes.Strings;

public static partial class ModificationExtensions
{
    [GeneratedRegex("([a-z0-9])([A-Z])")]
    private static partial Regex SnakeCaseStep1();

    [GeneratedRegex("([A-Z])([A-Z][a-z])")]
    private static partial Regex SnakeCaseStep2();

    [GeneratedRegex(@"\s+")]
    private static partial Regex SquishRegex();

    extension(string value)
    {
        /// <summary>Removes the first occurrence of any of the specified values from the start of the string.</summary>
        /// <param name="parts">The values to remove if they appear at the start of the string.</param>
        /// <returns>The resulting string after removing the starting value, or the original string if no match is found.</returns>
        public string ChopStart(params string[] parts)
        {
            foreach (string needle in parts) {
                if (value.StartsWith(needle)) {
                    return value[needle.Length..];
                }
            }

            return value;
        }

        /// <summary>Removes the first occurrence of any of the specified values from the end of the string.</summary>
        /// <param name="parts">The values to remove if they appear at the end of the string.</param>
        /// <returns>The resulting string after removing the ending value, or the original string if no match is found.</returns>
        public string ChopEnd(params string[] parts)
        {
            foreach (string needle in parts) {
                if (value.EndsWith(needle)) {
                    return value[..^needle.Length];
                }
            }

            return value;
        }

        /// <summary>
        ///     Replaces consecutive instances of the specified substring with a single instance in the string. By default,
        ///     deduplicates spaces.
        /// </summary>
        /// <param name="search">The substring to deduplicate.</param>
        /// <returns>The resulting string with consecutive duplicate substrings replaced by a single instance.</returns>
        public string Deduplicate(string search = " ")
        {
            return Regex.Replace(value, $"({Regex.Escape(search)})+", search);
        }

        /// <summary>Ends the string with a single instance of the specified value if it does not already end with it.</summary>
        /// <param name="end">The value to append to the string.</param>
        /// <returns>The resulting string ending with the specified value.</returns>
        public string EndWith(string end)
        {
            if (string.IsNullOrWhiteSpace(end) || value.EndsWith(end)) {
                return value;
            }

            return value + end;
        }

        /// <summary>Extracts an excerpt from the string that matches the first instance of the specified phrase.</summary>
        /// <param name="search">The phrase to search for within the string.</param>
        /// <param name="radius">The number of characters to include on each side of the truncated string (defaults to 100).</param>
        /// <param name="omission">The string to prepend and append as an omission indicator.</param>
        /// <returns>The extracted excerpt containing the matched phrase.</returns>
        public string Excerpt(string search, int radius = 100, string omission = "...")
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(search)) {
                return value;
            }

            int start = value.IndexOf(search, StringComparison.Ordinal);
            int end   = start + search.Length + radius;

            StringBuilder result = new();

            start -= radius;

            if (start <= 0) {
                start = 0;
            } else {
                result.Append(omission);
            }

            if (end > value.Length) {
                result.Append(value[start..]);
            } else {
                result.Append(value[start..end]);
                result.Append(omission);
            }

            return result.ToString();
        }

        /// <summary>Decodes the given Base64 string, or returns null if the string is not a valid Base64 encoding.</summary>
        /// <returns>The decoded string, or null if the input is not valid Base64.</returns>
        public string? FromBase64()
        {
            try {
                return string.IsNullOrEmpty(value) ? null : Encoding.UTF8.GetString(Convert.FromBase64String(value));
            } catch (Exception) {
                return null;
            }
        }

        /// <summary>Returns the string with its first character converted to lowercase.</summary>
        /// <returns>The resulting string with a lowercase first character.</returns>
        public string LcFirst()
        {
            if (string.IsNullOrEmpty(value) || char.IsLower(value[0])) {
                return value;
            }

            return char.ToLower(value[0]) + value[1..];
        }

        /// <summary>
        ///     Masks a portion of a string with a repeated character, which can be used to obfuscate segments of sensitive
        ///     strings such as email addresses and phone numbers.
        /// </summary>
        /// <param name="start">The character index at which to start masking.</param>
        /// <param name="end">The optional character index from the end at which to stop masking.</param>
        /// <param name="mask">The character used to replace the masked portion.</param>
        /// <returns>The resulting masked string.</returns>
        public string Mask(int start, int end = 0, char mask = '*')
        {
            if (string.IsNullOrEmpty(value)) {
                return value;
            }

            int endOffset   = Math.Abs(end);
            int startOffset = Math.Abs(start);

            int resultStartOffset = 0;
            int resultEndOffset   = 0;

            switch (start) {
                case < 0:
                    resultEndOffset = startOffset;

                    break;
                case > 0:
                    resultStartOffset = startOffset;

                    break;
            }

            switch (end) {
                case < 0:
                    resultStartOffset = Math.Max(resultStartOffset, endOffset);

                    break;
                case > 0:
                    resultEndOffset = Math.Max(resultEndOffset, startOffset);

                    break;
            }

            if (resultStartOffset >= value.Length) {
                return value;
            }

            resultEndOffset = value.Length - resultEndOffset;

            if (resultEndOffset < 0) {
                resultEndOffset = 0;
            }

            return string.Create(value.Length,
                                 (value, mask, resultStart: resultStartOffset, resultEnd: resultEndOffset),
                                 (chars, state) =>
                                 {
                                     state.value.AsSpan().CopyTo(chars);
                                     chars[state.resultStart..state.resultEnd].Fill(state.mask);
                                 });
        }

        /// <summary>Pads both sides of a string with another string until the final string reaches the specified length.</summary>
        /// <param name="length">The total length of the resulting string.</param>
        /// <param name="pad">The string to use for padding.</param>
        /// <returns>The resulting padded string.</returns>
        public string PadBoth(int length, string pad = " ")
        {
            int totalPadLength = length - value.Length;

            if (totalPadLength <= 0) {
                return value;
            }

            int leftPadIndex  = totalPadLength / 2;
            int rightPadIndex = length - (totalPadLength - leftPadIndex);

            return string.Create(length,
                                 (leftPadIndex, rightPad: rightPadIndex, pad),
                                 (chars, _) =>
                                 {
                                     for (int i = 0; i < chars.Length; i++) {
                                         if (i < leftPadIndex) {
                                             chars[i] = pad[i % pad.Length];
                                         } else if (i >= rightPadIndex) {
                                             chars[i] = pad[(i - rightPadIndex) % pad.Length];
                                         } else {
                                             chars[i] = value[i - leftPadIndex];
                                         }
                                     }
                                 });
        }

        /// <summary>Removes the specified values from the string in a case-sensitive manner.</summary>
        /// <param name="search">The values to remove from the string.</param>
        /// <returns>The resulting string with the specified values removed.</returns>
        public string Remove(params string[] search)
        {
            if (string.IsNullOrWhiteSpace(value)) {
                return value;
            }

            StringBuilder sb = new(value);

            foreach (string needle in search) {
                if (string.IsNullOrEmpty(needle)) {
                    continue;
                }

                sb.Replace(needle, string.Empty);
            }

            return sb.ToString();
        }

        /// <summary>Removes the specified values from the string in a case-insensitive manner.</summary>
        /// <param name="search">The values to remove from the string.</param>
        /// <returns>The resulting string with the specified values removed.</returns>
        public string RemoveIgnoreCase(params string[] search)
        {
            if (string.IsNullOrWhiteSpace(value)) {
                return value;
            }

            StringBuilder sb = new(value);

            foreach (string needle in search) {
                if (string.IsNullOrEmpty(needle)) {
                    continue;
                }

                int index;

                while ((index = sb.ToString().IndexOf(needle, StringComparison.OrdinalIgnoreCase)) != -1) {
                    sb.Remove(index, needle.Length);
                }
            }

            return sb.ToString();
        }

        /// <summary>Replaces a given value in the string sequentially using an array of replacement values.</summary>
        /// <param name="search">The value to search for.</param>
        /// <param name="replacements">The array of replacement values to apply sequentially.</param>
        /// <param name="caseSensitive">true to perform a case-sensitive comparison; otherwise, false.</param>
        /// <returns>The resulting string with the specified replacements made.</returns>
        public string Replace(string search, string[] replacements, bool caseSensitive = false)
        {
            if (string.IsNullOrWhiteSpace(search)) {
                return value;
            }

            StringBuilder    sb = new(value);
            int              index;
            int              repIndex   = 0;
            StringComparison comparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            while ((index = sb.ToString().IndexOf(search, comparison)) != -1) {
                sb.Remove(index, search.Length);
                sb.Insert(index, replacements[repIndex++ % replacements.Length]);
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Replaces the first occurrence of a specified string with another string, with an option to control case
        ///     sensitivity.
        /// </summary>
        /// <param name="search">The string to search for.</param>
        /// <param name="replacement">The string to replace the first occurrence with.</param>
        /// <param name="caseSensitive">true to perform a case-sensitive comparison; otherwise, false.</param>
        /// <returns>The resulting string with the first occurrence replaced.</returns>
        public string ReplaceFirst(string search, string replacement, bool caseSensitive = false)
        {
            if (string.IsNullOrWhiteSpace(search)) {
                return value;
            }

            int pos = value.IndexOf(search, caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);

            if (pos < 0) {
                return value;
            }

            ReadOnlySpan<char> source = value.AsSpan();

            return string.Concat(source[..pos], replacement, source[(pos + search.Length)..]);
        }

        /// <summary>
        ///     Replaces the last occurrence of a specified string with another string, with an option to control case
        ///     sensitivity.
        /// </summary>
        /// <param name="search">The string to search for.</param>
        /// <param name="replacement">The string to replace the first occurrence with.</param>
        /// <param name="caseSensitive">true to perform a case-sensitive comparison; otherwise, false.</param>
        /// <returns>The resulting string with the last occurrence replaced.</returns>
        public string ReplaceLast(string search, string replacement, bool caseSensitive = false)
        {
            if (string.IsNullOrEmpty(search)) {
                return value;
            }

            int pos = value.LastIndexOf(search, caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);

            if (pos < 0) {
                return value;
            }

            ReadOnlySpan<char> source = value.AsSpan();

            return string.Concat(source[..pos], replacement, source[(pos + search.Length)..]);
        }

        /// <summary>Replaces all portions of a string matching a regular expression pattern with the specified replacement string.</summary>
        /// <param name="pattern">The regular expression pattern to match against.</param>
        /// <param name="replacement">The string to replace each matched portion with.</param>
        /// <returns>The resulting string with the pattern matches replaced.</returns>
        public string ReplaceMatches(string pattern, string replacement)
        {
            return Regex.Replace(value, pattern, replacement);
        }

        /// <summary>Reverses the given string.</summary>
        /// <returns>The resulting string with its characters in reverse order.</returns>
        public string Reverse()
        {
            if (string.IsNullOrEmpty(value)) {
                return value;
            }

            return string.Create(value.Length,
                                 value,
                                 (chars, state) =>
                                 {
                                     state.AsSpan().CopyTo(chars);
                                     chars.Reverse();
                                 });
        }

        /// <summary>Removes all extraneous white space from the string, including extra white space between words.</summary>
        /// <returns>The resulting string with all extraneous white space removed.</returns>
        public string Squish()
        {
            return string.IsNullOrWhiteSpace(value) ? value.Trim() : ModificationExtensions.SquishRegex().Replace(value.Trim(), " ");
        }

        /// <summary>Begins the string with a single instance of the specified value if it does not already start with it.</summary>
        /// <param name="start">The value to prepend to the string.</param>
        /// <returns>The resulting string starting with the specified value.</returns>
        public string StartWith(string start)
        {
            if (string.IsNullOrWhiteSpace(start) || value.StartsWith(start)) {
                return value;
            }

            return start + value;
        }

        /// <summary>
        ///     Replaces text within a portion of a string, starting at the specified position and replacing the number of
        ///     characters specified by the length. Passing 0 for the length inserts the string without replacing any existing
        ///     characters.
        /// </summary>
        /// <param name="replacement">The string to insert or use as a replacement.</param>
        /// <param name="start">The zero-based starting position in the string.</param>
        /// <param name="length">
        ///     The number of characters to replace. Passing 0 inserts the string without replacing existing
        ///     characters.
        /// </param>
        /// <returns>The resulting string with the replacement or insertion made.</returns>
        public string SubstringReplace(string replacement, int start, int length = -1)
        {
            if (start < 0 || start >= value.Length) {
                return value;
            }

            int lastIndex = length < 0 ? value.Length : start + length;

            ReadOnlySpan<char> source = value.AsSpan();

            return lastIndex >= value.Length ? string.Concat(source[..start], replacement) : string.Concat(source[..start], replacement, source[lastIndex..]);
        }

        /// <summary>Replaces multiple values in the string.</summary>
        /// <param name="replacements">
        ///     A dictionary containing the search values as keys and their corresponding replacement values
        ///     as values.
        /// </param>
        /// <returns>The resulting string with all specified replacements made.</returns>
        public string Swap(Dictionary<string, string> replacements)
        {
            if (string.IsNullOrEmpty(value) || replacements.Count == 0) {
                return value;
            }

            return replacements.Aggregate(value, (current, pair) => current.Replace(pair.Key, pair.Value));
        }

        /// <summary>Converts the given string to its Base64-encoded representation.</summary>
        /// <returns>The resulting Base64-encoded string.</returns>
        public string ToBase64()
        {
            return string.IsNullOrEmpty(value) ? value : Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        }

        /// <summary>Converts the given string to camelCase.</summary>
        /// <returns>The string converted to camelCase.</returns>
        public string ToCamelCase()
        {
            return value.ToPascalCase().LcFirst();
        }

        /// <summary>Converts the string to kebab-case.</summary>
        /// <returns>The resulting string in kebab-case.</returns>
        public string ToKebabCase()
        {
            return value.ToSnakeCase().Replace("_", "-");
        }

        /// <summary>Converts the given string to PascalCase.</summary>
        /// <returns>The resulting string in PascalCase format.</returns>
        public string ToPascalCase()
        {
            if (string.IsNullOrEmpty(value)) {
                return value;
            }

            string[] words = value.ToSnakeCase().Split('_', StringSplitOptions.RemoveEmptyEntries);

            StringBuilder sb = new(value.Length);

            foreach (string word in words) {
                sb.Append(word.UcFirst());
            }

            return sb.ToString();
        }

        /// <summary>Converts the given string to snake_case.</summary>
        /// <returns>The resulting string in snake_case format.</returns>
        public string ToSnakeCase()
        {
            if (string.IsNullOrEmpty(value)) {
                return value;
            }

            value = value.Deduplicate();

            // Крок 1: Обробляємо переходи "маленька-Велика" (fooBar -> foo_Bar)
            string result = ModificationExtensions.SnakeCaseStep1().Replace(value, "$1_$2");

            // Крок 2: Обробляємо абревіатури перед словом (FOOBar -> FOO_Bar)
            result = ModificationExtensions.SnakeCaseStep2().Replace(result, "$1_$2");

            // Крок 3: Все в нижній регістр і заміна дефісів/пробілів, якщо вони були
            return result.ToLowerInvariant().Replace('-', '_').Replace(' ', '_');
        }

        /// <summary>Capitalizes the first character of the given string.</summary>
        /// <returns>The resulting string with its first character capitalized.</returns>
        public string UcFirst()
        {
            if (string.IsNullOrEmpty(value) || char.IsUpper(value[0])) {
                return value;
            }

            return char.ToUpper(value[0]) + value[1..];
        }

        /// <summary>Converts the first character of each word in the given string to uppercase.</summary>
        /// <returns>The resulting string with the first character of each word capitalized.</returns>
        public string UcWord()
        {
            if (string.IsNullOrEmpty(value)) {
                return value;
            }

            string[] words = value.Split([' ', '\n', '\t', '\r'], StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++) {
                words[i] = words[i].UcFirst();
            }

            return string.Join(" ", words);
        }
    }
}
