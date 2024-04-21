using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stage
{
    public int StageId { get; }
    public string Title { get; set; }

    public bool IsDone { get; set; }

    public List<Step> Steps = new();

    public Stage(int stageId, string title)
    {
        StageId = stageId;
        Title = title;
    }

    public void AddStep(Step step)
    {
        Steps.Add(step);
    }
}



