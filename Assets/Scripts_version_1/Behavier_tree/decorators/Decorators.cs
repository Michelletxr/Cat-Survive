public class Inverter : Task {

    private Task task;

    public Inverter(_task: Task){
        task = _tasks;
    }

    public STATE Execute(){

        STATUS status = this.task.Execute();
        switch (status) {
            case STATE.SUCCEED: return STATE.FAILED;
            case STATE.FAILED: return STATE.SUCCEED;
        }
        return STATE.RUNNING;
    }

}


public class RepeatUntilSucceed : Task {
    private Task task;

    public RepeatUntilSucceed(_task: Task){
        task = _tasks;
    }

    public STATE Execute(){

        STATUS status = this.task.Execute();
        if (status === STATE.SUCCEED) {
            return STATE.SUCCEED;
        }
        return STATE.RUNNING;
    }
}

public class Repeat : Task {

    private Task task;

    public Repeat(_task: Task){
        task = _tasks;
    }

    public STATE Execute(){

        this.task.Execute();
        return STATE.RUNNING;
    }

}