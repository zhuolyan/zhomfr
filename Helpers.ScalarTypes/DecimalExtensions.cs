namespace Zhomfr.Helpers.ScalarTypes;

/// <summary>Extensions method for decimal.</summary>
public static class DecimalExtensions
{
    extension(decimal value)
    {
        /// <summary>Gets the number of digits in the integer part of the decimal value.</summary>
        /// <returns>The number of digits before the decimal separator. Returns 1 for values between -1 and 1.</returns>
        /// <example>
        ///     <code>
        ///         decimal value = 123.45m;
        ///         int result = value.GetDigitsBeforeSeparator(); // 3
        ///     </code>
        /// </example>
        public int GetDigitsBeforeSeparator()
        {
            decimal absoluteValue = decimal.Abs(value);

            if (absoluteValue < decimal.One) {
                return 1;
            }

            // Uses Log10 to determine the magnitude of the integer part.
            return (int)Math.Floor(Math.Log10(Convert.ToDouble(absoluteValue))) + 1;
        }

        /// <summary>Gets the total number of digits before and after the decimal separator.</summary>
        /// <returns>The total number of digits represented by the decimal value.</returns>
        /// <example>
        ///     <code>
        ///         decimal value = 123.45m;
        ///         int result = value.GetDigits(); // 5
        ///     </code>
        /// </example>
        public int GetDigits()
        {
            return value.GetDigitsBeforeSeparator() + value.GetDecimal();
        }

        /// <summary>Gets the number of digits after the decimal separator.</summary>
        /// <remarks>The number of decimal places is determined from the internal scale stored in the <see cref="decimal"/> value.</remarks>
        /// <returns>The number of digits after the decimal separator.</returns>
        /// <example>
        ///     <code>
        ///         decimal value = 123.45m;
        ///         int result = value.GetDigits(); // 5
        ///     </code>
        /// </example>
        public int GetDecimal()
        {
            int[] bits = decimal.GetBits(value);

            // The scale factor is stored in bits 16-23 of the flags element (index 3).
            return bits[3] >> 16 & 0x7F;
        }
    }
}
