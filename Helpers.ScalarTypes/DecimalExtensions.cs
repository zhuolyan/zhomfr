namespace Zhomfr.Helpers.ScalarTypes;

public static class DecimalExtensions
{
    extension(decimal number)
    {
        /// <summary>Gets the number of digits in the integer part of the decimal value.</summary>
        /// <returns>The number of digits before the decimal separator. Returns 1 for values between -1 and 1.</returns>
        public int GetDigitsBeforeSeparator()
        {
            decimal absoluteValue = decimal.Abs(number);

            if (absoluteValue < decimal.One) {
                return 1;
            }

            // Uses Log10 to determine the magnitude of the integer part.
            return (int)Math.Floor(Math.Log10(Convert.ToDouble(absoluteValue))) + 1;
        }

        /// <summary>Gets the total number of digits before and after the decimal separator.</summary>
        /// <returns>The total number of digits represented by the decimal value.</returns>
        public int GetDigits()
        {
            return number.GetDigitsBeforeSeparator() + number.GetDecimal();
        }

        /// <summary>Gets the number of digits after the decimal separator.</summary>
        /// <remarks>The number of decimal places is determined from the internal scale stored in the <see cref="decimal"/> value.</remarks>
        /// <returns>The number of digits after the decimal separator.</returns>
        public int GetDecimal()
        {
            int[] bits = decimal.GetBits(number);

            // The scale factor is stored in bits 16-23 of the flags element (index 3).
            return bits[3] >> 16 & 0x7F;
        }
    }
}
