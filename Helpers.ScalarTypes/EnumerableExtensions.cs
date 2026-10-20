using System.Collections;

namespace Zhomfr.Helpers.ScalarTypes;

/// <summary>Extensions method for IEnumerable.</summary>
public static class EnumerableExtensions
{
    extension(IEnumerable value)
    {
        /// <summary>Determines whether the number of elements in the collection is greater than the specified minimum value.</summary>
        /// <param name="min">The threshold value to compare against (exclusive).</param>
        /// <returns><c>true</c> if the count of elements is greater than <paramref name="min"/>; otherwise, <c>false</c>.</returns>
        public bool CountIsGreatThan(int min)
        {
            IEnumerator  enumerator = value.GetEnumerator();
            IDisposable? disposable = enumerator as IDisposable;

            int count = 0;

            while (enumerator.MoveNext()) {
                count++;

                if (count > min) {
                    disposable?.Dispose();

                    return true;
                }
            }

            disposable?.Dispose();

            return false;
        }

        /// <summary>
        ///     Determines whether the number of elements in the collection is greater than or equal to the specified minimum
        ///     value.
        /// </summary>
        /// <param name="min">The minimum threshold value (inclusive).</param>
        /// <returns>
        ///     <c>true</c> if the count of elements is greater than or equal to <paramref name="min"/>; otherwise,
        ///     <c>false</c>.
        /// </returns>
        public bool CountIsGreatThanOrEqual(int min)
        {
            IEnumerator  enumerator = value.GetEnumerator();
            IDisposable? disposable = enumerator as IDisposable;

            int count = 0;

            while (enumerator.MoveNext()) {
                count++;

                if (count >= min) {
                    disposable?.Dispose();

                    return true;
                }
            }

            disposable?.Dispose();

            return false;
        }

        /// <summary>Determines whether the number of elements in the collection is less than the specified maximum value.</summary>
        /// <param name="max">The threshold value to compare against (exclusive).</param>
        /// <returns><c>true</c> if the count of elements is less than <paramref name="max"/>; otherwise, <c>false</c>.</returns>
        public bool CountIsLessThan(int max)
        {
            IEnumerator  enumerator = value.GetEnumerator();
            IDisposable? disposable = enumerator as IDisposable;

            int count = 0;

            while (enumerator.MoveNext()) {
                count++;

                if (count >= max) {
                    disposable?.Dispose();

                    return false;
                }
            }

            disposable?.Dispose();

            return true;
        }

        /// <summary>
        ///     Determines whether the number of elements in the collection is less than or equal to the specified maximum
        ///     value.
        /// </summary>
        /// <param name="max">The maximum threshold value (inclusive).</param>
        /// <returns><c>true</c> if the count of elements is less than or equal to <paramref name="max"/>; otherwise, <c>false</c>.</returns>
        public bool CountIsLessThanOrEqual(int max)
        {
            IEnumerator  enumerator = value.GetEnumerator();
            IDisposable? disposable = enumerator as IDisposable;

            int count = 0;

            while (enumerator.MoveNext()) {
                count++;

                if (count > max) {
                    disposable?.Dispose();

                    return false;
                }
            }

            disposable?.Dispose();

            return true;
        }

        /// <summary>Determines whether the number of elements in the collection is within the specified range (inclusive).</summary>
        /// <param name="min">The lower bound of the range (inclusive).</param>
        /// <param name="max">The upper bound of the range (inclusive).</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when <paramref name="min"/> is greater than <paramref name="max"/>
        ///     .
        /// </exception>
        /// <returns>
        ///     <c>true</c> if the count of elements is between <paramref name="min"/> and <paramref name="max"/>; otherwise,
        ///     <c>false</c>.
        /// </returns>
        public bool CountIsBetween(int min, int max)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(min, max);

            IEnumerator  enumerator = value.GetEnumerator();
            IDisposable? disposable = enumerator as IDisposable;

            int count = 0;

            while (enumerator.MoveNext()) {
                count++;

                if (count > max) {
                    disposable?.Dispose();

                    return false;
                }
            }

            disposable?.Dispose();

            return count >= min;
        }
    }
}
