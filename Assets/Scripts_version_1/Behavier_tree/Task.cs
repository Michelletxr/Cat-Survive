public enum STATE_TASK {SUCCEED, FAILED, RUNNING}

public interface Task
{
    //GameObject agent { get; set; }

   // STATE_TASK currState { get; set; }

    STATE_TASK Execute();
}