using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum STATE {IDLE, WALLKING, COLLET, ATTACK}

public class Idle : ObjectState{

    public Idle()
    {
        this.name = "idle";
        this.transition =  new Transition();
    }

    public override void ProcessAction()
    {
        float eixo_x = Input.GetAxis("Horizontal");
        float eixo_y = Input.GetAxis("Vertical");
        
        if(eixo_x != 0 ){
            Debug.Log("mudar animação para walking");
            this.transition.triggred = true;
            this.transition.target = (int)STATE.WALLKING;
            this.event_type = ObjectState.EVENT.EXIT;
        }

    }
    
}

public class wallking : ObjectState{
    private float eixo_x, eixo_y;

    public wallking(){
        this.name = "wallking";
        this.transition=  new Transition();
    }

    public override void ProcessAction()
    {
        eixo_x = Input.GetAxis("Horizontal");
        eixo_y = Input.GetAxis("Vertical");

        if(eixo_x != 0 || eixo_y != 0){
            var rb = this.agent.GetComponent<Rigidbody2D>();
           Vector3 move = new Vector3(eixo_x, eixo_y, 0f);
           agent.transform.position = agent.transform.position + 2 * move * Time.deltaTime;
        }else{
            Debug.Log("mudar animação para idle");
            this.transition.target = (int)STATE.IDLE;
            this.event_type = ObjectState.EVENT.EXIT;
        }

    }
}

public class Collect : ObjectState{

    Player player;

    public Collect(Player ply){
        player = ply;
        this.name = "collet";
        this.transition=  new Transition();
    }
    public override void ProcessAction()
    {
        player.totalRats +=1;
    }
}

public class Attack : ObjectState{

    Player player;

    public Attack(Player ply){
        player = ply;
        this.transition=  new Transition();
    }
    public override void ProcessAction()
    {
        //atualizar vida do player
    }
    
}


public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public ObjectState currentState;
    public FiniteStateMachine fsm;
    public int totalRats = 0;
    public int healt = 100;
    void Start()
    {
        Debug.Log("Bem vindo ao Cats Survive!");
        rb = GetComponent<Rigidbody2D>();

        ObjectState[] estados = new ObjectState[4];
        estados[(int)STATE.IDLE] = new Idle();
        estados[(int)STATE.WALLKING] = new wallking();
        estados[(int)STATE.COLLET] = new Collect(this);
        estados[(int)STATE.ATTACK] = new Attack(this);

      
        currentState = estados[(int)STATE.IDLE];
        currentState.setGameObj(gameObject);
      

        fsm = new FiniteStateMachine();
        fsm.InitializationFSM(estados, currentState);
        currentState.event_type = ObjectState.EVENT.INIT;
        currentState.ProcessAction();
    }

    void Update()
    {

        if(currentState.event_type == ObjectState.EVENT.EXIT){
          //  Debug.Log("EVENTO DISPARADO");
            currentState = fsm.UpdateState(currentState.transition);
            currentState.setGameObj(gameObject);
            currentState.event_type = ObjectState.EVENT.INIT;
        }else{
           // Debug.Log("ESTADO ATUAL");
           // Debug.Log(currentState.name);
            currentState.ProcessAction();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COLIDIU");

        if(collision.gameObject.tag == "rat")
        {
            collision.gameObject.SetActive(false);
            totalRats+=1;
            
        }else if(collision.gameObject.tag == "enemy")
        {
            healt-=10;
            if(healt<=0){
                Destroy(gameObject);
            }
        }

        
    }

    
}
