using TrivialCollections.Library.Interfaces;

namespace TrivialCollections.Library.SingleLinked;

/// <summary>
/// Представляет структуру-коллекцию, обслуживаемую по принципу "последним пришел - первым вышел" (LIFO).
/// </summary>
/// <typeparam name="T"></typeparam>
public struct TrivialStructStack<T> : IStack<T>
{
    /// <summary>
    /// Последний элемент в стеке.
    /// </summary>
    private LinkedNode<T>? _last;
    public bool IsEmpty => _last is null;

    public void Push(T? item) => _last = new LinkedNode<T>(item) { Link = _last };

    public T? Pop()
    {
        if (_last is null) throw new InvalidOperationException("Стек является пустым.");

        var res = _last!.Data;
        _last = _last.Link;
        return res;
    }

    public T? Peek() => _last is null ? throw new InvalidOperationException("Стек является пустым.") : _last!.Data;
}