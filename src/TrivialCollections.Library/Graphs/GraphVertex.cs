namespace TrivialCollections.Library.Graphs;

/// <summary>
/// Вершина графа с уникальным именем 
/// </summary>
public class GraphVertex<TName,TWeight> where TName : notnull
{
    /// <summary>
    /// Уникальное название вершины
    /// </summary>
    public TName Name { get; }

    /// <summary>
    /// Список ребер
    /// </summary>
    public List<GraphEdge<TName, TWeight>> Edges { get; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="vertexName">Название вершины</param>
    public GraphVertex(TName vertexName)
    {
        Name = vertexName;
        Edges = new List<GraphEdge<TName, TWeight>>();
    }

    /// <summary>
    /// Добавить ребро
    /// </summary>
    /// <param name="newEdge">Ребро</param>
    public void AddEdge(GraphEdge<TName, TWeight> newEdge) => Edges.Add(newEdge);

    /// <summary>
    /// Добавить ребро
    /// </summary>
    /// <param name="vertex">Вершина</param>
    /// <param name="edgeWeight">Вес</param>
    public void AddEdge(GraphVertex<TName, TWeight> vertex, TWeight edgeWeight) => 
        AddEdge(new GraphEdge<TName, TWeight>(vertex, edgeWeight));

    /// <summary>
    /// Преобразование в строку
    /// </summary>
    /// <returns>Имя вершины</returns>
    public override string ToString() => Name.ToString() 
                                         ?? throw new NullReferenceException(
                                             "Name не может быть null");
}