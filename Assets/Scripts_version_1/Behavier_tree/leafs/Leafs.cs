public delegate STATE ProcessAction();

public class Action: Task {

    //o agente que executa a ação
    private GameObject agent;
    //recebe uma função que executa uma ação pelo gameObject
    private ProcessAction currAction;

    public Action(GameObject gameObject, ProcessAction actionFunc){
        agent = gameObject;
        currAction = actionFunc;
    }

    //realiza a chamada da função executando a ação atual
    public STATE Execute(){
        return currAction();
    }
}


//função que checa uma condição
public delegate bool VerifyCondiction();

public class Condiction: Task {
    //recebe uma função que retorna se uma condição é verdadeira ou não
    private VerifyCondiction iscondiction;

    public Condiction(VerifyCondiction condictionFunc){
        iscondiction = condictionFunc;
    }


    //realiza a chamada da função verificando se ela é verdadeira ou não
    public STATE Execute(){
        if iscondiction()? STATE.SUCCEED else STATE.FAILED;
    }
}