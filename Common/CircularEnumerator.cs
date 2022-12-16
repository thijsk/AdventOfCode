using System.Collections;
using System.Collections.Generic;

namespace Common
{
    public static partial class IEnumerableExtensions
    {
        private class CircularEnumerable<T> : IEnumerable<T>
        {
            private readonly IEnumerable<T> _wrappedEnumerable;

            public CircularEnumerable(IEnumerable<T> wrappedEnumerable)
            {
                _wrappedEnumerable = wrappedEnumerable;
            }

            public IEnumerator<T> GetEnumerator()
            {
                return new CircularEnumerator<T>(_wrappedEnumerable.GetEnumerator());
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new CircularEnumerator<T>(_wrappedEnumerable.GetEnumerator());
            }
        }

        private class CircularEnumerator<T> : IEnumerator<T>
        {
            private readonly IEnumerator<T> _wrappedEnumerator;

            public CircularEnumerator(IEnumerator<T> wrappedEnumerator)
            {
                this._wrappedEnumerator = wrappedEnumerator;
            }

            object IEnumerator.Current => _wrappedEnumerator.Current;

            T IEnumerator<T>.Current => (T)_wrappedEnumerator.Current;

            public void Dispose()
            {
                _wrappedEnumerator.Dispose();
            }

            public bool MoveNext()
            {
                if (!_wrappedEnumerator.MoveNext())
                {
                    _wrappedEnumerator.Reset();
                    return _wrappedEnumerator.MoveNext();
                }
                return true;
            }

            public void Reset()
            {
                _wrappedEnumerator.Reset();
            }
        }
    }
}

