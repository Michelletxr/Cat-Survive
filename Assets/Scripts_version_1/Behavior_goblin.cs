
//defirnir o comportamento do goblin
using System;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Goblin {

    public Goblin_v1 goblin { get; set; }
    private GameObject agent { get; set; }

    //variaveis para auxiliar a ronda
    private Transform[] pointsRound = agent.pointsToMove;
    private int currPoint;

    public float velocity = 1;

    public Behavior_Goblin(Goblin_v1 _goblin){
        goblin = _goblin;
        this.agent = goblin.gameObject;
        this.pointsRound = agent.pointsToMove;
        this.currPoint = 0;
    }

    public Action MoveToWall(){

        return new Action((agent) => {
            Transform wallPoss = pointsRound[currPoint];
            
            if(agent.transform.position  == wallPoss.position){ return STATE_TASK.SUCCEED;}
            
            agent.transform.position = Vector2.MoveTowards(agent.transform.position, 
                pointsRound[currPoint].position, 
                velocity * Time.deltaTime);
            
            return TaskStatus.RUNNING;
        }, agent);

    }

    public Action Turn(){

        return new Action((agent) => {
            
            if (agent.transform.position  == pointsRound[currPoint].position){
                currPoint += 1;
                lastPosX = agent.transform.localPosition.x;
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
        Sequence round = new Sequence(new List<Task> {this.MoveToWall(), this.Turn()});
        round.Execute();
        
    }
}
