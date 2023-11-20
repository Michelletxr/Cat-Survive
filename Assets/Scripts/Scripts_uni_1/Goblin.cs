using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATEE {AROUND, ATACK}

public class Arround : ObjectState{

    public Transform posPlayer;


    public Arround()
    {
        this.name = "arround";
        this.transition =  new Transition();
        posPlayer = GameObject.FindWithTag("player").transform;
    }

    public override void ProcessAction()
    {
        if(posPlayer.gameObject != null){
            agent.transform.position = Vector2.MoveTowards(agent.transform.position,
             posPlayer.position, 1 * Time.deltaTime);
        }
    }
    
}

public class Attackk : ObjectState{
    private float eixo_x, eixo_y;

    public Attackk(){
        this.name = "attack";
        this.transition=  new Transition();
    }

    public override void ProcessAction()
    {
        eixo_x = Input.GetAxis("Horizontal");
        eixo_y = Input.GetAxis("Vertical");

         Debug.Log("ATACAR");

    }
}
public class Goblin : MonoBehaviour
{
    public Rigidbody2D rb;
    public ObjectState currState;
    public FiniteStateMachine fsm;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        ObjectState[] estados = new ObjectState[2];
        estados[(int)STATEE.AROUND] = new Arround();
        estados[(int)STATEE.ATACK] = new Attackk();

      
        currState = estados[(int)STATEE.AROUND];
        currState.setGameObj(gameObject);
      

        fsm = new FiniteStateMachine();
        fsm.InitializationFSM(estados, currState);
        currState.event_type = ObjectState.EVENT.INIT;

        //posPlayer = GameObject.FindWithTag("player").transform;
        //currState.ProcessAction();
    }

    void Update()
    {
        currState.ProcessAction();
        
    }

}
