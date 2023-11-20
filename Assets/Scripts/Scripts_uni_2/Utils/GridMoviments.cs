using System;
using System.Collections.Generic;
using UnityEngine;

//mudar node da classe para PathUtils
public class GridMoviments_v1 {
    public List<Tuple<int, int>> OrderPointsByDistance(List<Tuple<int, int>> points) {
        return points;
    }

    public GameObject FindObjectByPoint(GameObject[] objectsCheese, Tuple<int, int> point) {
        return objectsCheese[0];

    }

    public Tuple<int, int> FindObjcetPos(GameObject gameObject) {
        Vector3 position = gameObject.transform.position;
        return new Tuple<int, int>((int)position.x, (int)position.y);
    }

    public Node getNextNode(List<Node> nodes) {
        int index = 0;
        if(nodes.Count > 1){
            index = 1;
        }
        return nodes[index];

    }

    public List<Node> GeneratePathByPoints(Tuple<int, int> currPoint, Tuple<int, int> targetPoint, Dictionary<(int, int), List<((int, int), double)>> graph) {
        A_Star a_Star = new A_Star();
        Node currNode = new Node(currPoint.Item1, currPoint.Item2);
        Node targetNode = new Node(targetPoint.Item1, targetPoint.Item2);
        return a_Star.FindPath(graph, currNode, targetNode);

    }

}