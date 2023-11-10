
public class Inverter : Task{
    private Task task;

    public Inverter(Task _task){
        task = _task;
    }

    public STATE_TASK Execute(){
        STATE_TASK stateTask = task.Execute();
        switch (stateTask){
            case STATE_TASK.SUCCEED:
                return STATE_TASK.FAILED;
            case STATE_TASK.FAILED:
                return STATE_TASK.SUCCEED;
        }
        return STATE_TASK.RUNNING;
    }
}

public class RepeatUntilSucceed : Task{
    private Task task;

    public RepeatUntilSucceed(Task _task){
        task = _task;
    }

    public STATE_TASK Execute(){
        STATE_TASK stateTask = task.Execute();
        if (stateTask == STATE_TASK.SUCCEED){
            return STATE_TASK.SUCCEED;
        }
        return STATE_TASK.RUNNING;
    }
}

public class Repeat : Task{
    private Task task;

    public Repeat(Task _task){
        task = _task;
    }

    public STATE_TASK Execute(){
        task.Execute();
        return STATE_TASK.RUNNING;
    }
}
