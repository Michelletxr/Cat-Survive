using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class A_Star {
    List<Node> OPEN = new List<Node>();
    List<Node> CLOSED = new List<Node>();
    Node star;
    Node goal;

    public int calculate_heuristic(Node node, Node goal) {
        int dx = node.x - goal.x;
        int dy = node.y - goal.y;
        return (int)Math.Sqrt(dx * dx + dy * dy);
    }

    public double calculate_f(Node node) {
        return node.g_dist + node.h_dist;
    }

    public Node FindNodeLowerF(List<Node> list) {
        return list.OrderBy(node => node.f_dist).First();
    }

    public List<Node> FindPath(Dictionary<(int, int), List<((int, int), double)>> graph, Node start, Node goal) {

        start.g_dist = 0;
        start.h_dist = calculate_heuristic(start, goal);
        start.f_dist = calculate_f(start);
        List<Node> path = new List<Node>();

        
        OPEN.Add(start);

        //enquanto existir nós abertos executa
        while(OPEN.Count != 0) {
            
            //seleciona o nó com menor dist_f e remove da lista de nós abertos
            Node currentNode = FindNodeLowerF(OPEN);
            OPEN.Remove(currentNode);

            ///se o nó atual for o nó alvo
            if (new NodeComparer().Compare(currentNode, goal) == 0 ) {
                path = new List<Node>();
                while (currentNode != null){
                    path.Add(currentNode);
                    currentNode = currentNode.parent;
                }
                path.Reverse();
                return path;
            }

            //fecha o nó
            CLOSED.Add(currentNode);
        
            //atualiza os vizinhos do nó atual
            var keyNode = (currentNode.x, currentNode.y);
            foreach (var (neighborValue, cost) in graph[keyNode]) {

                Node neighbor = new Node(neighborValue.Item1, neighborValue.Item2);

                if (!CLOSED.Contains(neighbor)) {

                    //Debug.Log("ADICIONANDO NO FECHADO");
                    //Debug.Log(neighbor);

                    neighbor.parent = currentNode;
                    neighbor.g_dist = currentNode.g_dist + cost;
                    neighbor.h_dist = calculate_heuristic(neighbor, goal);
                    neighbor.f_dist = calculate_f(neighbor); 
                    OPEN.Add(neighbor);
                }
            }
        }

        return path; 
    }

}
