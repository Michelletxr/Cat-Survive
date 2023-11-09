public enum STATE {SUCCEED, FAILED, RUNNING}

public interface Task
{
    //GameObject agent { get; set; }

    STATE currState { get; set; }

    STATE Execute();
}