namespace TrivialCollections.Library.Interfaces;

/// <summary>
/// Необходимое поведение для Очереди.
/// </summary>
/// <typeparam name="T">Тип данных, хранимых в очереди.</typeparam>
public interface IQueue<T> : IEmpty
{
    /// <summary>
    /// Добавляет объект в конец очереди.
    /// </summary>
    /// <param name="item">Объект, добавляемый в коллекцию.</param>
    void Enqueue(T? item);

    /// <summary>
    /// Удаляет объект из начала очереди и возвращает его. 
    /// </summary>
    /// <returns>Объект, удаляемый из начала очереди.</returns>
    /// <exception cref="T:System.InvalidOperationException">Очередь является пустой.</exception>
    T? Dequeue();

    /// <summary>
    /// Возвращает объект, находящийся в начале очереди.
    /// </summary>
    /// <returns>Объект, находящийся в начале очереди.</returns>
    /// <exception cref="T:System.InvalidOperationException">Очередь является пустой.</exception>
    T? Peek();
}