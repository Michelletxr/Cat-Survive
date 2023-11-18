using System;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectMoviment : MonoBehaviour
{
    public GameObject objectToMove;
    public List<Vector2> pathPoints; // Lista de pontos no caminho
    private float step = 1.0f; // A velocidade do movimento
    private int currentTargetIndex = 0; // O índice do próximo ponto no caminho
     private bool moveObject = false;

    void Update()
    {
        if(moveObject){
            MoveObjectAlongPath();
        }
        
    }
    void Start(){
        InicializeVariabels();
    }

    void InicializeVariabels() {
        this.moveObject = false;
        this.currentPointIndex = 0;
        this.pathPoints = new List<Vector2>{};
    }

    void MoveObjectAlongPath() {
        if (currentTargetIndex < pathPoints.Count)
        {
            // Move o objeto para o próximo ponto no caminho
            Vector2 currentTarget = pathPoints[currentTargetIndex];
            objectToMove.transform.position = Vector2.MoveTowards(
                objectToMove.transform.position,
                currentTarget,
                step * Time.deltaTime
            );

            // Verifica se o objeto alcançou o ponto atual
            if (objectToMove.transform.position == currentTarget)
            {
                // Passa para o próximo ponto no caminho
                currentTargetIndex++;
            }
        }
    }

    void SetObjectAndPath(GameObject gameObject, List<Node> pathNodes){
        
        foreach (Node node in pathNodes) {
            pathPoints.Add(new Vector2(node.x, node.y));
        }
        this.objectToMove = gameObject;

    }
}
