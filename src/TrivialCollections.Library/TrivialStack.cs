using TrivialCollections.Library.Interfaces;

namespace TrivialCollections.Library;

public class TrivialStack<T> : IStack<T>
{
    private LinkedNode<T>? _last;

    public void Push(T? item) => _last = new LinkedNode<T>(item) { Link = _last }; 

    public T? Pop()
    {
        if (_last is null) throw new InvalidOperationException("Стек пуст");

        var res = _last.Data;
        _last = _last.Link;
        return res;
    }

    public T? Peek() => 
        _last is null ? throw new InvalidOperationException("Стек пуст") : _last.Data;
}