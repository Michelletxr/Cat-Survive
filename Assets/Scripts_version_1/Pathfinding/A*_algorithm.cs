using System;
using System.Collections.Generic;
using System.Linq;

public class A_Star{
   /* List<Node> OPEN = new List<Node>();
    List<Node> CLOSED = new List<Node>();
    Node star;
    Node goal;
    public int calculate_heuristic(Node node, Node goal){
        int dx = node.x - goal.x;
        int dy = node.y - goal.y;
        return (int)Math.Sqrt(dx * dx + dy * dy);
    }

    public int calculate_f(Node node){
        return node.g_dist + node.h_dist;
    }

    public Node FindNodeLowerF(List<Node> list){
        return list.OrderBy(node => node.f_dist).First();
    }

    public List<Node> FindPath(Dictionary<Node, List<Tuple<Node, int>>> graph, Node start, Node goal){
        star.g_dist = 0;
        star.h_dist = calculate_heuristic(star, goal);
        star.f_dist = calculate_f(star);
        List<Node> path = null;
        
        OPEN.Add(star);
        //enquanto existir nós abertos executa
        while(OPEN.length != 0){
            //seleciona o nó com menor dist_f
            Node currentNode = FindNodeLowerF(OPEN);

            //se o nó atual for o nó alvo
            if (currentNode == goal){
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
            foreach (var neighborTuple in graph[currentNode]){
                Node neighbor = neighborTuple.Item1;
                int cost = neighborTuple.Item2;

                if (!OPEN.Contains(neighbor)){
                        neighbor.parent = currentNode;
                        neighbor.g_dist = currentNode.g_dist + cost;
                        neighbor.h_dist = calculate_heuristic(neighbor, goal);
                        neighbor.f_dist = calculate_f(neighbor); 
                        OPEN.Add(neighbor);
                }
            }
        }

        return path;
    } */
}


//criar labirinto