using System;
using System.Collections.Generic;
using UnityEngine;

//mudar node da classe para PathUtils
public class GridMoviments_v1 {

    public float speed = 10f; // Velocidade de movimento
    private int currentPointIndex = 0;
    private int totalPoints = 0;

    private bool moveObject = false;

    public void MoveToPoints(GameObject objectToMove, Node node){

        if(node != null) {
            Vector2 targetPosition = new Vector2(node.x, node.y);
            float step = speed * Time.deltaTime;
            objectToMove.transform.position = Vector2.MoveTowards(objectToMove.transform.position, new Vector2(node.x, node.y), step);
        }
    }

    public List<Tuple<int, int>> OrderPointsByDistance(List<Tuple<int, int>> points){
        return points;
    }

    public GameObject FindObjectByPoint(GameObject[] objectsCheese, Tuple<int, int> point){
        return objectsCheese[0];

    }

    public Node getNextNode(List<Node> nodes){
        int index = 0;
        if(nodes.Count > 1){
            index = 1;
        }
        return nodes[index];

    }

    public List<Node> GeneratePathByPoints(Tuple<int, int> currPoint, Tuple<int, int> targetPoint, Dictionary<(int, int), List<((int, int), double)>> graph){
        A_Star a_Star = new A_Star();
        Node currNode = new Node(currPoint.Item1, currPoint.Item2);
        Node targetNode = new Node(targetPoint.Item1, targetPoint.Item2);
        return a_Star.FindPath(graph, currNode, targetNode);

    }

}