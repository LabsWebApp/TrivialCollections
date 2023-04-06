namespace TrivialCollections.Library.Graphs;

public class MapGraph : Graph<string, uint>
{
    public override bool AddVertex(string vertexName) => 
        !string.IsNullOrWhiteSpace(vertexName) && base.AddVertex(vertexName);

    //public override bool AddEdge(string firstName, string secondName, int weight) =>
    //    weight > 0 && base.AddEdge(firstName, secondName, weight);
}