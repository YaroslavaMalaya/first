namespace main;

public class Queue<T>
{
    private const int Capacity = 50;
    private T[] _array = new T[Capacity];
    private int _pointer;

    public void Enqueue(T value)
    {
        if (_pointer == _array.Length)
        {
            throw new Exception("Stack overflowed");
        }

        _array[_pointer++] = value;
    }

    public void Clear()
    {
        Array.Clear(_array);
    }
}
