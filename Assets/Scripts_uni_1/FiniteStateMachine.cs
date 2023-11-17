using System;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine.PlayerLoop;
using UnityEngine;

public class ObjectState {

    public enum EVENT {INIT, EXIT}
    public string name;
    public GameObject agent;
    public EVENT event_type;
    public Transition transition;

    public void setGameObj(GameObject gameObject){
        this.agent = gameObject;
    }
    //enquanto o objeto estiver nesse estado ele executa esta ação
    public virtual void ProcessAction(){
        Debug.Log("executar ação");
    }


}

public class Transition {
    public int target;
    public int curr;
    public bool triggred;
    public Transition() {
        this.triggred = false;
    }
    public bool IsTriggered()   {
        return triggred;

    }
}

public class FiniteStateMachine {
    public ObjectState[] estados;
    public ObjectState currState;
    public GameObject agent;

    public void InitializationFSM(ObjectState[] estados, ObjectState state){
        this.currState = state;
        this.estados = estados;
    }

    public ObjectState UpdateState(Transition transition) {
        currState = this.estados[transition.target];
        return currState;
    }

}