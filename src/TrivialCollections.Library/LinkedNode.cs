namespace TrivialCollections.Library;

internal class LinkedNode<T>
{
    internal T? Data { get; init; }
    internal LinkedNode<T>? Link { get; set; }

    internal LinkedNode(T? data = default) => Data = data;
}