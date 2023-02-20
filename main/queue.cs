using System.Collections;

namespace main
{
    public class Queue<T> : IEnumerable<T>
    {
        private const int Capacity = 50;
        private T[] _array;
        private int _pointer;

        public Queue()
        { 
            _array = new T[Capacity];
            _pointer = 0;
        }

        public int Count => _pointer;

        public void Enqueue(T value)
        {
            if (_pointer == _array.Length)
            {
                Array.Resize(ref _array, _array.Length * 2);
            }

            _array[_pointer++] = value;
        }

        public T Dequeue()
        {
            if (_pointer == 0)
            {
                throw new InvalidOperationException("Queue is empty.");
            }

            T value = _array[--_pointer];
            return value;
        }

        public void Clear()
        {
            Array.Clear(_array);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _pointer; i++)
            {
                yield return _array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


    }
}
