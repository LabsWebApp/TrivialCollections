namespace TrivialCollections.Library.Interfaces;

/// <summary>
/// Необходимое поведение для Стека.
/// </summary>
/// <typeparam name="T">Тип объектов, хранимых в стеке.</typeparam>
public interface IStack<T> : IEmpty
{
    /// <summary>
    /// Вставляет объект как верхний элемент стека.
    /// </summary>
    /// <param name="item">Объект, вставляемый в стек.</param>
    void Push(T? item);

    /// <summary>
    /// Удаляет и возвращает объект, находящийся в начале стека.
    /// </summary>
    /// <returns>Объект, удаляемый из начала стека.</returns>
    /// <exception cref="T:System.InvalidOperationException">Стек является пустым.</exception>
    T? Pop();

    /// <summary>
    /// Возвращает объект, находящийся в начале стека.
    /// </summary>
    /// <returns>Объект, находящийся в начале стека.</returns>
    /// <exception cref="T:System.InvalidOperationException">Стек является пустым.</exception>
    T? Peek();
}