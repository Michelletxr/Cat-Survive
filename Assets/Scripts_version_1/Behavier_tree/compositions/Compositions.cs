public class Sequence : Task {

    private Task[] subtasks;

    public Sequence(tasks: Task[]){
        subtasks = tasks;
    }

    public STATE Execute(){

        foreach (Task task in subtasks){
            foreach (Task task in subtasks){
                STATE stateTask = task.Execute();
                if (stateTask != STATE.SUCCEED){
                    return STATE.FAILED;
                }
            }
            return STATE.SUCCEED;
        }

    }

}

public class Selector : Task {
    private Task[] subtasks;

    public Selector(tasks: Task[]){
        subtasks = tasks;
    }

     public STATE Execute(){

        foreach (Task task in subtasks){
            STATE stateTask = task.Execute();
            if (stateTask != STATE.FAILED){
                return STATE.SUCCEED;
            }
        }
        return STATE.FAILED;
    }

}

public class Parallel : Task {
    private Task[] subtasks;
    private int totalSucced = 0;
    private int totalFailed = 0;

     public Parallel(tasks: Task[]){
        subtasks = tasks;
    }

    public STATE Execute(){
        
        int countSuccess = 0
        int countFailure = 0
        STATE result = STATE.RUNNING;
        STATE status = task.execute(gameObj)
        switch (status) {
            case Task.SUCCEED: countSuccess++; break;
            case Task.FAILED: countFailure++; break;
        }

        if(countSuccess >= this.totalSucced){
            result = Task.SUCCEED;
        }else if(countFailure >= this.totalFailed){
            result = Task.FAILED;
        }

        return result;
    }

}