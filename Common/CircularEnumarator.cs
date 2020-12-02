using System.Collections;
using System.Collections.Generic;

namespace Common
{
    public static partial class IEnumerableExtensions
    {
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