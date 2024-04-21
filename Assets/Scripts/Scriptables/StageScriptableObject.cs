using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "StageScriptableObject", menuName = "ScriptableObject/Stage")]
public class StageScriptableObject : ScriptableObject
{
    public int ActiveStageId { get; set; } = 1;
    public int ActiveStepId { get; set; } = 1;
    private List<Stage> Stages = new();

    private Dictionary<string, List<string>> InitialStages = new() {
        { "Clean Wafer", new List<string>{
            "Pick up wafer",
            "Place wafer in acetone petri dish",
            "Place wafer in IPA petri dish",
            "Place wafer in DI Water petri dish",
            "Dry wafer with nitrogen gun"}
        },
        { "Application of photoresist", new List<string>{
            "Prebake wafer",
            "Apply photoresist to wafer",
            "Spread resist on wafer with spincoater",
            "Postbake the wafer"}
        }
    };

    StageScriptableObject(): base()
    {
        int i = 1, j;

        foreach (var stageKeyPair in InitialStages)
        {
            var stage = new Stage(1, $"Stage {i}: {stageKeyPair.Key}");
            j = 1;

            foreach (var stepStr in stageKeyPair.Value)
            {
                stage.AddStep(new Step(j, stepStr));
                j++;
            }

            Stages.Add(stage);
            i++;
        }
    }




    public void AddStage(Stage stage)
    {
        Stages.Add(stage);
    }

    public List<Stage> GetStages()
    {
        return Stages;
    }


    public void RemoveStage(Stage stage)
    {
        Stages.Remove(stage);
    }

    public Stage GetCurrentStage()
    {
        var stage = Stages.Find(x => x.StageId == ActiveStageId);
        return stage;
    }

    public List<Step> GetCurrentStageSteps()
    {
        return GetCurrentStage().Steps;
    }

    public Step GetCurrentStep()
    {
        return GetCurrentStageSteps().Find(x => x.StepId == ActiveStepId);
    }

    public void SetNextStage()
    {
        if (ActiveStageId >= Stages.Count) return;
        ActiveStageId++;
        ActiveStepId = 1;
    }

    public void SetNextStep()
    {
        if (ActiveStepId >= GetCurrentStageSteps().Count) return;
        ActiveStepId++;
    }
}
