namespace TrivialCollections.Library.Graphs;

/// <summary>
/// Ребро графа
/// </summary>
public class GraphEdge<TName, TWeight>
    where TName : notnull
{
    /// <summary>
    /// Связанная вершина
    /// </summary>
    public GraphVertex<TName, TWeight> ConnectedVertex { get; }

    /// <summary>
    /// Вес ребра
    /// </summary>
    public TWeight EdgeWeight { get; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="connectedVertex">Связанная вершина</param>
    /// <param name="weight">Вес ребра</param>
    public GraphEdge(GraphVertex<TName, TWeight> connectedVertex, TWeight weight)
    {
        ConnectedVertex = connectedVertex;
        EdgeWeight = weight;
    }
}