
//defirnir o comportamento do goblin
using System;
using UnityEngine;

public class Behavior_Goblin {

    public Goblin_v1 goblin { get; set; }
    private GameObject agent { get; set; }

    public Behavior_Goblin(Goblin_v1 _goblin){
        goblin = _goblin;
        this.agent = goblin.gameObject;
    }

    public Action Dead
    {
        get
        {
            return new Action((agent) => {
                if (goblin.isLifeZero) {
                    GameObject.Destroy(agent);
                    return STATE_TASK.SUCCEED;
                }
                return STATE_TASK.FAILED;
            }, agent);
        }
    }

    public Action Damage
    {
        get
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
    }

    public Action AttackPlayer
    {
        get
        {
            return new Action((agent) => {
                if (goblin.isPlayerAround && !goblin.isPlayerOnCollisionEnter2D) {
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
                if (goblin.isPlayerAround && goblin.isPlayerOnCollisionEnter2D) {
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
                if (goblin.isPlayerAround) {
                    agent.transform.position = Vector2.MoveTowards(agent.transform.position, 
                    goblin.posPlayer.position, 1 * Time.deltaTime);
                    return STATE_TASK.SUCCEED;
                }
                return STATE_TASK.FAILED;
            }, agent);
        }
    }
}
