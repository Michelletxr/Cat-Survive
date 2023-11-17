using System;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Rat {
    public Rat_v1 rat { get; set; }
    public GameObject agent { get; set; }
    //variaveis para auxiliar na coleta de queijo
    public GameObject[] objectsCheese { get; set; }
    private int currPoint;
    public float velocity;
    public GridMoviments gridMoviments;
    public MainMaze mainMaze;
    public Tuple<int, int> pointRat;
    public List<Tuple<int, int>> pointsCheese;
   
    public Condition isCheeseOnCollision(){
        return new Condition(() => {
            return rat.collideCheese;
        });
    }

    public Condition isLifeZero(){
        return new Condition(() => {
            return rat.life == 0;
        });
    }


    public Action CheckPointsCheese() {
        return new Action((agent) => {

           this.objectsCheese = GameObject.FindGameObjectsWithTag("item");
            
            if(objectsCheese.Length > 0) {
                Debug.Log("EXISTE QUEIJO NA REGIÃO");
                this.currPoint = objectsCheese.Length - 1;
                return STATE_TASK.SUCCEED;
            }
            
            return STATE_TASK.FAILED;
        }, this.agent);
    }

    public Action MoveToCheese() {

        return new Action((agent) => {

            if(mainMaze.graphGenerate == null || mainMaze.itemsPoints == null) {return STATE_TASK.FAILED;}

            if(this.pointsCheese == null){ 
                
                this.pointsCheese = this.gridMoviments.OrderPointsByDistance(mainMaze.itemsPoints);

            }

            Tuple<int, int> nextPoint = this.pointsCheese[0];
            List<Node> path_cheese = gridMoviments.GeneratePathByPoints(this.pointRat, nextPoint, mainMaze.graphGenerate);

            
            if(agent.transform.position  == new Vector3((float)nextPoint.Item1, (float)nextPoint.Item2, 0)){ 
                this.pointRat = nextPoint;
                return STATE_TASK.SUCCEED;
            }else{

                Node nextNode = gridMoviments.getNextNode(path_cheese);
                gridMoviments.MoveToPoints(this.agent, nextNode);
                this.pointRat = new Tuple<int, int>(nextNode.x, nextNode.y);
                Debug.Log("RATO esta no ponto: " + nextNode);
            }

            return STATE_TASK.FAILED;

        }, this.agent);
    }

    public Action colletCheese() {
        return new Action((agent) => {
            Debug.Log("RATO COLETA QUEIJO");
            STATE_TASK stateCollision = isCheeseOnCollision().Execute();
            GameObject objectDisable = gridMoviments.FindObjectByPoint(objectsCheese, this.pointsCheese[0]);
            objectDisable.SetActive(false);
            pointsCheese.RemoveAt(0);
            return STATE_TASK.SUCCEED;
        }, this.agent);
    }

        public Action FindOut(){ 
            return new Action((agent) => {
                    if(mainMaze.graphGenerate == null) {return STATE_TASK.FAILED;}

                    GameObject.Find("end");
                    return STATE_TASK.SUCCEED;

            }, this.agent); }

        public Action MoveToOut(){ 
            return new Action((agent) => {
                
                Tuple<int, int> endPoint = new Tuple<int, int>(10,9);
                List<Node> path_end = gridMoviments.GeneratePathByPoints(this.pointRat, endPoint, mainMaze.graphGenerate);

            
            if(agent.transform.position  == new Vector3((float)endPoint.Item1, (float)endPoint.Item2, 0)){ 
                
                this.pointRat = endPoint;

                return STATE_TASK.SUCCEED;

            }else{
   
                Node nextNode = gridMoviments.getNextNode(path_end);
                gridMoviments.MoveToPoints(this.agent, nextNode);
                this.pointRat = new Tuple<int, int>(nextNode.x, nextNode.y);
                
                Debug.Log("RATO esta no ponto: " + nextNode);
            }

            return STATE_TASK.FAILED;
            }, this.agent);}

        public Action LeaveMap(){ 
            return new Action((agent) => {
                return STATE_TASK.FAILED;
        }, this.agent); }

    

    public Behavior_Rat(Rat_v1 _rat, MainMaze main){
        rat = _rat;
        mainMaze = main;
        gridMoviments = new GridMoviments();
        agent = GameObject.FindWithTag("rat");
        pointRat = new Tuple<int, int>(0,0);
        this.velocity = 1;
        this.currPoint = -1;
    }

    public void createBehavior() {
        // --- COMPOSITIONS ---
        List<Task> actionsList = new List<Task> {CheckPointsCheese(), MoveToCheese(), colletCheese()};
        List<Task> actionsList_1 = new List<Task> {FindOut(), MoveToOut()};
        Sequence findCheese = new Sequence(actionsList);
        Sequence findOut = new Sequence(actionsList_1);
        findCheese.Execute();
        findOut.Execute();
        
    }
}