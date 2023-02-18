using TrivialCollections.Library.Interfaces;

namespace TrivialCollections.Library;

/// <summary>
/// Представляет коллекцию объектов, основанную на принципе «первым поступил — первым обслужен» (FIFO).
/// </summary>
/// <typeparam name="T">Задает тип элементов в очереди.</typeparam>
public class TrivialQueue<T> : IQueue<T>
{
    /// <summary>
    /// Первый элемент в очереди.
    /// </summary>
    private LinkedNode<T>? _first;

    public bool IsEmpty => _first is null;

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
        if (IsEmpty) throw new InvalidOperationException("Очередь пуста.");

        var result = _first!.Data;
        _first = _first.Link;
        return result;
    }

    public T? Peek() => IsEmpty ? throw new InvalidOperationException("Очередь пуста.") : _first!.Data;
}