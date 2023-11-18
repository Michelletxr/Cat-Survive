using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate STATE_TASK ProcessAction(GameObject agent);

public class Action : Task{
    private ProcessAction currAction;
    private GameObject agent;

    public Action(ProcessAction actionFunc, GameObject agentNPC){
        currAction = actionFunc;
        agent = agentNPC;
    }

    public STATE_TASK Execute(){
        return currAction(agent);
    }
}

public delegate bool VerifyCondition();

public class Condition : Task
{
    private VerifyCondition isCondition;

    public Condition(VerifyCondition conditionFunc) //GameObject agentNPC)
    {
        isCondition = conditionFunc;
    }

    public STATE_TASK Execute()
    {
        if (isCondition()){
            return STATE_TASK.SUCCEED;
        }
        else{
            return STATE_TASK.FAILED;
        }
    }
}
