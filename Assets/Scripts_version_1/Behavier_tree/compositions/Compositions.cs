
using System;
using System.Collections.Generic;

public class Sequence : Task{
    private  List<Task> subtasks;

    public Sequence(List<Task>  tasks){
        subtasks = tasks;
    }

    public STATE_TASK Execute(){

        foreach (Task task in subtasks){
            STATE_TASK stateTask = task.Execute();
            if (stateTask != STATE_TASK.SUCCEED){
                return STATE_TASK.FAILED;
            }
        }
        return STATE_TASK.SUCCEED;
    }
}

public class Selector : Task{
    private Task[] subtasks;

    public Selector(Task[] tasks){
        subtasks = tasks;
    }

    public STATE_TASK Execute(){
        foreach (Task task in subtasks){
            STATE_TASK stateTask = task.Execute();
            if (stateTask != STATE_TASK.FAILED){
                return STATE_TASK.SUCCEED;
            }
        }
        return STATE_TASK.FAILED;
    }
}

public class Parallel : Task{
    private Task[] subtasks;
    private int totalSucceed = 0;
    private int totalFailed = 0;

    public Parallel(Task[] tasks, int succeedThreshold, int failThreshold){
        subtasks = tasks;
        totalSucceed = succeedThreshold;
        totalFailed = failThreshold;
    }

    public STATE_TASK Execute(){
        int countSuccess = 0;
        int countFailure = 0;
        STATE_TASK result = STATE_TASK.RUNNING;

        foreach (Task task in subtasks){
            STATE_TASK stateTask = task.Execute();
            switch (stateTask){
                case STATE_TASK.SUCCEED:
                    countSuccess++;
                    break;
                case STATE_TASK.FAILED:
                    countFailure++;
                    break;
            }
        }

        if (countSuccess >= totalSucceed){
            result = STATE_TASK.SUCCEED;
        }
        else if (countFailure >= totalFailed){
            result = STATE_TASK.FAILED;
        }

        return result;
    }
}
