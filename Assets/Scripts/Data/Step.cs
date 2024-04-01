using System.Collections;
using System.Collections.Generic;


public class Step
{
    public int StepId { get; }
    public string Title { get; set; }
    public bool IsDone { get; set; }

    public Step(int stepId, string title)
    {
        StepId = stepId;
        Title = title;
    }
}



