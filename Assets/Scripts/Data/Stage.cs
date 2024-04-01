using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stage
{
    public int StageId { get; }
    public string Title { get; set; }
    public int ActiveStepId { get; set; }
    public bool IsActive;
    public List<Step> Steps = new();

    public Stage(int stageId, string title)
    {
        StageId = stageId;
        Title = title;
        ActiveStepId = 1;
    }

    public void AddStep(Step step)
    {
        Steps.Add(step);
    }


    public Step GetCurrentStep()
    {
        return Steps.Find(x => x.StepId == ActiveStepId);
    }
}



