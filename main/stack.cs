namespace main;

public class Stack<T>
{
    private const int Capacity = 50;
    private T[] _array = new T[Capacity];
    private int _pointer;

    public void Push(T value)
    {
        if (_pointer == _array.Length)
        {
            throw new Exception("Stack overflowed");
        }

        _array[_pointer++] = value;
    }

    public T Pop()
    {
        return _array[--_pointer];
    }
    
    public int Count => _pointer;

    public bool Contains(T value)
    {
        return _array.Contains(value);
    }

    public void Clear()
    {
        Array.Clear(_array);
    }
}