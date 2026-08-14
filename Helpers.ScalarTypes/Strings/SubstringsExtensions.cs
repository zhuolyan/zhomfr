using System.Text.RegularExpressions;

namespace Zhomfr.Helpers.ScalarTypes.Strings;

public static partial class SubstringsExtensions
{
    [GeneratedRegex("(?=[A-Z])")]
    private static partial Regex UcSplitRegex();

    extension(string value)
    {
        /// <summary>Returns everything after the given value in a string, or the entire string if the value is not found.</summary>
        /// <param name="search">The value to search for within the string.</param>
        /// <returns>The substring after the specified value, or the entire string if the value is not found.</returns>
        public string After(string search)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(search)) {
                return value;
            }

            int index = value.IndexOf(search, StringComparison.Ordinal);

            if (index < 0) {
                return value;
            }

            index += search.Length;

            return index >= value.Length ? string.Empty : value[index..];
        }

        /// <summary>
        ///     Returns everything after the last occurrence of the given value in a string, or the entire string if the value
        ///     is not found.
        /// </summary>
        /// <param name="search">The value to search for within the string.</param>
        /// <returns>
        ///     The substring after the last occurrence of the specified value, or the entire string if the value is not
        ///     found.
        /// </returns>
        public string AfterLast(string search)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(search)) {
                return value;
            }

            int index     = 0;
            int lastIndex = 0;

            while ((index = value.IndexOf(search, index, StringComparison.Ordinal)) != -1) {
                index     += search.Length;
                lastIndex =  index;
            }

            if (lastIndex >= value.Length) {
                return string.Empty;
            }

            return lastIndex > 0 ? value[lastIndex..] : value;
        }

        /// <summary>Returns everything before the given value in a string.</summary>
        /// <param name="search">The value to search for within the string.</param>
        /// <returns>The substring before the specified value, or the entire string if the value is not found.</returns>
        public string Before(string search)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(search)) {
                return value;
            }

            int index = value.IndexOf(search, StringComparison.Ordinal);

            return index switch
                   {
                       < 0   => value,
                       0     => string.Empty,
                       var _ => value[..index],
                   };
        }

        /// <summary>Returns everything before the last occurrence of the given value in a string.</summary>
        /// <param name="search">The value to search for within the string.</param>
        /// <returns>
        ///     The substring before the last occurrence of the specified value, or the entire string if the value is not
        ///     found.
        /// </returns>
        public string BeforeLast(string search)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(search)) {
                return value;
            }

            int index     = 0;
            int lastIndex = -1;

            while ((index = value.IndexOf(search, index, StringComparison.Ordinal)) != -1) {
                lastIndex =  index;
                index     += search.Length;
            }

            return lastIndex < 0 ? value : value[..lastIndex];
        }

        /// <summary>Returns the portion of a string between two specified values.</summary>
        /// <param name="after">The value after which to start extracting.</param>
        /// <param name="before">The value before which to stop extracting.</param>
        /// <returns>The substring between the specified values.</returns>
        public string Between(string after, string before)
        {
            return value.After(after).BeforeLast(before);
        }

        /// <summary>Returns the smallest possible portion of a string between two specified values.</summary>
        /// <param name="after">The value after which to start extracting.</param>
        /// <param name="before">The value before which to stop extracting.</param>
        /// <returns>The smallest substring between the specified values.</returns>
        public string BetweenFirst(string after, string before)
        {
            return value.After(after).Before(before);
        }

        /// <summary>Returns the last smallest possible portion of a string between two specified values.</summary>
        /// <param name="after">The value after which to start extracting.</param>
        /// <param name="before">The value before which to stop extracting.</param>
        /// <returns>The smallest substring between the specified values.</returns>
        public string BetweenLast(string after, string before)
        {
            return value.AfterLast(after).BeforeLast(before);
        }

        /// <summary>
        ///     Truncates the string to the specified length, optionally appending a custom string to the end of the truncated
        ///     result. Can preserve complete words at the nearest word boundary when specified.
        /// </summary>
        /// <param name="limit">The maximum length of the truncated string.</param>
        /// <param name="end">The optional string to append to the end of the truncated string.</param>
        /// <param name="preserveWords">Specifies whether to truncate at the nearest complete word boundary.</param>
        /// <returns>The resulting truncated string.</returns>
        public string Limit(int limit, string end = " ...", bool preserveWords = false)
        {
            if (string.IsNullOrEmpty(value) || value.Length <= limit) {
                return value;
            }

            value = value[..(limit - end.Length)];

            if (preserveWords) {
                value = value.BeforeLast(" ");
            }

            return value + end;
        }

        /// <summary>Splits the string into a list by uppercase characters.</summary>
        /// <returns>A list of strings resulting from the split.</returns>
        public List<string> UcSplit()
        {
            return string.IsNullOrEmpty(value) ? [] : [.. SubstringsExtensions.UcSplitRegex().Split(value).Where(s => !string.IsNullOrEmpty(s))];
        }
    }
}
