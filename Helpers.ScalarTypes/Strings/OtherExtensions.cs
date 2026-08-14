using System.Text;

namespace Zhomfr.Helpers.ScalarTypes.Strings;

public static class OtherExtensions
{
    extension(string value)
    {
        /// <summary>Returns the character at the specified index, or null if the index is out of bounds.</summary>
        /// <param name="index">The zero-based index of the character to return.</param>
        /// <returns>The character at the specified index, or null if the index is out of range.</returns>
        public char? CharAt(int index)
        {
            return index < 0 || index >= value.Length ? null : value[index];
        }

        /// <summary>Returns the initials of the string, optionally capitalizing them.</summary>
        /// <param name="capitalize">Specifies whether to capitalize the initials (defaults to true).</param>
        /// <returns>The initials extracted from the string.</returns>
        public string Initials(bool capitalize = true)
        {
            string[]      words = value.Split([' ', '\n', '\t', '\r'], StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb    = new(words.Length);

            foreach (string word in words) {
                char initial = word[0];
                sb.Append(capitalize ? char.ToUpper(initial) : initial);
            }

            return sb.ToString();
        }

        /// <summary>Returns the number of occurrences of a given value in the string.</summary>
        /// <param name="substring">The value to locate within the string.</param>
        /// <returns>The number of times the specified value occurs in the string.</returns>
        public int SubstringCount(string substring)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(substring)) {
                return 0;
            }

            int count = 0;
            int index = 0;

            while ((index = value.IndexOf(substring, index, StringComparison.Ordinal)) != -1) {
                count++;
                index += substring.Length;
            }

            return count;
        }

        /// <summary>Returns the number of words that the string contains.</summary>
        /// <returns>The number of words in the string.</returns>
        public int WordCount()
        {
            if (string.IsNullOrEmpty(value)) {
                return 0;
            }

            int  count  = 0;
            bool inWord = false;

            foreach (char c in value) {
                if (char.IsWhiteSpace(c)) {
                    inWord = false;
                } else if (!inWord) {
                    inWord = true;
                    count++;
                }
            }

            return count;
        }
    }
}
