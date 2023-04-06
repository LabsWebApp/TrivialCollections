namespace TrivialCollections.Library.Graphs;

/// <summary>
/// Граф
/// </summary>
public class Graph<TName, TWeight> where TName : notnull
{
    /// <summary>
    /// Список вершин графа
    /// </summary>
    public virtual IList<GraphVertex<TName, TWeight>> Vertices { get; }

    /// <summary>
    /// Конструктор
    /// </summary>
    public Graph() => Vertices = new List<GraphVertex<TName, TWeight>>();

    /// <summary>
    /// Добавление вершины
    /// </summary>
    /// <param name="vertexName">Уникальное имя вершины</param>
    public virtual bool AddVertex(TName vertexName)
    {
        if (Vertices.Any(v => v.Name.Equals(vertexName))) return false;
        Vertices.Add(new GraphVertex<TName, TWeight>(vertexName));
        return true;
    }

    /// <summary>
    /// Поиск вершины
    /// </summary>
    /// <param name="vertexName">Название вершины</param>
    /// <returns>Найденная вершина</returns>
    public GraphVertex<TName, TWeight>? FindVertex(TName vertexName) => 
        Vertices.FirstOrDefault(v => v.Name.Equals(vertexName));

    /// <summary>
    /// Добавление ребра
    /// </summary>
    /// <param name="firstName">Имя первой вершины</param>
    /// <param name="secondName">Имя второй вершины</param>
    /// <param name="weight">Вес ребра соединяющего вершины</param>
    public virtual bool AddEdge(TName firstName, TName secondName, TWeight weight)
    {
        var v1 = FindVertex(firstName);
        if(v1 == null) return false;
        var v2 = FindVertex(secondName);
        if(v2 == null) return false;

        v1.AddEdge(v2, weight);
        v2.AddEdge(v1, weight);
        return true;
    }
}