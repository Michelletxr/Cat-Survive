
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
    public float velocity = 1;

    public Behavior_Goblin(Goblin_v1 _goblin){
        goblin = _goblin;
        agent = GameObject.FindWithTag("goblin");
        this.pointsRound = GameObject.FindGameObjectsWithTag("PointsRound");
        this.currPoint = 0;
    }


    /// conditions
    public Condition IsRatOnCollision(){
        return new Condition(() => {
            return goblin.collideRat;
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

            if(agent.transform.position  == wallPoss.position){ return STATE_TASK.SUCCEED;}

            Debug.Log("GOBLIN SE MOVE ATÉ PAREDE");
            
            this.agent.transform.position = Vector2.MoveTowards(this.agent.transform.position, 
                wallPoss.position, 
                1 * Time.deltaTime);
                
            return STATE_TASK.SUCCEED;
        }, this.agent);

    }

    public Action Turn(){

        return new Action((agent) => {
            Debug.Log("Turn");
            STATE_TASK stateCollision = IsPointsOnCollision().Execute();
            if (stateCollision == STATE_TASK.SUCCEED){
                goblin.collidePointsRound = false;
                currPoint += 1;
                
                Vector3 lastPosY = agent.transform.localScale;
                lastPosY.y = lastPosY.y * -1;
                agent.transform.localScale = lastPosY;
                
                if (currPoint >= pointsRound.Length){ currPoint = 0; }
                return STATE_TASK.SUCCEED;
            }
                return STATE_TASK.FAILED;
        }, agent);

    }

    public Action Dead()
    {
        return new Action((agent) => {
 
            return STATE_TASK.FAILED;
            }, agent);
    }

    public Action Damage()
    {
            return new Action((agent) => {
                bool condition = true; // Substitua por sua própria condição
                if (condition) {
                    Debug.Log("GOBLIN SOFRE DANO");
                    return STATE_TASK.SUCCEED;
                }
                return STATE_TASK.FAILED;
            }, agent);
        
    }


    public Action MoveToRat(){
            return new Action((agent) => {
                STATE_TASK stateCollision = IsRatOnCollision().Execute();
                if(stateCollision == STATE_TASK.SUCCEED){ 
                    Debug.Log("GOBLIN PERCEGUE RATO");
                     
                    this.agent.transform.position = Vector2.MoveTowards(this.agent.transform.position, 
                        GameObject.FindWithTag("rat").transform.position, 
                        1 * Time.deltaTime);

                    return STATE_TASK.SUCCEED;
                }
                     
                return STATE_TASK.FAILED;
            }, agent);
    }


    public Action AttackRat() {
        //aciona evento de dano
        //criar evento de dano no rato
        return new Action((agent) => {
            Debug.Log("GOBLIN ATACA RATO");
            return STATE_TASK.FAILED;
        }, agent);
    }

    

    public void createBehavior() {

        // --- COMPOSITIONS ---
        //List<Task> actionsList = new List<Task> {MoveToWall(), Turn()};
        List<Task> actionsList_1 = new List<Task> {IsRatOnCollision(), MoveToRat(), AttackRat()};
       // Sequence round = new Sequence(actionsList);
        Sequence captureRat = new Sequence(actionsList_1);

        //definir comportamento para seguir o rato ou fazer ronda
        //Selector selector = new Selector(new List<Task>{round, captureRat});
        Selector selector = new Selector(new List<Task>{captureRat});
        selector.Execute();
        //atacar rato  
    }
}
