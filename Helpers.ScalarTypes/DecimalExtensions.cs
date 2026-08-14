namespace Zhomfr.Helpers.ScalarTypes;

public static class DecimalExtensions
{
    extension(decimal number)
    {
        /// <summary>Calculates the number of digits to the left of the decimal separator.</summary>
        /// <returns>The count of integer digits. Returns 1 for values between -1 and 1.</returns>
        public int GetDigitsBeforeSeparator()
        {
            decimal absoluteValue = decimal.Abs(number);

            if (absoluteValue < decimal.One) {
                return 1;
            }

            // Uses Log10 to determine the magnitude of the integer part.
            return (int)Math.Floor(Math.Log10(Convert.ToDouble(absoluteValue))) + 1;
        }

        /// <summary>Calculates the total number of digits (integer part + fractional part).</summary>
        /// <returns>The total count of significant digits.</returns>
        public int GetDigits()
        {
            return number.GetDigitsBeforeSeparator() + number.GetDecimal();
        }

        /// <summary>Extracts the number of decimal places from the numeric value.</summary>
        /// <remarks>Converts the value to a decimal and inspects its internal scaling factor.</remarks>
        /// <returns>The number of digits after the decimal point.</returns>
        public int GetDecimal()
        {
            int[] bits = decimal.GetBits(number);

            // The scale factor is stored in bits 16-23 of the flags element (index 3).
            return bits[3] >> 16 & 0x7F;
        }
    }
}
