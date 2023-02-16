using TrivialCollections.Library.Interfaces;

namespace TrivialCollections.Library;

public class TrivialQueue<T> : IQueue<T>
{
    private LinkedNode<T>? _first;

    public void Enqueue(T? item)
    {
        var newElement = new LinkedNode<T>(item);

        if (_first is null)
        {
            _first = newElement;
            return;
        }

        var element = _first;
        while (element.Link is not null) element = element.Link;
        element.Link = newElement;
    }

    public T? Dequeue()
    {
        if (_first is null) throw new InvalidOperationException("Очередь пуста");

        var result = _first.Data;
        _first = _first.Link;
        return result;
    }

    public T? Peek() => 
        _first is null ? throw new InvalidOperationException("Очередь пуста") : _first.Data;
}