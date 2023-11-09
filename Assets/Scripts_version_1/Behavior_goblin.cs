
//defirnir o comportamento do goblin
public STATE Dead(){
    public STATE Execute(){
        if(this.agent.isLifeZero){
            Destroy(this.agent);
            return STATE.SUCCEED;
        }
        return STATE.FAILED;
    }
}

public STATE Damage(){
    public STATE Execute(){
        if(){
            Debug.Log("GOBLIN SOFRE DANO");
            return STATE.RUNNING;
        }
        return STATE.FAILED;
    }
}

public STATE AttackPlayer(){
    public STATE Execute(){
        if(this.agent.isPlayerAround and !this.agent.isPlayerOnCollisionEnter2D){
            Debug.Log("ATACANDO PLAYER");
            return STATE.RUNNING;
        }
        return STATE.FAILED;
    }
}

public STATE killerPlayer(){
     public STATE Execute(){
        if(this.agent.isPlayerOnCollisionEnter2D){
            GameObject.FindWithTag("player").SetActive(false)
            return STATE.SUCCEED;
        }
        return STATE.FAILED;
    }
}

public STATE moveToPlayer(){
    public STATE Execute(){
        if(this.agent.isPlayerAround){
            this.agent.transform.position = Vector2.MoveTowards(agent.transform.position,
            this.agent.posPlayer.position, 1 * Time.deltaTime);
            return STATE.SUCCEED;
        }
        return STATE.FAILED;
    }
}