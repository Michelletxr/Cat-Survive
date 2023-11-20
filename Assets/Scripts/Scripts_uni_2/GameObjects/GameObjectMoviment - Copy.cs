using System;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectMoviment : MonoBehaviour
{
    public GameObject objectToMove;
    public List<Vector3> pathPoints; // Lista de pontos no caminho
    private float step = 5.0f; // A velocidade do movimento
    private int currentTargetIndex = 0; // O índice do próximo ponto no caminho
     private Vector3 currentTarget;
    private bool moveObject = false;

    void Update() {
        if(this.moveObject==true) {
            MoveObjectAlongPath();
        }
        
    }
    void Start() {
        this.InizializePathingVal();
    }

    public void InizializePathingVal() {
        this.moveObject = false;
        this.currentTargetIndex = 0;
        this.pathPoints = new List<Vector3>();
        this.currentTarget = new Vector3();
    }

    private bool CheckPositionTarget(Vector3 currentTarget){
        return this.objectToMove.transform.position == currentTarget;
    }

    public void MoveObjectAlongPath() {

        if (this.currentTargetIndex < pathPoints.Count) {
            // Move o objeto para o próximo ponto no caminho
            this.currentTarget = pathPoints[currentTargetIndex];
            this.objectToMove.transform.position = Vector2.MoveTowards(
                this.objectToMove.transform.position,
                currentTarget,
                step * Time.deltaTime
            );

            // Verifica se o objeto alcançou o ponto atual
            if (this.CheckPositionTarget(this.currentTarget)) {
                // Passa para o próximo ponto no caminho
                this.currentTargetIndex++;
            }
        }
    }

    public void SetObjectAndPath(GameObject gameObject, List<Node> pathNodes) {
        
        foreach (Node node in pathNodes) {
            pathPoints.Add(new Vector3(node.x, node.y, 0));
        }
        this.objectToMove = gameObject;
        this.moveObject = true;

    }
}
