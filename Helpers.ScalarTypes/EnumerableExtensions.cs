using System.Collections;

namespace Zhomfr.Helpers.ScalarTypes;

public static class EnumerableExtensions
{
    extension(IEnumerable value)
    {
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
