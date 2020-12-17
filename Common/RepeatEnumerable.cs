using System.Collections;
using System.Collections.Generic;

namespace Common
{
    internal class RepeatEnumerable<T> : IEnumerable<T>
    {
        private IEnumerable<T> _wrappedEnumerable;
        private int _times;

        public RepeatEnumerable(IEnumerable<T> t, int times)
        {
            this._wrappedEnumerable = t;
            this._times = times;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new RepeatEnumerator<T>(_wrappedEnumerable.GetEnumerator(), _times);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new RepeatEnumerator<T>(_wrappedEnumerable.GetEnumerator(), _times);
        }
    }

    internal class RepeatEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _wrappedEnumerator;
        private int _times;
        private int _iteration;

        public RepeatEnumerator(IEnumerator<T> wrappedEnumerator, int times)
        {
            _wrappedEnumerator = wrappedEnumerator;
            _times = times;
            _iteration = 0;
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
                _iteration++;
                if (_iteration < _times)
                {
                    return _wrappedEnumerator.MoveNext();
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public void Reset()
        {
            _wrappedEnumerator.Reset();
            _iteration = 0;
        }
    }
}