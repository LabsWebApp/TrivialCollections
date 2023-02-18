namespace TrivialCollections.Library.Interfaces;

/// <summary>
/// Добавляет в коллекцию проверку на наличие хотя бы одного объекта
/// </summary>
public interface IEmpty
{
    /// <summary>
    /// Возвращает значение, указывающее - является ли коллекция пустой.
    /// </summary>
    bool IsEmpty { get; }
}