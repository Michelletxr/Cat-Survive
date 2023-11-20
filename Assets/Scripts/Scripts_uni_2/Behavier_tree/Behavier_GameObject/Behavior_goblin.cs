
//defirnir o comportamento do goblin
using System;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Goblin {
    public Goblin_v1 goblin { get; set; }
    public GameObject agent { get; set; }
    //variaveis para auxiliar a ronda
    public GameObject[] pointsRound { get; set; }
    private int currPoint;

    public GridMoviments_v1 gridMoviments;
    public GameObjectMoviment movimentScript;
    public Tuple<int, int> pointGoblin;
    public MainMaze mainMaze;

    public Behavior_Goblin(Goblin_v1 _goblin, MainMaze main){
        goblin = _goblin;
        agent = GameObject.FindWithTag("enemy");
        this.pointsRound = GameObject.FindGameObjectsWithTag("PointsRound");
        //this.pointGoblin = this.movimentScript.FindObjcetPos(this.agent);
        this.currPoint = 0;

        mainMaze = main;
        gridMoviments = new GridMoviments_v1();
        GameObject gameAgentsObj = GameObject.Find("GameAgents");

        if (gameAgentsObj != null) {
           this.movimentScript = gameAgentsObj.GetComponent<GameObjectMoviment>();
           this.pointGoblin = gridMoviments.FindObjcetPos(this.agent);
        }
    }


    /// conditions
    public Condition IsRatOnArea(){
        return new Condition(() => {
            Debug.Log("checando colisão com rato: " + goblin.collideRat);
            return goblin.collideRat || goblin.chaseRat;
        });
    }

    public Condition IsPointsOnCollision(){
        return new Condition(() => {
            return goblin.collidePointsRound;
        });
    } 

    //actions
    public Action MoveToWall(){

        Transform wallPoss = pointsRound[currPoint].transform;

        return new Action((agent) => {
            Debug.Log("GOBLIN SE MOVE ATÉ PAREDE");   
            return STATE_TASK.FAILED;
        }, this.agent);
    }

    public Action Turn(){

        return new Action((agent) => {
            Debug.Log("Turn");
            return STATE_TASK.FAILED;
        }, agent);

    }


    public Action MoveToRat(){
            return new Action((agent) => {
                
                //pega localização do rato
                if(mainMaze.graphGenerate == null ) {return STATE_TASK.FAILED;}
                

                //pointGoblin = gridMoviments.FindObjcetPos(this.agent);
                Tuple<int, int> nextPoint = gridMoviments.FindObjcetPos(GameObject.Find("Rat"));
                Debug.Log("RATO ESTA NA POS: " + nextPoint);
                List<Node> path_rat = gridMoviments.GeneratePathByPoints(pointGoblin, nextPoint, mainMaze.graphGenerate);
            
               if(agent.transform.position  == new Vector3(nextPoint.Item1, nextPoint.Item2, 0)){ 
                    this.pointGoblin = nextPoint;
                    this.movimentScript.InizializePathingVal();
                    goblin.collideRat = true;
                    return STATE_TASK.SUCCEED;
                } else {
                    goblin.chaseRat = true;
                    goblin.collideRat = false;
                    this.movimentScript.SetObjectAndPath(this.agent, path_rat);
                }

            return STATE_TASK.FAILED;
            }, agent);
    }


    public Action AttackRat() {
        //aciona evento de dano
        //criar evento de dano no rato
        return new Action((agent) => {
            if(goblin.collideRat){
                GameObject.FindWithTag("rat").SetActive(false);
                //Debug.Log("GOBLIN capura RATO");
                goblin.collideRat = false;
                goblin.chaseRat = false;
                return STATE_TASK.SUCCEED;
            }
            //GameObject.FindWithTag("rat").SetActive(false);
            return STATE_TASK.FAILED;
        }, agent);
    }

    


    //definir comportamentos de ronda e de capturar ratos
    public void createBehavior() {

        // --- COMPOSITIONS ---
        //List<Task> actionsList = new List<Task> {MoveToWall(), Turn()};
        List<Task> actionsList_1 = new List<Task> {IsRatOnArea(), MoveToRat(), AttackRat()};
        // Sequence round = new Sequence(actionsList);
        Sequence captureRat = new Sequence(actionsList_1);
        //captureRat.Execute();

        //definir comportamento para seguir o rato ou fazer ronda
        //Selector selector = new Selector(new List<Task>{round, captureRat});
        //Selector selector = new Selector(new List<Task>{captureRat});
        //selector.Execute();
    }
}
