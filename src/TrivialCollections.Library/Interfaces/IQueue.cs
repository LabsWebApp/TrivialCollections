namespace TrivialCollections.Library.Interfaces;

public interface IQueue<T>
{
    void Enqueue(T? item);
    T? Dequeue();
    T? Peek();
}