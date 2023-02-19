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
            // this code is raising an exception about reaching stack limit
            throw new Exception("Stack overflowed");
        }

        _array[_pointer] = value;
        _pointer++;
    }

    public T Pop()
    {
        var value = _array[_pointer--];
        _pointer--;
        // _array = _array.Where(e => e != value).ToArray();
        return value;
    }
    
    public int Count
    {
        get { return _array.Length; }
    }
    
    public bool Contains(T value)
    {
        return _array.Contains(value);
    }

    /* public T Clear()
    {
        return _array.Clear();
    } */
}