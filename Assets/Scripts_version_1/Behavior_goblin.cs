
//defirnir o comportamento do goblin
using System;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Goblin {

    public Goblin_v1 goblin { get; set; }
    public GameObject agent { get; set; }

    //variaveis para auxiliar a ronda
    public Transform[] pointsRound { get; set; }
    private int currPoint;
    public float velocity = 1;

    public Behavior_Goblin(Goblin_v1 _goblin){
        goblin = _goblin;
        agent = GameObject.FindWithTag("goblin");
        //this.pointsRound = goblin.pointsToMove;
        this.pointsRound = new Transform[1];
        this.pointsRound[0] = GameObject.FindWithTag("rat").transform;
        this.currPoint = 0;
    }

    public Action MoveToWall()
    {

        Transform wallPoss = pointsRound[currPoint];

        return new Action((agent) => {
            Debug.Log("GOBLIN SE MOVE ATÉ PAREDE");

            if(agent.transform.position  == wallPoss.position){ return STATE_TASK.SUCCEED;}

            Debug.Log("GOBLIN SE MOVE ATÉ PAREDE");
            
            this.agent.transform.position = Vector2.MoveTowards(this.agent.transform.position, 
                GameObject.FindWithTag("rat").transform.position, 
                1 * Time.deltaTime);
                
            
            
            return STATE_TASK.SUCCEED;
        }, this.agent);

    }

    public Action Turn(){

        return new Action((agent) => {
            Debug.Log("Turn");
            if (goblin.isRatOnCollision().Execute() = STATE_TASK.SUCCEED){
                Debug.Log("HAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                currPoint += 1;
                float lastPosX = this.agent.transform.localPosition.x;
                if (currPoint >= pointsRound.Length){ currPoint = 0; }
                return STATE_TASK.SUCCEED;
            }
                return STATE_TASK.FAILED;
        }, agent);

    }

    public Action Dead()
    {
        return new Action((agent) => {

            if (goblin.isLifeZero()) {
                GameObject.Destroy(agent);
                return STATE_TASK.SUCCEED;
            }
                
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

    public Action AttackPlayer
    {
        get
        {
            return new Action((agent) => {
                if (goblin.isPlayerAround() && !goblin.isPlayerOnCollisionEnter2D()) {
                    Debug.Log("ATACANDO PLAYER");
                    return STATE_TASK.RUNNING;
                }
                return STATE_TASK.FAILED;
            }, agent);
        }
    }

    public Action KillerPlayer
    {
        get
        {
            return new Action((agent) => {
                if (goblin.isPlayerAround() && goblin.isPlayerOnCollisionEnter2D()) {
                    GameObject player = GameObject.FindWithTag("player");
                    if (player != null) {
                        GameObject.Destroy(player);
                    }
                    return STATE_TASK.SUCCEED;
                }
                return STATE_TASK.FAILED;
            }, agent);
        }
    }

    public Action MoveToPlayer
    {
        get
        {
            return new Action((agent) => {
                if (goblin.isPlayerAround()) {
                    agent.transform.position = Vector2.MoveTowards(agent.transform.position, 
                    goblin.posPlayer.position, 1 * Time.deltaTime);
                    return STATE_TASK.SUCCEED;
                }
                return STATE_TASK.FAILED;
            }, agent);
        }
    }


    

    public void createBehavior() {

        // --- COMPOSITIONS ---
        List<Task> actionsList = new List<Task> {MoveToWall(), Turn()};
        Sequence round = new Sequence(actionsList);
        Debug.Log(round.Execute());
        
    }
}
