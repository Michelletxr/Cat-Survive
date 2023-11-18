using System;
using UnityEngine;
using System.Collections.Generic;

public class GraphGenerator {

    public void AddNeighbor(Dictionary<(int, int), List<((int, int), double)>> graph){
        
        foreach (var node in graph)
        {
            var key = node.Key;
            int x = key.Item1;
            int y = key.Item2;
            var keys = new List<(int, int)> { (x, y - 1), (x, y + 1), (x - 1, y), (x + 1, y) };

            foreach (var neighborKey in keys)
            {
                if (graph.ContainsKey(neighborKey))
                {
                    graph[(x, y)].Add((neighborKey, 1.0)); // Adiciona o vizinho com um custo de 1.0 à lista correspondente à chave (x, y)
                }
            }
        }
    }

    public void PrintGraph(Dictionary<(int, int), List<((int, int), double)>> graph)
    {
        foreach (var kvp in graph)
        {
            Debug.Log($"Node ({kvp.Key.Item1}, {kvp.Key.Item2}): ");

            foreach (var neighbor in kvp.Value)
            {
                Debug.Log($" Neighbor: [{neighbor.Item1.Item1}, {neighbor.Item1.Item2}]({neighbor.Item2}) ");
            }

        }
    }
    

    public Dictionary<(int, int), List<((int, int), double)>> CreatGraph(List<Node> nodes){
        
        var graph = new Dictionary<(int, int), List<((int, int), double)>>();
        nodes.Sort(new NodeComparer());

        // Console.WriteLine("Lista de objetos Node ordenada:");
        foreach (var node in nodes)
        {
            var key = (node.x, node.y);
            if (!graph.ContainsKey(key))
            {
                graph.Add(key, new List<((int, int), double)>());
            }
        }

        AddNeighbor(graph);
        return graph;
    }
}
